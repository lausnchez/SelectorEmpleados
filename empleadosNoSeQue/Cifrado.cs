using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace empleadosNoSeQue
{
    internal class Cifrado
    {
        public String cifrar(String cadena)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(cadena));
        }

        public String descrifrar(String cadena)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(cadena));
        }
    }
}
