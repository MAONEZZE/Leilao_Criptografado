using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Client_Leilao.WinApp.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Client_Leilao.WinApp.ModuloLeilao
{
    public partial class TelaLeilao : UserControl
    {
        public static TelaLeilao telaLeilao;
        private ChatLeilao chatLeilao;
        private Cliente objClient;
        private Thread threadRecebimentoProduto;
        private Thread threadRecebimentoTempo;
        private TcpClient clienteTempo;
        private NetworkStream fluxoDadosTempo;

        public TelaLeilao(Leilao objLeilao, Cliente objClient)
        {
            InitializeComponent();

            telaLeilao = this;

            chatLeilao = new ChatLeilao(objLeilao);

            this.objClient = objClient;

            threadRecebimentoProduto = new Thread(() => ApresentarDadosProduto(objLeilao));
            threadRecebimentoTempo = new Thread(() => IniciarReqTempo(objLeilao));

            threadRecebimentoTempo.IsBackground = true;

            threadRecebimentoProduto.Start();
            threadRecebimentoTempo.Start();
        }

        public void ApresentarDadosChat(string msgRecebida)
        {
            try
            {
                if (msgRecebida != null)
                {
                    ChatMsg msg = JsonSerializer.Deserialize<ChatMsg>(msgRecebida);

                    if (lbx_chat.InvokeRequired && msg != null)
                    {
                        lbx_chat.Invoke(new Action(() =>
                        {
                            lbx_chat.Items.Add(msg.Nome + " - Lance: " + msg.Lance);
                            lbx_chat.Items.Add("");
                        }));
                    }
                    else if(msg != null)
                    {
                        lbx_chat.Items.Add(msg.Nome + " - Lance: " + msg.Lance);
                        lbx_chat.Items.Add("");
                    }
                }

                Thread.Sleep(1000);
                
            }
            catch (Exception ex)
            {
                Tela_Principal.telaPrincipal.AtualizarRodaPe("Erro: " + ex.Message, true);
            }
        }

        private void ApresentarDadosProduto(Leilao objLeilao)
        {
            TcpClient cliente = new TcpClient(objLeilao.Ip, objLeilao.Porta);
            NetworkStream fluxoDados = cliente.GetStream();

            try
            {
                byte[] bufferDadosRecebidos = new byte[65507];
                int qtdBytesRecebidos = fluxoDados.Read(bufferDadosRecebidos, 0, bufferDadosRecebidos.Length);

                byte[] bytesRecebidos = new byte[qtdBytesRecebidos];
                Array.Copy(bufferDadosRecebidos, bytesRecebidos, qtdBytesRecebidos);

                string dadosDecriptografados = DecriptografiaSimetrica(bytesRecebidos);

                JsonElement root = Tela_Principal.telaPrincipal.LerJSON(dadosDecriptografados);

                var imagem = Convert.FromBase64String(root.GetProperty("Imagem").GetString());
                var descricao = root.GetProperty("Descricao").GetString();
                var valor = root.GetProperty("Valor").GetInt32();

                Produto produto = new Produto(imagem, descricao, valor);

                AtualizarComponentesGUI(produto);
            }
            catch (Exception ex)
            {
                Tela_Principal.telaPrincipal.AtualizarRodaPe("Erro: " + ex.Message, true);
            }

            fluxoDados.Close();
            cliente.Close();
        }

        private void AtualizarComponentesGUI(Produto produto)
        {
            if (lbx_infoProduto.InvokeRequired)
            {
                lbx_infoProduto.Invoke(new Action(() =>
                {
                    lbx_infoProduto.Items.Add("Descricao:");
                    lbx_infoProduto.Items.Add(produto.Descricao);
                    lbx_infoProduto.Items.Add("");
                    lbx_infoProduto.Items.Add("Valor:");
                    lbx_infoProduto.Items.Add("R$ " + produto.Valor);
                }));
            }
            else
            {
                lbx_infoProduto.Items.Add("Descricao:");
                lbx_infoProduto.Items.Add(produto.Descricao);
                lbx_infoProduto.Items.Add("");
                lbx_infoProduto.Items.Add("Valor:");
                lbx_infoProduto.Items.Add("R$ " + produto.Valor);
            }

            if (pcbox_produto.InvokeRequired)
            {
                pcbox_produto.Invoke(new Action(() =>
                {
                    pcbox_produto.Image = Image.FromStream(new MemoryStream(produto.Imagem));
                }));
            }
            else
            {
                pcbox_produto.Image = Image.FromStream(new MemoryStream(produto.Imagem));
            }
        }

        private void IniciarReqTempo(Leilao objLeilao)
        {
            clienteTempo = new TcpClient(objLeilao.Ip, objLeilao.PortaTempo);
            fluxoDadosTempo = clienteTempo.GetStream();

            try
            {
                while (true)
                {
                    if (fluxoDadosTempo.DataAvailable)
                    {
                        byte[] bufferDadosRecebidos = new byte[65507];
                        int qtdBytesRecebidos = fluxoDadosTempo.Read(bufferDadosRecebidos, 0, bufferDadosRecebidos.Length);

                        byte[] byteTempoRecebido = new byte[qtdBytesRecebidos];
                        Array.Copy(bufferDadosRecebidos, byteTempoRecebido, qtdBytesRecebidos);

                        string tempoRecebido = Encoding.UTF8.GetString(byteTempoRecebido);

                        if (lbl_tempo.InvokeRequired)
                        {
                            lbl_tempo.Invoke(new Action(() =>
                            {
                                lbl_tempo.Text = "";
                                lbl_tempo.Text = tempoRecebido.Trim('"');
                            }));
                        }
                        else
                        {
                            lbl_tempo.Text = tempoRecebido;
                        }

                        if (tempoRecebido.Trim('"').Equals("00:00:00"))
                        {
                            btn_confirmar.Invoke(new Action(() =>
                            {
                                btn_confirmar.Enabled = false;
                            }));

                            txb_valor.Invoke(new Action(() =>
                            {
                                txb_valor.Enabled = false;
                            }));

                            clienteTempo.Close();
                            fluxoDadosTempo.Close();
                            break;
                        }
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Tela_Principal.telaPrincipal.AtualizarRodaPe("Erro: " + ex.Message, true);
                fluxoDadosTempo.Close();
                clienteTempo.Close();
            }
        }

        private string DecriptografiaSimetrica(byte[] bytesRecebidos)
        {
            try
            {
                Aes aes = Aes.Create();
                aes.Key = Tela_Principal.telaPrincipal.ChaveSimetrica;
                aes.IV = Tela_Principal.telaPrincipal.Iv;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                using (MemoryStream ms = new MemoryStream(bytesRecebidos))
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

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            lbl_ultimoLance.Text = txb_valor.Text;

            var jsonMensagem = JsonSerializer.Serialize(new ChatMsg(int.Parse(txb_valor.Text), objClient.Nome));
            chatLeilao.EnviarMensagem(jsonMensagem);

            txb_valor.Text = "";
        }

        private void btn_sair_Click(object sender, EventArgs e)
        {
            try
            {
                if (clienteTempo != null)
                {
                    if (clienteTempo.Connected)
                    {
                        fluxoDadosTempo.Close(); // Fecha o fluxo de dados
                        clienteTempo.Close(); // Fecha a conexão
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao fechar a conexão: " + ex.Message);
            }
        }
    }

    
}
