using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OSubtracao : OMatematico, IOperador
    {
        #region IOperador Members

        private string cadeia = "-";

        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public OSubtracao()
        {

        }

        public OSubtracao(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
