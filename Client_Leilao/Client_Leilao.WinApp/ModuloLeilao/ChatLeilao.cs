using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Client_Leilao.WinApp.Models;

namespace Client_Leilao.WinApp.ModuloLeilao
{
    public class ChatLeilao
    {
        private const string multicastIP = "224.0.0.1";
        private UdpClient udpCliente;
        private Leilao objLeilao;
        private IPEndPoint grupoEPEnvio;

        public ChatLeilao(Leilao objLeilao)
        {
            this.objLeilao = objLeilao;

            udpCliente = new UdpClient();
            udpCliente.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpCliente.Client.Bind(new IPEndPoint(IPAddress.Any, objLeilao.PortaChat));
            udpCliente.JoinMulticastGroup(IPAddress.Parse(multicastIP));

            grupoEPEnvio = new IPEndPoint(IPAddress.Parse(multicastIP), objLeilao.PortaChat);

            Thread threadReceberMsgChat = new Thread(() => ReceberMensagens());
            threadReceberMsgChat.IsBackground = true;
            threadReceberMsgChat.Start();
        }

        public void EnviarMensagem(string jsonMensagem)
        {
            try
            {
                byte[] mensagemCriptografada = CriptografarMensagem(jsonMensagem);
                udpCliente.Send(mensagemCriptografada, mensagemCriptografada.Length, grupoEPEnvio);
            }
            catch (Exception ex)
            {
                Tela_Principal.telaPrincipal.AtualizarRodaPe("Erro: " + ex.Message, true);
            }
        }

        private void ReceberMensagens()
        {
            try
            {
                while (true)
                {
                    if (udpCliente.Available > 0)
                    {
                        IPEndPoint grupoEPRececbimento = new IPEndPoint(IPAddress.Parse(multicastIP), objLeilao.PortaChat);

                        byte[] dadosRecebidos = udpCliente.Receive(ref grupoEPRececbimento);
                        string mensagemRecebida = DecriptografarMensagem(dadosRecebidos);
                        TelaLeilao.telaLeilao.ApresentarDadosChat(mensagemRecebida);
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Tela_Principal.telaPrincipal.AtualizarRodaPe("Erro em receber mensagem do chat: " + ex.Message);
            }
        }

        private byte[] CriptografarMensagem(string jsonMensagem)
        {
            Aes aes = Aes.Create();
            aes.Key = Tela_Principal.telaPrincipal.ChaveSimetrica;
            aes.IV = Tela_Principal.telaPrincipal.Iv;

            ICryptoTransform encryptor = aes.CreateEncryptor();
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                byte[] dados = Encoding.UTF8.GetBytes(jsonMensagem);
                cs.Write(dados, 0, dados.Length);
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
        }

        private string DecriptografarMensagem(byte[] dadosCriptografados)
        {
            try
            {
                Aes aes = Aes.Create();
                aes.Key = Tela_Principal.telaPrincipal.ChaveSimetrica;
                aes.IV = Tela_Principal.telaPrincipal.Iv;

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
    }
}
