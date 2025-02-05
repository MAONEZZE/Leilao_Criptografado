namespace Server_Leilao.WinApp.Models
{
    public class Produto
    {
        public byte[] Imagem { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public Produto(byte[] imagem, string descricao, decimal valor)
        {
            Imagem = imagem;
            Descricao = descricao;
            Valor = valor;
        }
    }
}
