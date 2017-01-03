using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class Valor : Token
    {
        public Valor(string Nome, string Valor, string TipoValor)
        {
            this.nomeVariavel = Nome;
            this.valorVariavel = Valor;
            this.tipo = TipoValor;
        }

        public Valor(string Valor, string TipoValor, int NumeroLinha)
        {
            this.valorVariavel = Valor;
            this.tipo = TipoValor;
            this.Linha = NumeroLinha;
        }

        private string nomeVariavel;

        public string NomeVariavel
        {
            get
            {
                return nomeVariavel;
            }

            set
            {
                nomeVariavel = value;
            }
        }

        private string valorVariavel;

        public string ValorVariavel
        {
            get
            {
                return valorVariavel;
            }

            set
            {
                valorVariavel = value;
            }
        }

        private string tipo;

            public string Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
            }
        }

        public Valor Copia()
        {
            Valor vCopia = new Valor(this.ValorVariavel, this.Tipo, this.Linha);
            vCopia.NomeVariavel = this.NomeVariavel;
            
            return vCopia;
        }
    }
}
