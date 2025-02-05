using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading;
using Client_Leilao.WinApp.Models;
using Client_Leilao.WinApp.ModuloLeilao;

namespace Client_Leilao.WinApp
{

    public partial class Tela_Principal : Form
    {
        public static Tela_Principal telaPrincipal;
        public byte[] ChaveSimetrica { get; set; }
        public byte[] Iv { get; set; }

        private Cliente objCliente;

        public Tela_Principal()
        {
            InitializeComponent();

            telaPrincipal = this;

            this.label_status.Text = "";
        }

        private void btn_entrar_Click(object sender, EventArgs e)
        {

            foreach (var items in this.panel.Controls)
            {
                if (items.GetType() == typeof(TextBox))
                {
                    var textBox = (TextBox)items;
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        AtualizarRodaPe("Preencha todos os campos!", true);
                        return;
                    }
                }
            }

            foreach (var items in this.groupBox1.Controls)
            {
                if (items.GetType() == typeof(TextBox))
                {
                    var textBox = (TextBox)items;
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        AtualizarRodaPe("Preencha todos os campos!", true);
                        return;
                    }
                }
            }

            //EstadosDosCampos(false);
            ProcessoDeComunicacao();
        }

        private void ProcessoDeComunicacao()
        {
            string caminhoPrivateKey = @"C:\Users\ruan.souza\Documents\Meus_Programas\Leilao_Criptografado\Client_Leilao\Client_Leilao.WinApp\Chaves\privateKey.base64";
            string ip = txb_ip.Text; // 127.0.0.1 
            int port = int.Parse(txb_port.Text); // 51000

            try
            {
                // Cria uma conexão com o servidor
                TcpClient cliente = new TcpClient(ip, port);

                // Fluxo de rede para enviar e receber dados
                NetworkStream fluxoDados = cliente.GetStream();

                EnviarMensagem(fluxoDados);
                var (jsonRecebido, sucesso) = ReceberMensagem(fluxoDados, cliente, caminhoPrivateKey);

                if (!sucesso)
                {
                    EstadosDosCampos(true);
                    AtualizarRodaPe(jsonRecebido.ToString(), true);
                    return;
                }

                Leilao objLeilao = (Leilao)jsonRecebido;

                FormatarDadosRecebidos(objLeilao);
            }
            catch (Exception ex)
            {
                EstadosDosCampos(true);
                AtualizarRodaPe(ex.Message, true);
            }
        }

        private (object, bool) ReceberMensagem(NetworkStream fluxoDados, TcpClient cliente, string caminhoPrivateKey)
        {
            try
            {
                string privateKey = File.ReadAllText(caminhoPrivateKey);
                bool sucesso = false;

                byte[] bufferDadosRecebidos = new byte[65507];
                int qtdBytesRecebidos = fluxoDados.Read(bufferDadosRecebidos, 0, bufferDadosRecebidos.Length);

                if (qtdBytesRecebidos == 0)
                {
                    return ("Erro ao receber dados do servidor!", sucesso);
                }

                // Redimensiona para o tamanho exato
                byte[] bytesRecebidos = new byte[qtdBytesRecebidos];
                Array.Copy(bufferDadosRecebidos, bytesRecebidos, qtdBytesRecebidos);

                string dadosRecebidosJson = Encoding.UTF8.GetString(bytesRecebidos);

                if (dadosRecebidosJson.Contains("ERRO"))
                {
                    return (dadosRecebidosJson, sucesso);
                }

                fluxoDados.Close();
                cliente.Close();

                JsonElement root = LerJSON(dadosRecebidosJson);

                byte[] infoLeilaoCriptografados = Convert.FromBase64String(root.GetProperty("InfoLeilao").GetString());
                byte[] chaveSimetricaEncriptada = Convert.FromBase64String(root.GetProperty("ChaveSimetricaEncriptada").GetString());
                byte[] iv = Convert.FromBase64String(root.GetProperty("IvObj").GetString());

                DadosRecebidos dadosRecebidosEncriptados = new DadosRecebidos(infoLeilaoCriptografados, chaveSimetricaEncriptada, iv);

                byte[] privateKeyBytes = Convert.FromBase64String(privateKey);

                return DecriptografarDadosRecebidos(dadosRecebidosEncriptados, privateKeyBytes, root, sucesso);
            }
            catch (Exception ex)
            {
                return ("Erro: " + ex.Message, false);
            }
        }

        private (Leilao, bool) DecriptografarDadosRecebidos(DadosRecebidos dadosRecebidosEncriptados, byte[] privateKeyBytes, JsonElement root, bool sucesso)
        {
            try
            {
                RSA rsa = RSA.Create();
                Aes aes = Aes.Create();

                rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
                byte[] chaveSimetrica = rsa.Decrypt(dadosRecebidosEncriptados.ChaveSimetricaEncriptada, RSAEncryptionPadding.OaepSHA256);

                // Decripta os dados usando AES
                ChaveSimetrica = chaveSimetrica;
                Iv = dadosRecebidosEncriptados.IvObj;

                aes.Key = chaveSimetrica;
                aes.IV = dadosRecebidosEncriptados.IvObj;

                ICryptoTransform decryptor = aes.CreateDecryptor();

                var ms = new MemoryStream(dadosRecebidosEncriptados.InfoLeilao);
                var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                var sr = new StreamReader(cs);

                var dadosDecriptografados = sr.ReadToEnd();
                sucesso = !dadosDecriptografados.Contains("ERRO:");

                root = LerJSON(dadosDecriptografados);

                var porta = root.GetProperty("Porta").GetInt32();
                var ip = root.GetProperty("Ip").GetString();
                var portaChat = root.GetProperty("PortaChat").GetInt32();
                var portaTempo = root.GetProperty("PortaTempo").GetInt32();

                Leilao objLeilao = new Leilao(ip, porta, portaChat, portaTempo, chaveSimetrica, dadosRecebidosEncriptados.IvObj);

                return (objLeilao, sucesso);
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message); 
            }
        }

        public JsonElement LerJSON(string json)
        {
            JsonDocument doc = JsonDocument.Parse(json);
            return doc.RootElement;
        }

        private void EnviarMensagem(NetworkStream fluxoDados)
        {
            objCliente = new Cliente(txb_nome.Text, txb_cpf.Text);

            string objJson = JsonSerializer.Serialize(objCliente);
            byte[] bufferDadosEnvio = Encoding.UTF8.GetBytes(objJson);

            // Envia os dados para o servidor
            fluxoDados.Write(bufferDadosEnvio, 0, bufferDadosEnvio.Length);
        }

        private void FormatarDadosRecebidos(Leilao objLeilao)
        {
            if (objLeilao == null)
            {
                EstadosDosCampos(true);
                AtualizarRodaPe("Erro ao desserializar dados do servidor!", true);
                return;
            }

            AtualizarRodaPe("Sucesso ao receber dados do servidor!");

            UserControl telaLeilao = new TelaLeilao(objLeilao, objCliente);

            telaLeilao.Dock = DockStyle.Fill;

            panel.Controls.Clear();

            panel.Controls.Add(telaLeilao);
        }

        private void EstadosDosCampos(bool habilitar)
        {
            foreach (var items in this.panel.Controls)
            {
                if (items.GetType() == typeof(TextBox))
                {
                    var textBox = (TextBox)items;
                    textBox.Enabled = habilitar;
                }
            }
        }

        public async void AtualizarRodaPe(string mensagem, bool ehErro = false)
        {
            this.label_status.ForeColor = ehErro == true ? Color.Red : Color.AliceBlue;

            this.label_status.Text = mensagem;

            await Task.Delay(3000);

            this.label_status.Text = "";
        }
    }
}
