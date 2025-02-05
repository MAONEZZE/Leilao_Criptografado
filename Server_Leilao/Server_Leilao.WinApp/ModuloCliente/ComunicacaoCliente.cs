using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using Server_Leilao.WinApp.Models;

namespace Server_Leilao.WinApp.ModuloCliente
{
    public class ComunicacaoCliente
    {
        private const string ip = "127.0.0.1"; // ip para todos na rede poderem acessar
        private const int port = 51000; // Porta desejada

        private readonly TcpListener servidor;

        private bool isRunning = false;

        public ComunicacaoCliente()
        {
            servidor = new TcpListener(IPAddress.Parse(ip), port);
            servidor.Start();
            isRunning = true;

            Thread threadLerMensagem = new Thread(() => LerMensagem());
            threadLerMensagem.Start();
        }

        private void EnviarMsg(string mensagem, NetworkStream fluxoDados)
        {
            try
            {
                byte[] respostaBytes = Encoding.UTF8.GetBytes(mensagem);
                fluxoDados.Write(respostaBytes, 0, respostaBytes.Length);

                fluxoDados.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
        }

        private void LerMensagem()
        {
            try
            {
                while (isRunning)
                {
                    if (servidor.Pending())
                    {
                        TcpClient client = servidor.AcceptTcpClient();

                        Thread thread = new Thread(() => ProcessarReqCliente(client));
                        thread.Start();
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
            
            EncerrarServidor();
        }

        private void EncerrarServidor()
        {
            // Encerra o servidor
            if (servidor != null)
            {
                servidor.Stop();
            }
        }

        public void Dispose()
        {
            isRunning = false; // Para o loop principal
            EncerrarServidor();
        }

        private void ProcessarReqCliente(TcpClient client)
        {
            try
            {
                NetworkStream fluxoDados = client.GetStream();

                // Recebe a mensagem do cliente
                byte[] bufferDadosRecebidos = new byte[65507];
                int qtdBytesRecebidos = fluxoDados.Read(bufferDadosRecebidos, 0, bufferDadosRecebidos.Length);

                // Redimensiona para o tamanho exato
                byte[] bytesRecebidos = new byte[qtdBytesRecebidos];
                Array.Copy(bufferDadosRecebidos, bytesRecebidos, qtdBytesRecebidos);

                string dadosRecebidosJson = Encoding.UTF8.GetString(bytesRecebidos);

                // Verificar existência do cliente no JSON
                var publicKey = VerificarUsuarioNaLista(dadosRecebidosJson);

                if (publicKey.Equals(""))
                {
                    EnviarMsg("ERRO: Mensagem recebida, porém não foi encontrado nenhum usuário com este CPF!", fluxoDados);
                    return;
                }

                // Processa o envio da resposta
                ProcessoDeEnvio(publicKey, client, fluxoDados);
                
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO: " + ex.Message);
            }

            client.Close();
        }

        private void ProcessoDeEnvio(string publicKey, TcpClient client, NetworkStream fluxoDados)
        {
            try
            {
                // Criptografar a msg com a chave publica dele
                var (dadosCriptografados, chaveSimetricaEncriptada) = CriptografarMensagem(publicKey);

                var objEnvio = new
                {
                    InfoLeilao = dadosCriptografados,
                    ChaveSimetricaEncriptada = chaveSimetricaEncriptada,
                    IvObj = TelaPrincipal.Iv
                };


                // Envia uma resposta ao cliente
                string objJson = JsonSerializer.Serialize(objEnvio);
                EnviarMsg(objJson, fluxoDados);
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO: " + ex.Message);
            }

            client.Close(); // Fecha a conexão com o cliente
        }

        private (byte[], byte[]) CriptografarMensagem(string publicKey)
        {
            try
            {
                Aes aes = Aes.Create(); //para criptografia simétrica
                RSA rsa = RSA.Create(); //para criptografia assimétrica
                var ms = new MemoryStream();
                DadosLeilao dadosLeilao = new DadosLeilao("127.0.0.1", 53000, 54000, 56000);

                aes.Key = TelaPrincipal.ChaveSimetrica;
                aes.IV = TelaPrincipal.Iv;

                // Criptografa os dados com a chave simétrica
                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] dadosCriptografados;

                var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

                var objJson = JsonSerializer.Serialize(dadosLeilao);
                byte[] dados = Encoding.UTF8.GetBytes(objJson);

                cs.Write(dados, 0, dados.Length);
                cs.FlushFinalBlock();
                dadosCriptografados = ms.ToArray();

                // Criptografa a chave simétrica com a chave pública do destinatário
                byte[] chaveSimetricaEncriptada = CriptografarChaveSimetrica(publicKey, rsa);

                return (dadosCriptografados, chaveSimetricaEncriptada);
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO: " + ex.Message);
            }
        }

        private byte[] CriptografarChaveSimetrica(string publicKey, RSA rsa)
        {
            try
            {
                byte[] keyBytes = Convert.FromBase64String(publicKey);

                rsa.ImportSubjectPublicKeyInfo(keyBytes, out _); // Importa a chave pública
                var chaveRSA = rsa.ExportParameters(false);

                rsa.ImportParameters(chaveRSA);
                return rsa.Encrypt(TelaPrincipal.ChaveSimetrica, RSAEncryptionPadding.OaepSHA256);
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO: " + ex.Message);
            }
        }

        private string VerificarUsuarioNaLista(string mensagem)
        {
            var objCliente = JsonSerializer.Deserialize<Cliente>(mensagem);

            string caminhoArquivo = "C:\\Users\\ruan.souza\\Documents\\Meus_Programas\\Leilao_Criptografado\\Server_Leilao\\Server_Leilao.WinApp\\Database\\Users.json";
            string json = File.ReadAllText(caminhoArquivo);

            string publicKey = "";

            // Parse do JSON para um objeto JsonDocument
            using (JsonDocument document = JsonDocument.Parse(json))
            {
                JsonElement root = document.RootElement;

                // Percorre as chaves no JSON
                publicKey = PercorrerChaves(root, objCliente);
            }

            return publicKey.ToString();
        }

        private string PercorrerChaves(JsonElement elemento, Cliente objCliente)
        {
            string publicKey = "";  

            foreach (JsonProperty propriedade in elemento.EnumerateObject())
            {
                if (propriedade.Name == objCliente.Cpf)
                {
                    publicKey = propriedade.Value.ToString();
                    break;
                }
            }

            return publicKey;
        }
    }
}
