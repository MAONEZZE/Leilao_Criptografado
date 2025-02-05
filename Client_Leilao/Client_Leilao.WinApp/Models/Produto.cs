namespace Client_Leilao.WinApp.Models
{
    public class Produto
    {
        public byte[] Imagem { get; set; }
        public string Descricao { get; set; }
        public int Valor { get; set; }

        public Produto(byte[] imagem, string descricao, int valor)
        {
            Imagem = imagem;
            Descricao = descricao;
            Valor = valor;
        }

        private string[] FormatadorDescricao(string descricao)
        {
            List<string> list = new List<string>();
            List<char> listAux = descricao.ToList();
            string descricaoFormatada = "";

            if (descricao.Length > 29)
            {
                int i = 0;
                foreach (char caractere in descricao)
                {
                    descricaoFormatada += caractere;

                    if (i == 27)
                    {
                        list.Add(descricaoFormatada);
                        descricaoFormatada = "";
                        i = 0;
                    }
                    else if (listAux.Count == 1)
                    {
                        list.Add(descricaoFormatada);
                    }

                    i++;

                    if (listAux.Count > 0)
                    {
                        listAux.RemoveAt(0);
                    }
                }
            }

            return list.ToArray();
        }
    }
}
