using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using Leilao.ConsoleApp.Models;
using Leilao.ConsoleApp.ModuloCliente;

namespace Leilao.ConsoleApp.ModuloServidor
{
    public class ComunicacaoServidor
    {
        public static byte[] Tempo { get; private set; }
        private const string ip = "10.151.57.122";
        private const int portaLeilao = 52000;
        private const int portaTempo = 55000;
        private readonly TcpListener leilao;
        private readonly TcpListener leilaoTempo;

        public ComunicacaoServidor()
        {
            leilao = new TcpListener(IPAddress.Parse(ip), portaLeilao);
            leilaoTempo = new TcpListener(IPAddress.Parse(ip), portaTempo);

            leilao.Start();
            leilaoTempo.Start();

            Thread threadLerMensagem = new Thread(() => LerMensagemServidor());
            Thread threadLerTempo = new Thread(() => LerTempoServidor());

            threadLerTempo.Start();
            threadLerMensagem.Start();

            Console.WriteLine("Comunicação com o servidor inicializada...");
        }

        private void LerTempoServidor()
        {
            try
            {
                TcpClient cliente = leilaoTempo.AcceptTcpClient();

                Thread thread = new Thread(() => ProcessarTempoServidor(cliente));
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: \n" + ex.Message);
            }

            Program.Dispose(leilaoTempo);
        }

        private void ProcessarTempoServidor(TcpClient cliente)
        {
            try
            {
                NetworkStream fluxoDados = cliente.GetStream();

                while (cliente.Connected) 
                {
                    if (fluxoDados.DataAvailable) 
                    {
                        byte[] buffer = new byte[65507];
                        int qtdBytesRecebidos = fluxoDados.Read(buffer, 0, buffer.Length);

                        Tempo = new byte[qtdBytesRecebidos];
                        Array.Copy(buffer, Tempo, qtdBytesRecebidos);
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: \n" + ex.Message);
            }
        }

        public void LerMensagemServidor()
        {
            try
            {
                TcpClient cliente = leilao.AcceptTcpClient(); 
                Thread thread = new Thread(() => ProcessarNovoProdutoServidor(cliente));
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: \n" + ex.Message);
            }

            Program.Dispose(leilao);
        }

        private void ProcessarNovoProdutoServidor(TcpClient cliente)
        {
            try
            {
                NetworkStream fluxoDados = cliente.GetStream();

                while (cliente.Connected)
                {
                    if (fluxoDados.DataAvailable)
                    {
                        byte[] buffer = new byte[65507];
                        int qtdBytesRecebidos = fluxoDados.Read(buffer, 0, buffer.Length);

                        if (qtdBytesRecebidos == 0) break;

                        Console.WriteLine("\nProduto recebido com sucesso!");

                        Program.DadosProdutoCriptografado = new byte[qtdBytesRecebidos];
                        Array.Copy(buffer, Program.DadosProdutoCriptografado, qtdBytesRecebidos);
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: \n" + ex.Message);
            }
        }
    }
}
