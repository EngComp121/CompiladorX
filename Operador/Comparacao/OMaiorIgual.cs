using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
        class OMaiorIgual : OComparacao, IOperador
        {
            #region IOperador Members

            private string cadeia = ">=";
            public override Cadeia Cadeia
            {
                get
                {
                    return new Cadeia(cadeia);
                }
            }

            #endregion

            public OMaiorIgual()
            {

            }

            public OMaiorIgual(int NumeroLinha)
            {
                this.Linha = NumeroLinha;
            }
        }
 }

