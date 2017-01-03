using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class ODiferente : OComparacao, IOperador
    {
        #region IOperador Members

        private string cadeia = "><";
        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public ODiferente()
        {

        }

        public ODiferente(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
