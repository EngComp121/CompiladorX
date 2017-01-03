using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CompiladorX
{
    class AnalisadorSintatico
    {
        private string mensagens;

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


        Analisador_Lexico analise;
        public Analisador_Lexico AnaliseLexica
        {
            get
            {
                return analise;
            }
        }

        private string ExpressaoRegularCadeia()
        {
            return @"(\" + '"'.ToString() + @"(\w|\.|\:|\-|\+|\*|\&|\(|\)|\%|\$|\#|\@|\!|\?|\<|\>|\;|\)*\" + '"'.ToString() + @" )|\w+"; 
        }

        private string ExpressaoRegularPermitePontoEmVariavel()
        {
            return @"(\.\w+)*";
        }

        /*
         * 
         */
        private string ExpressaoRegularOperadoresComparacao()
        {
            StringBuilder sb = new StringBuilder();

            //OPERADOR IGUAL
            sb.Append(@"(\");
            sb.Append(new OIgual().Cadeia.Valor);

            sb.Append(@"|");

            //OPERADOR DIFERENTE
            sb.Append(@"(\");
            sb.Append(new ODiferente().Cadeia.Valor);

            sb.Append(@"|");

            //OPERADOR MAIOR
            sb.Append(@"(\");
            sb.Append(new OMaior().Cadeia.Valor);

            sb.Append(@"|");

            //OPERADOR MENOR
            sb.Append(@"(\");
            sb.Append(new OMenor().Cadeia.Valor);

            sb.Append(@"|");

            //OPERADOR MAIOR OU IGUAL
            sb.Append(@"(\");
            sb.Append(new OMaiorIgual().Cadeia.Valor);

            sb.Append(@"|");

            //OPERADOR MENOR OU IGUAL
            sb.Append(@"(\");
            sb.Append(new OMenorIgual().Cadeia.Valor);

            sb.Append(@"|");

            sb.Append(@")");


            return sb.ToString();
        }

        private string ExpressaoRegularOperadoresLogicos()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"(");

            //OPERADORES AND
            sb.Append(new OAnd().Cadeia.Valor);

            sb.Append(@"|");

            //OPERADORES OR
            sb.Append(new OOr().Cadeia.Valor);

            sb.Append(@")");

            return sb.ToString();
        }

        /*
         * 
         */
        private string ExpressaoRegularOperadoresMatematicos(bool ComEspacoFinal)
        {
            StringBuilder sb = new StringBuilder();

            if (ComEspacoFinal)
            {
                sb.Append(@"((");
            }
            else
            {
                sb.Append(@"(\s(");
            }

            //OPERADOR SOMA
            sb.Append(@"\");
            sb.Append(new OSoma().Cadeia.Valor);

            sb.Append(@"|");

            //OPERADOR SUBTRAÇÃO
            sb.Append(@"\");
            sb.Append(new OSubtracao().Cadeia.Valor);

            sb.Append(@"|");

            //OPERADOR SMULTIPLICAÇÃO
            sb.Append(@"\");
            sb.Append(new OMultiplicacao().Cadeia.Valor);

            sb.Append(@"|");

            //OPERADOR DIVISÃO
            sb.Append(@"\");
            sb.Append(new ODivisao().Cadeia.Valor);

            sb.Append(@"|");

            if (ComEspacoFinal)
            {
                sb.Append(@")\s\w+" + ExpressaoRegularPermitePontoEmVariavel() + @"\s)*");
            }
            else
            {
                sb.Append(@")\s\w+" + ExpressaoRegularPermitePontoEmVariavel() + @")*");
            }

            return sb.ToString();
        }

         /*
         * 
         */
        private string ExpressaoRegularExpressoes()
        {
            StringBuilder sb = new StringBuilder();
            string OperadoresMatematicos = ExpressaoRegularOperadoresMatematicos(true);
            string OperadoresMatematicosFinais = ExpressaoRegularOperadoresMatematicos(false);
            string OperadoresComparacao = ExpressaoRegularOperadoresComparacao();

            sb.Append(@"^" + ExpressaoRegularCadeia() + ExpressaoRegularPermitePontoEmVariavel() + @"\s");
            sb.Append(OperadoresMatematicos);
            sb.Append(OperadoresComparacao);
            sb.Append(@"+\s" + ExpressaoRegularCadeia() + ExpressaoRegularPermitePontoEmVariavel() + @"");
            sb.Append(OperadoresMatematicosFinais);
            sb.Append(@"$");

            return sb.ToString();
        }

         /*
         * 
         */
        private string ExpressaoRegularSeEntao()
        {
            StringBuilder sb = new StringBuilder();
            string OperadoresComparacao = ExpressaoRegularOperadoresComparacao();
            string OperadoresLogicos = ExpressaoRegularOperadoresLogicos();
            string OperadoresMatematicos = ExpressaoRegularOperadoresMatematicos(true);

            sb.Append(@"if\s" + ExpressaoRegularCadeia() + ExpressaoRegularPermitePontoEmVariavel() + @"\s");
            sb.Append(OperadoresMatematicos);
            sb.Append(OperadoresComparacao);

            sb.Append(@"\s" + ExpressaoRegularCadeia() + ExpressaoRegularPermitePontoEmVariavel() + @"\s(");
            sb.Append(OperadoresMatematicos);
            sb.Append(OperadoresLogicos);

            sb.Append(@"\s" + ExpressaoRegularCadeia() + ExpressaoRegularPermitePontoEmVariavel() + @"\s");
            sb.Append(OperadoresMatematicos);
            sb.Append(OperadoresComparacao);

            sb.Append(@"\s" + ExpressaoRegularCadeia() + ExpressaoRegularPermitePontoEmVariavel() + @"\s");
            sb.Append(OperadoresMatematicos);
            sb.Append(@")*then");

            return sb.ToString();
        }

         /*
         * 
         */
        public bool Validar(Analisador_Lexico Analise)
        {
            bool retorno = true;

            analise = Analise;

            //
            string lido = "";
            int linha = 0;

            bool dentrodeIF = false;
            bool dentrodeTHEN = false;
            bool dentrodeELSE = false;

            bool ocorreuMundancaEstruturaIf = false;
            bool ocorreuOperadorMatematico = false;
            bool ocorreuOperadorComparacao = false;
            bool ocorreuOperadorLogico = false;

            string ConteudoIf = "";
            string ConteudoThen_PorLinha = "";
            string ConteudoElse_PorLinha = "";

            for (int Pos = 0; Pos < Analise.CodigoFonte.Count; Pos++)
            {
                Token tk = Analise.CodigoFonte[Pos];

                if(linha != tk.Linha)
                {
                    lido = "";

                    if(tk is Operador && !(tk is OSe) && !(tk is OSenao) && !(tk is OFimSe))
                    {
                        this.mensagemerro = "Erros de Sintaxe: Uso Incorreto do Operador. Linha" + tk.Linha + ".";
                        retorno = false;
                        break;
                    }
                }

                linha = tk.Linha;

                if(Pos < Analise.CodigoFonte.Count - 1)
                {
                    Token ProximoToken = Analise.CodigoFonte[Pos + 1];

                    if(ProximoToken.Linha != linha)
                    {
                        if(tk is OComparacao || tk is OMatematico || tk is OSe)
                        {
                            this.mensagemerro = "Erro de Sintaxe: Uso incorreto do Operador. Linha " + tk.Linha + ".";
                            retorno = false;
                            break;
                        }
                    }
                }

                if(tk is Valor)
                {
                    if(lido =="" || lido == "O")
                    {
                        lido = "V";
                    }
                    else
                    {
                        this.mensagemerro = "Erro de Sintaxe: Simbolo" + ((Valor)tk).NomeVariavel + " Usada de forma incorreta. Linha " + linha + ".";
                        retorno = false;
                        break;
                    }
                }

                else if(tk is Operador)
                {
                    if(lido == "" || lido == "V")
                    {
                        lido = "O";
                    }
                    else
                    {
                        this.mensagemerro = "Erro de sintaxe: Operador usado de forma incorreta. Linha " + linha + ".";
                        return false;
                        break;
                    }
                }

                ocorreuMundancaEstruturaIf = false;

                if(tk is OSe)
                {
                    dentrodeIF = true;
                    dentrodeTHEN = false;
                    dentrodeELSE = false;

                    ocorreuMundancaEstruturaIf = true;
                }


                if(tk is OEntao)
                {
                    dentrodeIF =  false;
                    dentrodeTHEN = true;
                    dentrodeELSE = false;

                    ocorreuMundancaEstruturaIf = true;
                }

                 if(tk is OSenao)
                {
                    dentrodeIF =  false;
                    dentrodeTHEN = false;
                    dentrodeELSE = true;

                    ocorreuMundancaEstruturaIf = true;
                }

                 if(tk is OFimSe)
                {
                    dentrodeIF =  false;
                    dentrodeTHEN = false;
                    dentrodeELSE = false;

                    ocorreuMundancaEstruturaIf = true;
                }

                Token tkAnterior = null;
                Token tkProximo = null;

                if(Pos > 0)
                {
                    tkAnterior = Analise.CodigoFonte[Pos - 1];
                }

                if(Pos < Analise.CodigoFonte.Count - 1)
                {
                    tkProximo = Analise.CodigoFonte[Pos + 1];
                }

                if(tk is OLogico && !dentrodeIF)
                {
                    this.mensagemerro = "Erro de Sintaxe: Operador Lógico " + ((OLogico)tk).Cadeia.Valor + linha + "." ;
                    retorno = false;
                    break;
                }

                if(dentrodeIF)
                {
                    if(tk is OSe)
                    {
                        ConteudoIf += ((OSe)tk).Cadeia.Valor;
                    }

                    else if(tk is OComparacao)
                    {
                        ConteudoIf += ((OComparacao)tk).Cadeia.Valor;
                    }

                    else if(tk is OMatematico)
                    {
                        ConteudoIf += ((OMatematico)tk).Cadeia.Valor;
                    }

                    else if(tk is OLogico)
                    {
                        ConteudoIf += ((OLogico)tk).Cadeia.Valor;
                    }

                    else if(tk is Valor)
                    {
                        if(((Valor)tk).NomeVariavel != null)
                        {
                            ConteudoIf += ((Valor)tk).NomeVariavel;
                        }
                        else
                        {
                            ConteudoIf += ((Valor)tk).ValorVariavel;
                        }
                    }

                    else
                    {
                        this.mensagemerro = "Erro de Sintaxe" + new OSe().Cadeia.Valor + "Simbolo " + linha + ".";
                        retorno = false;
                        break;
                    }

                    ConteudoIf += " ";

                }

                else if(dentrodeTHEN)
                {
                    if(ConteudoIf != "")
                    {
                        ConteudoIf += new OEntao().Cadeia.Valor;
                        string ER =  ExpressaoRegularSeEntao();
                        Match match  = Regex.Match(ConteudoIf, ER);

                        if(match.Success)
                        {
                            ConteudoIf = "";
                        }
                        else
                        {
                            this.mensagemerro = "Erro de Sintaxe" + new OSe().Cadeia.Valor + "Simbolo " + linha + ".";
                            retorno = false;
                            break;
                        }
                    }
                    else
                    {
                        if(tk is OComparacao)
                        {
                            ConteudoThen_PorLinha += ((OComparacao)tk).Cadeia.Valor;
                        }

                        else if(tk is OMatematico)
                        {
                            ConteudoThen_PorLinha += ((OMatematico)tk).Cadeia.Valor;
                        }

                        else if(tk is OLogico)
                        {
                            ConteudoThen_PorLinha += ((OLogico)tk).Cadeia.Valor;
                        }

                         else if(tk is Valor)
                        {
                             if(((Valor)tk).NomeVariavel != null)
                             {
                                 ConteudoThen_PorLinha += ((Valor)tk).NomeVariavel;
                             }
                             else
                             {
                                 ConteudoThen_PorLinha += ((Valor)tk).ValorVariavel;
                             }
                        }

                        else
                        {
                            this.mensagemerro = "Erro de Sintaxe" + new OSe().Cadeia.Valor + "Simbolo " + linha + ".";
                            retorno = false;
                            break;
                        }

                        if(tkAnterior != null)
                        {
                            if((!(tk is OEntao) && tk.Linha != tkProximo.Linha) || tkProximo is OSenao || tkProximo is OFimSe)
                            {
                                string ER = ExpressaoRegularExpressoes();
                                Match match = Regex.Match(ConteudoThen_PorLinha, ER);

                                if(match.Success)
                                {
                                    ConteudoElse_PorLinha = "";
                                }

                                else
                                {
                                    this.mensagemerro = "Erro de Sintaxe" + new OSe().Cadeia.Valor + "Simbolo " + linha + ".";
                                    retorno = false;
                                    break;
                                }
                            }
                        }

                        if(ConteudoThen_PorLinha != "")
                        {
                            ConteudoThen_PorLinha += " ";
                        }
                    }
                }

                else if(dentrodeELSE)
                {
                    if(tk is OComparacao)
                    {
                        ConteudoElse_PorLinha += ((OComparacao)tk).Cadeia.Valor;
                    }
                    
                    else if(tk is OMatematico)
                    {
                        ConteudoElse_PorLinha += ((OMatematico)tk).Cadeia.Valor;
                    }

                     else if(tk is OLogico)
                    {
                        ConteudoElse_PorLinha += ((OLogico)tk).Cadeia.Valor;
                    }

                     else if(tk is Valor)
                    {
                         if(((Valor)tk).NomeVariavel != null)
                         {
                             ConteudoElse_PorLinha += ((Valor)tk).NomeVariavel;
                         }
                         else
                         {
                             ConteudoElse_PorLinha += ((Valor)tk).ValorVariavel;
                         }
                    }

                    else if(!(tk is OSenao))
                    {
                        this.mensagemerro = "Erro de Sintaxe" + new OSe().Cadeia.Valor + "Simbolo " + linha + ".";
                        retorno = false;
                        break;
                    }

                    if(tkAnterior != null)
                    {
                        if((!(tk is OSenao) && tk.Linha != tkProximo.Linha) || tkProximo is OFimSe)
                        {
                            if(ConteudoElse_PorLinha.Trim() != "")
                            {
                                string ER = ExpressaoRegularExpressoes();
                                Match match = Regex.Match(ConteudoElse_PorLinha, ER);

                                if(match.Success)
                                {
                                    ConteudoElse_PorLinha = "";
                                }

                                else
                                {
                                    this.mensagemerro = "Erro de Sintaxe" + new OSe().Cadeia.Valor + "Simbolo " + linha + ".";
                                    retorno = false;
                                    break;
                                }
                            }
                        }
                    }

                    if(ConteudoElse_PorLinha != "")
                    {
                        ConteudoElse_PorLinha += " ";
                    }
                }

                else
                {
                    if(ConteudoIf != "")
                    {
                        this.mensagemerro = "Erro de Sintaxe" + new OEntao().Cadeia.Valor + "Simbolo " + linha + ".";
                        retorno = false;
                        break;
                    }

                    if(ConteudoThen_PorLinha != "")
                    {
                        this.mensagemerro = "Erro de Sintaxe" + new OSenao().Cadeia.Valor + "Simbolo " + linha + ".";
                        retorno = false;
                        break;
                    }
                }

            }

            return retorno;

        }
    }
}
