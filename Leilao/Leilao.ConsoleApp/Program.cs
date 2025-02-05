using Leilao.ConsoleApp.Models;
using Leilao.ConsoleApp.ModuloCliente;
using Leilao.ConsoleApp.ModuloServidor;
using System.Net.Sockets;

namespace Leilao.ConsoleApp
{
    public class Program
    {
        public static byte[] DadosProdutoCriptografado { get; set; }
        public static byte[] Iv
        {
            get
            {
                return Convert.FromBase64String(File.ReadAllText("C:\\Users\\ruan.souza\\Documents\\Meus_Programas\\Leilao_Criptografado\\Leilao\\Leilao.ConsoleApp\\Criptografia\\Iv.txt"));
            }
        }
        public static byte[] ChaveSimetrica
        {
            get
            {
                return Convert.FromBase64String(File.ReadAllText("C:\\Users\\ruan.souza\\Documents\\Meus_Programas\\Leilao_Criptografado\\Leilao\\Leilao.ConsoleApp\\Criptografia\\ChaveSimetrica.txt"));
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Leilão inicializado...\n");

            ComunicacaoCliente comunicacaoCliente = new ComunicacaoCliente();
            ComunicacaoServidor comunicacaoServidor = new ComunicacaoServidor();
        }

        public static bool Dispose(TcpListener servidor)
        {
            EncerrarServidor(servidor);
            return false; 
        }

        public static void EncerrarServidor(TcpListener servidor)
        {
            // Encerra o servidor
            servidor?.Stop();
        }

    }
}
