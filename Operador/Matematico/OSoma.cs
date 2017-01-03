using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OSoma : OMatematico, IOperador
    {
        #region IOperador Members

        private string cadeia = "+";

        public override Cadeia Cadeia
        {
            get 
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public OSoma()
        {

        }

        public OSoma(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
