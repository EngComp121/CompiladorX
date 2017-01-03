using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OEntao : Operador, IOperador
    {
        #region IOperador Members

        private string cadeia = "entao";

        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public OEntao()
        {

        }

        public OEntao(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
