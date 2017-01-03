using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OMaior : OComparacao, IOperador
    {
        #region IOperador Members

        private string cadeia = ">>";
        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public OMaior()
        {

        }

        public OMaior(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
