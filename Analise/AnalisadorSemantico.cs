using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CompiladorX
{
    class AnalisadorSemantico
    {
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

        AnalisadorSintatico analise;

        public AnalisadorSintatico AnaliseSintatica
        {
            get
            {
                return analise;
            }
        }

        CodigoIntermediario codigoIntermediario = new CodigoIntermediario();
        public CodigoIntermediario Codigo
        {
            get
            {
                return codigoIntermediario;
            }
        }

        public DataTable getCodigoIntermediario()
        {
            DataTable retorno = new DataTable();
            retorno.Columns.Add("Condição");
            retorno.Columns.Add("Expressão");
            retorno.Columns.Add("ExpCondicaoNaoAtendida");

            foreach(ExpressaoCodigoIntermediario expressao in codigoIntermediario.Codigo)
            {
                DataRow DR = retorno.NewRow();
                StringBuilder exp = new StringBuilder();

                foreach(Token tk in expressao.Condicao)
                {
                    exp.Append(tk.Texto);
                    exp.Append(" ");
                }
                DR["Condicao"] = exp.ToString();

                exp = new StringBuilder();

                foreach(Token tk in expressao.Expressao)
                {
                    exp.Append(tk.Texto);
                    exp.Append(" ");
                }
                DR["Expressao"] = exp.ToString();

                exp = new StringBuilder();

                foreach(Token tk in expressao.ExpressaoCondicaoNaoAtendida)
                {
                    exp.Append(tk.Texto);
                    exp.Append(" ");
                }
                DR["ExpCondicaoNaoAtendida"] = exp.ToString().Trim();
                retorno.Rows.Add(DR);
            }

            return retorno;
        }

        public bool Validar(AnalisadorSintatico Analise)
        {
            bool retorno = true;

            analise = Analise;


            //
            bool dentrodeIF = false;
            bool dentrodeTHEN = false;
            bool dentroELSE = false;

            ExpressaoCodigoIntermediario expressao = new ExpressaoCodigoIntermediario();

            for (int pos = 0; pos < Analise.AnaliseLexica.CodigoFonte.Count; pos++)
            {
                Token tk = Analise.AnaliseLexica.CodigoFonte[pos];
                Token tkAnterior = null;
                Token tkProximo = null;

                int linha = 0;
                linha = tk.Linha;

                if(pos > 0)
                {
                    tkAnterior = Analise.AnaliseLexica.CodigoFonte[pos - 1];
                }

                if(pos < Analise.AnaliseLexica.CodigoFonte.Count - 1)
                {
                    tkProximo = Analise.AnaliseLexica.CodigoFonte[pos + 1];
                }

                if(tk is OMatematico && tk is OComparacao)
                {
                    if(tk is OMaior || tk is OMenor || tk is OMaiorIgual || tk is OMenorIgual)
                    {
                        
                        if(((Valor)tkAnterior).Tipo != Tipos.Dec && 
                          ((Valor)tkAnterior).Tipo != Tipos.Hex && 
                          ((Valor)tkAnterior).Tipo != Tipos.Bin)
                        {
                            string NomeValor = ((Valor)tkAnterior).NomeVariavel == "" ? ((Valor)tkAnterior).ValorVariavel.ToString() : ((Valor)tkAnterior).NomeVariavel;
                            this.mensagemerro = "Valores Invalidos" + NomeValor;
                            retorno = false;
                            break;
                        }
                    }

                    else
                    {
                        this.mensagemerro = "Erro: " + ((Valor)tk).Tipo + " Linha " + linha;
                        retorno = false;
                        break;
                    }
                }

                if(tk is OComparacao)
                {
                    if (((Valor)tkAnterior).Tipo == Tipos.Dec || ((Valor)tkAnterior).Tipo == Tipos.Hex || ((Valor)tkAnterior).Tipo == Tipos.Bin)
                    {
                        if (((Valor)tkAnterior).Tipo != ((Valor)tkProximo).Tipo)
                        {
                            this.mensagemerro = "Erro: " + linha;
                            retorno = false;
                            break;
                        }
                    }

                    else
                    {
                        if (!(tk is OIgual))
                        {
                            this.mensagemerro = "Erro: " + ((Valor)tkAnterior).Tipo + " é " + ((Valor)tkProximo).Tipo + "Erro na linha " + linha;
                            retorno = false;
                            break;
                        }
                    }
                }

                if(tkAnterior != null)
                {
                    if(tkAnterior.Linha != tk.Linha)
                    {
                        if(expressao.Expressao.Count > 0 || expressao.ExpressaoCondicaoNaoAtendida.Count > 0)
                        {
                            codigoIntermediario.AdicionarExpressao(expressao);

                            ExpressaoCodigoIntermediario expressaoTemp = new ExpressaoCodigoIntermediario();

                            if(dentrodeTHEN || dentroELSE && (tk is OFimSe))
                            {
                                expressaoTemp.Condicao = expressao.getCopiaCondicao();
                            }

                            expressao = expressaoTemp;
                        }

                        if(tk is OFimSe && expressao.Condicao.Count > 0)
                        {
                            expressao.Condicao = new List<Token>();
                        }
                    }
                }

                if(tk is  OSe)
                {
                    dentrodeIF = true;
                    dentrodeTHEN = false;
                    dentroELSE = false;

                    continue;
                }

                if (tk is OEntao)
                {
                    dentrodeIF = false;
                    dentrodeTHEN = true;
                    dentroELSE = false;

                    continue;
                }

                if (tk is OSenao)
                {
                    dentrodeIF = false;
                    dentrodeTHEN = false;
                    dentroELSE = true;

                    continue;
                }

                if (tk is OFimSe)
                {
                    dentrodeIF = false;
                    dentrodeTHEN = false;
                    dentroELSE = false;

                    continue;
                }

                if(dentrodeIF)
                {
                    expressao.AdicionarTokenEmCondicao(tk);
                }

                else if(dentrodeTHEN)
                {
                    expressao.AdicionarTokenEmExpressao(tk);
                }

                else if (dentroELSE)
                {
                    expressao.AdicionarTokenEmExpressao(tk);
                }

                else
                {
                    expressao.AdicionarTokenEmExpressao(tk);
                }
            }

            if(expressao.Expressao.Count > 0 || expressao.ExpressaoCondicaoNaoAtendida.Count > 0)
            {
                codigoIntermediario.AdicionarExpressao(expressao);
            }

            return retorno;
        }
    }
}
