namespace Server_Leilao.WinApp.Models
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string PublicKey { get; set; }

        public Cliente(string nome, string cpf, string publicKey)
        {
            Nome = nome;
            Cpf = cpf;
            PublicKey = publicKey;
        }
    }
}