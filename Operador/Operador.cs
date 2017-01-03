using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    public abstract class Operador : Token, IOperador
    {
        #region IOperador Members

        public abstract Cadeia Cadeia
        {
            get;
        }

        #endregion
    }
}
