using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OAnd : OLogico, IOperador
    {
        #region IOperador Members

        private string cadeia = "&#";

        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion


        public OAnd()
        {

        }

        public OAnd(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
