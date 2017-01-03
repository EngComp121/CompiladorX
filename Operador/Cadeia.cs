using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    public class Cadeia
    {
        private String ValorRetorno;

        public Cadeia(String valor)
        {
            ValorRetorno = valor;
        }

        public String Valor
        {
            get
            {
                return ValorRetorno;
            }

            set
            {
                ValorRetorno = value;
            }
        }
    }
}
