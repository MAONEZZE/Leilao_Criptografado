namespace Client_Leilao.WinApp.Models
{
    public class ChatMsg
    {
        public int Lance { get; set; }
        public string Nome { get; set; }

        public ChatMsg(int lance, string nome)
        {
            Lance = lance;
            Nome = nome;
        }   
    }
}
