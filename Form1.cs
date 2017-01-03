using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompiladorX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CompiladorX.Variaveis variaveis = new CompiladorX.Variaveis(new List<CompiladorX.Valor>());
        CompiladorX.CodigoIntermediario codigo = null;

        private void button2_Click(object sender, EventArgs e)
        {
            CompiladorX.Analisador_Lexico A1 = new CompiladorX.Analisador_Lexico();

            if (A1.Validar(richTextBox1.Text, variaveis.ListaVariaveis))
            {
                CompiladorX.AnalisadorSintatico Asin = new CompiladorX.AnalisadorSintatico();
                if (Asin.Validar(A1))
                {
                    CompiladorX.AnalisadorSemantico Asem = new CompiladorX.AnalisadorSemantico();

                    if (Asem.Validar(Asin))
                    {
                        dataGridView2.DataSource = Asem.getCodigoIntermediario();
                        codigo = Asem.Codigo;
                    }

                    else
                    {
                        MessageBox.Show(Asem.MensagemErro);
                    }
                }

                else
                {
                    MessageBox.Show(Asin.MensagemErro);
                }
            }

            else
            {
                MessageBox.Show(A1.MensagemErro);
            }
        }

        private void Adicionar_Click(object sender, EventArgs e)
        {
            CompiladorX.Valor v1 = new CompiladorX.Valor(tbNome.Text, tbValor.Text, tbTipo.Text);
            variaveis.AdicionarVariavel(v1);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = variaveis;
            dataGridView1.DataSource = bSource;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            variaveis.AdicionarVariavel(new CompiladorX.Valor("RETORNO", "10", CompiladorX.Tipos.Dec));
            variaveis.AdicionarVariavel(new CompiladorX.Valor("IGNICAO", "5", CompiladorX.Tipos.Dec));
            variaveis.AdicionarVariavel(new CompiladorX.Valor("GERAMENSAGEM", "20", CompiladorX.Tipos.Dec));

            BindingSource bSource = new BindingSource();
            bSource.DataSource = variaveis.ListaVariaveis;
            dataGridView1.DataSource = bSource;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CompiladorX.Compilador cmp = new CompiladorX.Compilador();
            cmp.Executar(codigo, variaveis);

            richTextBox2.Text = "";

            if (cmp.MensagemErro.Count > 0)
            {
                foreach (string err in cmp.MensagemErro)
                {
                    richTextBox2.Text += err + "\r\n";
                }
            }

            else
            {
                MessageBox.Show("Validação com Sucesso!");
            }
        }
    }
}
