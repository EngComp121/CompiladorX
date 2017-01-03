using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX

{
    public class CodigoIntermediario
    {
        List<ExpressaoCodigoIntermediario> codigo = new List<ExpressaoCodigoIntermediario>();
        public List<ExpressaoCodigoIntermediario> Codigo
        {
            get
            {
                return codigo;
            }
        }

        private string mensagens;
        public string Mensagens
        {
            get
            {
                return mensagens;
            }
            set
            {
                mensagens = value;
            }
        }

        public CodigoIntermediario()
        {
        }

        public CodigoIntermediario(List<ExpressaoCodigoIntermediario> codigo)
        {
            codigo = Codigo;
        }

        public void AdicionarExpressao(ExpressaoCodigoIntermediario Expressao)
        {
            codigo.Add(Expressao);
        }
    }
}
