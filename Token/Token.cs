using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    public abstract class Token
    {
        private int linha;

        public int Linha
        {
            get
            {
                return linha;
            }

            set
            {
                linha = value;
            }
        }

        private string texto;

        public string Texto
        {
            get
            {
                if (this is Valor)
                {
                    texto = ((Valor)this).NomeVariavel != null ? ((Valor)this).NomeVariavel : ((Valor)this).ValorVariavel.ToString();
                }
                else
                {
                    texto = ((Operador)this).Cadeia.Valor;
                }

                return texto;
            }
        }
    }
}
