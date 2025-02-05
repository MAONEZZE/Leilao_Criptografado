using System.Net.Sockets;
using System.Net;
using Leilao.ConsoleApp.ModuloServidor;
using System.Text;
using System.Security.Cryptography;
using System.Text.Json;
using Leilao.ConsoleApp.Models;

namespace Leilao.ConsoleApp.ModuloCliente
{
    public class ComunicacaoCliente
    {
        public static ComunicacaoCliente comunicacaoCliente;

        private const string ip = "127.0.0.1";
        private const string multicastIP = "224.0.0.1";

        private const int portaLeilao = 53000;
        private const int portaChat = 54000;
        private const int portaTempo = 56000;

        private readonly TcpListener leilao;
        private readonly TcpListener leilaoTempo;
        private readonly UdpClient udpCliente;

        private int maiorLance = 0;
        private int qtdClientesConectados = 0;

        private bool isRunning;
        private bool clienteConectado;

        private ChatMsg ganhador;

        public ComunicacaoCliente()
        {
            udpCliente = new UdpClient();
            udpCliente.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpCliente.Client.Bind(new IPEndPoint(IPAddress.Any, portaChat));
            udpCliente.JoinMulticastGroup(IPAddress.Parse(multicastIP));

            leilao = new TcpListener(IPAddress.Parse(ip), portaLeilao);
            leilaoTempo = new TcpListener(IPAddress.Parse(ip), portaTempo);

            leilao.Start();
            leilaoTempo.Start();

            isRunning = true;

            Thread threadLerMensagem = new Thread(() => LerMensagem());
            Thread threadLerMsgChat = new Thread(() => ReceberMensagensChat());

            threadLerMensagem.Start();
            threadLerMsgChat.Start();

            comunicacaoCliente = this;
            Console.WriteLine("Comunicação com o cliente inicializada...");
        }

        private void AvaliarMaiorLance(string mensagemRecebidaChat)
        {
            ChatMsg chatMsg = JsonSerializer.Deserialize<ChatMsg>(mensagemRecebidaChat);

            if (chatMsg.Lance > maiorLance)
            {
                maiorLance = chatMsg.Lance;
                ganhador = chatMsg;
                Console.WriteLine($"\nMaior lance: {maiorLance} - Ganhador: {ganhador.Nome}");
            }
        }

        private void ReceberMensagensChat()
        {
            try
            {
                while (true)
                {
                    IPEndPoint grupoEP = new IPEndPoint(IPAddress.Any, portaChat);
                    byte[] dadosRecebidos = udpCliente.Receive(ref grupoEP);
                    string mensagemRecebidaChat = DecriptografiaSimetrica(dadosRecebidos);

                    if (mensagemRecebidaChat.Contains("Erro"))
                    {
                        throw new Exception("Não foi possível na decriptografar a mensagem do chat");
                    }

                    AvaliarMaiorLance(mensagemRecebidaChat);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fechando receptor...");
                Console.WriteLine("\nErro: \n" + ex.Message);
            }
        }

        private string DecriptografiaSimetrica(byte[] dadosCriptografados)
        {
            try
            {
                Aes aes = Aes.Create();
                aes.Key = Program.ChaveSimetrica;
                aes.IV = Program.Iv;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                using (MemoryStream ms = new MemoryStream(dadosCriptografados))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return "Erro na decriptação: " + ex.Message;
            }
        }

        //private void LerChat()
        //{
        //    try
        //    {
        //        while (isRunning)
        //        {
        //            if (leilaoChat.Pending())
        //            {
        //                TcpClient cliente = leilaoChat.AcceptTcpClient();

        //                Thread thread = new Thread(() => EnviarChatCliente(cliente));
        //                thread.Start();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Erro: \n" + ex.Message);
        //    }
        //}

        //private void EnviarChatCliente(TcpClient cliente)
        //{
        //    try
        //    {
        //        NetworkStream fluxoDados = cliente.GetStream();

        //        while (true)
        //        {
        //            if (fluxoDados.DataAvailable)
        //            {
        //                byte[] bufferDadosRecebidos = new byte[65507];
        //                int qtdBytesRecebidos = fluxoDados.Read(bufferDadosRecebidos, 0, bufferDadosRecebidos.Length);

        //                // Redimensiona para o tamanho exato
        //                byte[] bytesRecebidos = new byte[qtdBytesRecebidos];
        //                Array.Copy(bufferDadosRecebidos, bytesRecebidos, qtdBytesRecebidos);

        //                fluxoDados.Write(bytesRecebidos, 0, bytesRecebidos.Length);
        //            }
        //            Thread.Sleep(1000);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Erro: \n" + ex.Message);
        //    }
        //}

        private void LerMensagem()
        {
            try
            {
                while (isRunning)
                {
                    if (leilao.Pending())
                    {
                        qtdClientesConectados++;

                        TcpClient client = leilao.AcceptTcpClient();
                        Console.WriteLine($"\nCliente - {qtdClientesConectados} conectado");

                        Thread thread = new Thread(() => EnviarProdutoClientes(client));
                        Thread threadEnviarTempo = new Thread(() => ProcessarTempoClientes());

                        thread.Start();
                        threadEnviarTempo.Start();
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro em enviar produto ao cliente: \n" + ex.Message);
            }

            isRunning = Program.Dispose(leilao);
        }

        private void EnviarProdutoClientes(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            stream.Write(Program.DadosProdutoCriptografado, 0, Program.DadosProdutoCriptografado.Length);
            Console.WriteLine($"\nEnviando produto ao cliente...");

            stream.Close();
            client.Close();
            Console.WriteLine($"Produto enviado");
        }

        private void ProcessarTempoClientes()
        {
            try
            {
                while (isRunning)
                {
                    if(leilaoTempo.Pending())
                    {
                        TcpClient cliente = leilaoTempo.AcceptTcpClient();
                        Thread thread = new Thread(() => EnviarTempo(cliente));
                        thread.Start();
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                qtdClientesConectados--;
            }
        }

        private void EnviarTempo(TcpClient cliente)
        {
            NetworkStream fluxoDados = cliente.GetStream();
            clienteConectado = true;

            try
            {
                while (clienteConectado)
                {
                    string tempoStr = Encoding.UTF8.GetString(ComunicacaoServidor.Tempo).Trim('"');
                    TimeSpan tempo = TimeSpan.Parse(tempoStr);

                    if (cliente.Connected)
                    {
                        if (tempo.TotalHours >= 0 && tempo.TotalMinutes >= 0 && tempo.TotalSeconds >= 0)
                        {
                            fluxoDados.Write(ComunicacaoServidor.Tempo, 0, ComunicacaoServidor.Tempo.Length);
                        }
                        else
                        {
                            EnviarGanhadorEncerrarConexao(fluxoDados);
                        }
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                DesconectarCliente(cliente, fluxoDados);
            }
        }

        private void DesconectarCliente(TcpClient cliente, NetworkStream fluxoDados)
        {
            Console.WriteLine($"\nCliente desconectado...");

            clienteConectado = false;
            qtdClientesConectados--;

            cliente.Close();
            fluxoDados.Close();
        }

        private void EnviarGanhadorEncerrarConexao(NetworkStream fluxoDados)
        {
            string ganhadorJson = JsonSerializer.Serialize(ganhador);
            byte[] bytesGanhadorJson = Encoding.UTF8.GetBytes(ganhadorJson);

            fluxoDados.Write(bytesGanhadorJson, 0, bytesGanhadorJson.Length);

            Console.WriteLine("\nEnviando ganhador ao cliente e encerrando o servidor...");
            leilao.Stop();
            leilaoTempo.Stop();
        }
    }
}
