using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OMenor : OComparacao, IOperador
    {
        #region IOperador Members

        private string cadeia = "<<";
        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public OMenor()
        {

        }

        public OMenor(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
