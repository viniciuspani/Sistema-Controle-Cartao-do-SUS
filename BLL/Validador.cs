using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Cartao_Sus.BLL
{
    class Validador
    {
        public static bool ETexto(string valor)
        {
            char[] caracteres = valor.ToCharArray();
            bool result = false;

            foreach (var caracter in caracteres)
            {
                if (char.IsDigit(caracter))
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
