using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Leilao.WinApp.Models
{
    public class DadosRecebidos
    {
        public byte[] InfoLeilao { get; set; }
        public byte[] ChaveSimetricaEncriptada { get; set; }
        public byte[] IvObj { get; set; }

        public DadosRecebidos(byte[] infoLeilao, byte[] chaveSiemtricaEncriptada, byte[] iv)
        {
            InfoLeilao = infoLeilao;
            ChaveSimetricaEncriptada = chaveSiemtricaEncriptada;
            IvObj = iv;
        }
    }
}
