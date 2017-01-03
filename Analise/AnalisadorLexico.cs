using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    class Analisador_Lexico
    {
        private string Espaco = "°";

        private string mensagemerro;

        public string MensagemErro
        {
            get
            {
                return mensagemerro;
            }

            set
            {
                mensagemerro = value;
            }
        }

        private Variaveis var = null;

        public Variaveis Variaveis
        {
            get
            {
                return var;
            }
        }

        private List<Token> codigofonte;

        public List<Token> CodigoFonte
        {
            get
            {
                if(codigofonte == null)
                {
                    codigofonte = new List<Token>();
                }

                return codigofonte;
            }
        }

        public bool Validar(string Codigo, List<Valor> ListaVariaveis)
        {
            bool retorno = true;
            //
            //
            string codigoRemontado = "";
            bool dentroString = false;

            for (int pos = 0; pos < Codigo.Length; pos++)
            {
                char letra = Codigo[pos];
                char letraAnterior = new char();
                char proximaLetra = new char();

                if(pos > 0)
                {
                    letraAnterior = Codigo[pos - 1];
                }

                if(pos < Codigo.Length - 1)
                {
                    proximaLetra = Codigo[pos + 1];
                }

                if(letra == '"')
                {
                    if (!dentroString)
                    {
                        dentroString = true;
                    }
                    else
                    {
                        dentroString = false;
                    }
                }

                if (letra == '\n' && dentroString)
                {
                    break;
                }
                else if (dentroString)
                {
                    codigoRemontado += letra.ToString();
                }

                else if (letra == ' ')
                {
                    if (letraAnterior != ' ' &&
                       letraAnterior != '\n' &&
                       proximaLetra != '\n')
                    {
                        codigoRemontado += Espaco;
                    }
                    else
                    {
                        continue;
                    }

                }
                else
                {
                    codigoRemontado += letra.ToString();
                }
            }

            if(dentroString)
            {
                this.mensagemerro = "Sequencia de String de valores não fechada";
                return false;
            }

            Codigo = codigoRemontado;

            //
            Codigo = Codigo.Replace("\n", Espaco + "\n" + Espaco);

            string[] Tokens = Codigo.Split(Convert.ToChar(Espaco));
            int Linha = 1;

            for (int pos = 0; pos < Tokens.Length; pos++)
            {
                String valor = Tokens[pos] != "\n" ? Tokens[pos].Trim() : Tokens[pos];

                //
                if(valor == "\n")
                {
                    Linha++;
                    continue;
                }

                else if(valor == "")
                {
                    continue;
                }

                var = new Variaveis(ListaVariaveis);
                Int64 numeroConvertidos = 0;

                //SE FOR STRING
                if(valor[0] == '"')
                {
                    codigofonte.Add(new Valor(valor, Tipos.Txt, Linha));
                }

                //SE FOR UM NUMERO
                else if(Int64.TryParse(valor, out numeroConvertidos))
                {
                    codigofonte.Add(new Valor(numeroConvertidos.ToString(), Tipos.Dec, Linha));
                }

                //SE FOR UM NOME DE VARIAVEL
                else if(var.ExisteVariavel(valor))
                {
                    Valor variavel = var.getVariavel(valor).Copia();
                    variavel.Linha = Linha;
                    codigofonte.Add(variavel);
                }

                //SE É UM IF
                else if(new OSe().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OSe(Linha));
                }

                //SE É UM THEN
                else if(new OEntao().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OEntao(Linha));
                }

                //SE É UM ELSE
                else if(new OSenao().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OSenao(Linha));
                }

                //SE É UM END IF
                else if(new OFimSe().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OFimSe(Linha));
                }

                //SE É UM IGUAL
                else if(new OIgual().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OIgual(Linha));
                }

                //SE É UM DIFERENTE
                else if(new ODiferente().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new ODiferente(Linha));
                }

                //SE É UM MAIOR
                else if(new OMaior().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OMaior(Linha));
                }

                //SE É UM MENOR
                else if (new OMenor().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OMenor(Linha));
                }

                //SE É UM MAIOR OU IGUAL
                else if (new OMaiorIgual().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OMaiorIgual(Linha));
                }

                //SE É UM MENOR OU IGUAL
                else if (new OMenorIgual().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OMenorIgual(Linha));
                }

                //SE É UMA SOMA
                else if (new OSoma().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OSoma(Linha));
                }

                //SE É UMA SUBTRAÇÃO
                else if (new OSubtracao().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OSubtracao(Linha));
                }

                //SE É UMA MULTIPLICAÇÃO
                else if (new OMultiplicacao().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OMultiplicacao(Linha));
                }

                //SE É UMA DIVISAO
                else if (new ODivisao().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new ODivisao(Linha));
                }

                //SE É UM OR
                else if (new OOr().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OOr(Linha));
                }

                //SE É UM AND
                else if (new OAnd().Cadeia.Valor == valor)
                {
                    codigofonte.Add(new OAnd(Linha));
                }

                //SE É UM VAZIO PULA
                else if (valor == "")
                {
                    continue;
                }
                else
                {
                    this.mensagemerro = "Simbolo " + valor + " Nao reconhecido na linha " + Linha + ".";
                    retorno = false;
                    break;
                }
            }

            return retorno;
        }
    }
}
