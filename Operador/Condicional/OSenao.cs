using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OSenao : Operador, IOperador
    {
        #region IOperador Members

        private string cadeia = "senao";

        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public OSenao()
        {

        }

        public OSenao(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
