﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class OIgual : OComparacao, IOperador
    {
         #region IOperador Members

        private string cadeia = "==";
        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public OIgual()
        {

        }

        public OIgual(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
