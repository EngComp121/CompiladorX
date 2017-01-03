using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OOr : OLogico, IOperador
    {
        #region IOperador Members

        private string cadeia = "|#";

        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion


        public OOr()
        {

        }

        public OOr(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
