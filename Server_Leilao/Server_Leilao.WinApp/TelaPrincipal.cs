using Server_Leilao.WinApp.ModuloCliente;
using Server_Leilao.WinApp.ModuloEnvioLeilao;

namespace Server_Leilao.WinApp
{
    public partial class TelaPrincipal : Form
    {
        public static byte[] Iv
        {
            get
            {
                return Convert.FromBase64String(File.ReadAllText("C:\\Users\\ruan.souza\\Documents\\Meus_Programas\\Leilao_Criptografado\\Server_Leilao\\Server_Leilao.WinApp\\Criptografia\\Iv.txt"));
            }
        }
        public static byte[] ChaveSimetrica
        {
            get
            {
                return Convert.FromBase64String(File.ReadAllText("C:\\Users\\ruan.souza\\Documents\\Meus_Programas\\Leilao_Criptografado\\Server_Leilao\\Server_Leilao.WinApp\\Criptografia\\ChaveSimetrica.txt"));
            }
        }

        public static TelaPrincipal telaPrincipal;
        private ComunicacaoCliente comunicacaoCliente;

        public TelaPrincipal()
        {
            InitializeComponent();

            telaPrincipal = this;

            this.label_status.Text = "";

            comunicacaoCliente = new ComunicacaoCliente();
        }

        private void btn_entrar_Click(object sender, EventArgs e)
        {
            foreach (var items in this.groupBox.Controls)
            {
                if (items.GetType() == typeof(TextBox))
                {
                    var textBox = (TextBox)items;
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        AtualizarRodaPe("Preencha todos os campos!", true);
                        return;
                    }
                }
            }

            ComunicacaoLeila telaLeilao = new ComunicacaoLeila(this.txb_ip.Text, this.txb_porta.Text, telaPrincipal);

            telaLeilao.Dock = DockStyle.Fill;

            panel.Controls.Clear();

            panel.Controls.Add(telaLeilao);
        }

        public async void AtualizarRodaPe(string mensagem, bool ehErro = false)
        {

            this.label_status.ForeColor = ehErro == true? Color.Red : Color.AliceBlue;
            
            this.label_status.Text = mensagem;

            await Task.Delay(3000);

            this.label_status.Text = "";
        }

        private void TelaPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            comunicacaoCliente.Dispose();
        }
    }
}
