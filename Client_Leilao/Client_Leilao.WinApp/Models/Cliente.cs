namespace Client_Leilao.WinApp.Models
{
    public class Cliente
    {
        //private string caminhoPublicKey = @"C:\Users\ruan.souza\Documents\Meus_Programas\Leilao_Criptografado\Client_Leilao\Client_Leilao.WinApp\Chaves\publicKey.base64";
        public string Nome { get; set; }
        public string Cpf {  get; set; }
        //public string PublicKey { get; set; }

        public Cliente(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
            //PublicKey = File.ReadAllText(caminhoPublicKey);
        }
    }
}
