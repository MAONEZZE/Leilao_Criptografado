using Server_Leilao.WinApp.Models;
using System.Drawing.Imaging;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Server_Leilao.WinApp.ModuloEnvioLeilao
{
    public partial class ComunicacaoLeila : UserControl
    {
        private readonly TelaPrincipal telaPrincipal;
        private readonly string ip;
        private readonly int portaLeilao;

        private const int portaTempo = 55000;

        private Image imagemProduto;

        public ComunicacaoLeila(string ip, string porta, TelaPrincipal tela)
        {
            InitializeComponent();

            this.ip = ip;
            this.portaLeilao = Convert.ToInt32(porta);
            this.telaPrincipal = tela;

            txb_descricao.Text = "Gato Laranja da raça miau";
            txb_valor.Text = "1000";
            txb_tempo.Text = "1";
        }

        private byte[] CriptografarMensagem(Produto objProduto)
        {
            Aes aes = Aes.Create();
            var ms = new MemoryStream();

            aes.Key = TelaPrincipal.ChaveSimetrica;
            aes.IV = TelaPrincipal.Iv;

            ICryptoTransform encryptor = aes.CreateEncryptor();

            var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

            var objJson = JsonSerializer.Serialize(objProduto);
            byte[] dados = Encoding.UTF8.GetBytes(objJson);

            cs.Write(dados, 0, dados.Length);
            cs.FlushFinalBlock();

            return ms.ToArray();
        }

        private async void FinalizarCadastroProduto(TcpClient client)
        {
            Thread thread = new Thread(() => IniciarTemporizador());
            thread.Start();

            client.Close();

            TelaPrincipal.telaPrincipal.AtualizarRodaPe("Produto cadastrado e enviado ao leilão com sucesso!");

            this.txb_caminhoImg.Text = "";
            this.txb_descricao.Text = "";
            this.txb_valor.Text = "";
            this.txb_tempo.Text = "";   
            this.pcbox_produto.Image = null;
        }

        private void IniciarTemporizador()
        {
            try
            {
                TimeSpan tempo = TimeSpan.FromMinutes(Convert.ToInt32(txb_tempo.Text));
                TcpClient client = new TcpClient(ip, portaTempo);
                NetworkStream fluxoDados = client.GetStream();

                while(tempo.TotalHours >= 0 && tempo.TotalMinutes >= 0 && tempo.TotalSeconds >= 0)
                {
                    string json = JsonSerializer.Serialize(tempo);
                    byte[] objEnvio = Encoding.UTF8.GetBytes(json);

                    fluxoDados.Write(objEnvio);

                    Thread.Sleep(1000);

                    tempo = tempo.Add(TimeSpan.FromSeconds(-1));
                }
            }
            catch (Exception ex)
            {
                telaPrincipal.AtualizarRodaPe($"ERRO: {ex.Message}", true);
            }
        }

        private void btn_enviar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Cria uma conexão com o servidor
                var client = new TcpClient(ip, portaLeilao);
                var fluxoDados = client.GetStream();

                byte[] imagemBytes;

                using (var ms = new MemoryStream())
                {
                    // Salva a imagem no MemoryStream em um formato específico (por exemplo, JPEG)
                    imagemProduto.Save(ms, ImageFormat.Jpeg);
                    imagemBytes = ms.ToArray();
                }

                Produto objProduto = new Produto(imagemBytes, this.txb_descricao.Text, Convert.ToDecimal(this.txb_valor.Text));

                byte[] objEnvio = CriptografarMensagem(objProduto);

                // Envia os dados para o servidor
                fluxoDados.Write(objEnvio, 0, objEnvio.Length);

                FinalizarCadastroProduto(client);
            }
            catch (Exception ex)
            {
                telaPrincipal.AtualizarRodaPe($"ERRO: {ex.Message}", true);
            }
        }

        private void btn_selecionar_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Selecione a imagem do produto";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "Arquivos de imagem (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                txb_caminhoImg.Text = openFileDialog.FileName;

                imagemProduto = Image.FromFile(openFileDialog.FileName);

                pcbox_produto.Image = imagemProduto;

                pcbox_produto.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }
}
