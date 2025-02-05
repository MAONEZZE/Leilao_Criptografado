using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leilao.ConsoleApp.Models
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
