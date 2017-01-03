using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OFimSe : Operador, IOperador
    {
        #region IOperador Members

        private string cadeia = "fimse";

        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public OFimSe()
        {

        }

        public OFimSe(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
