using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OSe : Operador, IOperador
    {
        #region IOperador Members

        private string cadeia = "se";

        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public OSe()
        {

        }

        public OSe(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
