﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class ODivisao : OMatematico, IOperador
    {
        #region IOperador Members

        private string cadeia = "/";

        public override Cadeia Cadeia
        {
            get
            {
                return new Cadeia(cadeia);
            }
        }

        #endregion

        public ODivisao()
        {

        }

        public ODivisao(int NumeroLinha)
        {
            this.Linha = NumeroLinha;
        }
    }
}
