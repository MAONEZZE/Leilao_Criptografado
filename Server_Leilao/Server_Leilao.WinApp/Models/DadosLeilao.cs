namespace Server_Leilao.WinApp.Models
{
    public class DadosLeilao
    {
        public string Ip { get; set; }
        public int Porta { get; set; }
        public int PortaChat { get; set; }
        public int PortaTempo { get; set; }

        public DadosLeilao(string ip, int porta, int portaChat, int portaTempo)
        {
            Ip = ip;
            Porta = porta;
            PortaChat = portaChat;
            PortaTempo = portaTempo;
        }
    }
}
