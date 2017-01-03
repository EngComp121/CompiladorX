using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OMultiplicacao : OMatematico, IOperador
    {
        #region IOperador Members

        private string cadeia = "*";

        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public OMultiplicacao()
        {

        }

        public OMultiplicacao(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
