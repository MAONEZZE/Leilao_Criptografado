namespace Client_Leilao.WinApp.Models
{
    public class Leilao
    {
        public string Ip { get; set; }
        public int Porta { get; set; }
        public int PortaChat { get; set; }
        public int PortaTempo { get; set; }
        public byte[] ChaveSimetrica { get; set; }
        public byte[] Iv { get; set; }

        public Leilao(string ip, int port, int portaChat, int portaTempo, byte[] chaveCripto, byte[] iv)
        {
            Ip = ip;
            Porta = port;
            PortaChat = portaChat;
            PortaTempo = portaTempo;
            ChaveSimetrica = chaveCripto;
            Iv = iv;
        }
    }
}
