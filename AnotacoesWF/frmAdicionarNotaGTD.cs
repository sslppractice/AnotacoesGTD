using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Anotacoes;

namespace AnotacoesWF
{
    public partial class frmAdicionarNotaGTD : Form
    {
        private AcessoEvernote acesso;
        private Color colorSelection = Color.DarkGray;
        public frmAdicionarNotaGTD()
        {
            string url = "https://www.evernote.com/shard/s449/notestore";
            string token, token_path = @"F:\Dropbox\Settings\evernote_token.txt";
            using (TextReader tr = new StreamReader(token_path))
            {
                token = tr.ReadToEnd();
            }
            this.acesso = new AcessoEvernote(token, url);
            
            InitializeComponent();
            this.hora.Value = DateTime.Now;
            this.hora_ValueChanged(new object(), new EventArgs());
            this.rbNãoEspecificada_CheckedChanged(new object(), new EventArgs());

            this.txtTitulo.Text = string.Format("[{0}]", DateTime.Now.ToShortDateString());
            this.txtConteudo.Text = this.txtTitulo.Text;

        }

        private string RodapéDoConteúdo()
        {
            DateTime dt = DateTime.Now;
            StringBuilder sb = new StringBuilder();

            sb.Append(Environment.NewLine);
            sb.Append("-----------------");
            sb.Append(Environment.NewLine);
            sb.Append(dt.ToLongDateString());
            sb.Append(" ");
            sb.Append(dt.ToLongTimeString());
            sb.Append(Environment.NewLine);
            sb.Append(string.Format("{0}-{1}-{2} {3}:{4}",
                dt.Year, dt.Month, dt.Day,
                dt.Hour, dt.Minute));
            return sb.ToString();

        }

        private void btnNovaNota_Click(object sender, EventArgs e)
        {
            string stTitulo = txtTitulo.Text;
            try
            {
                List<string> Tags = new List<string>();
                if (rbPróximaAção.Checked)
                {
                    foreach (string item in lstContexto.SelectedItems)
                        Tags.Add(item);
                    foreach (string item in lstPrioridade.SelectedItems)
                        Tags.Add(item);
                    foreach (string item in lstDuração.SelectedItems)
                        Tags.Add(item);
                    foreach (string item in lstEnergia.SelectedItems)
                        Tags.Add(item);
                    stTitulo += "[NEXT ACTIONS]";
                }
                else if (rbAgenda.Checked)
                {
                    Tags.Add("!0.agenda");
                    stTitulo = lblAgenda.Text + " " + txtTitulo.Text;
                    stTitulo += "[AGENDA]";
                }
                else if (rbAguardando.Checked)
                {
                    Tags.Add("!7.waiting.for");
                    stTitulo += "[WAITING FOR]";
                }
                else if (rbRotina.Checked)
                {
                    foreach (string item in lstRotina.Items)
                        Tags.Add(item);
                    stTitulo += "[ROTINA]";
                }
                string stConteudo = txtConteudo.Text + this.RodapéDoConteúdo();
                this.acesso.CriarNota(stTitulo, txtConteudo.Text, Tags);

                StringBuilder sbMsg = new StringBuilder();
                sbMsg.Append("Nota adicionada com sucesso:");
                sbMsg.Append(string.Format("Título:{0}\n", stTitulo));
                foreach (var tag in Tags)
                {
                    sbMsg.Append(string.Format("\tTag:{0}\n", tag));
                }
                MessageBox.Show(sbMsg.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Um erro ocorreu: " + ex.Message);
            }
        }

        private void hora_ValueChanged(object sender, EventArgs e)
        {
            calendario.SetDate(hora.Value);
            lblAgenda.Text = string.Format("{0}-{1}-{2} {3}:{4}",
                hora.Value.Year, hora.Value.Month, hora.Value.Day,
                hora.Value.Hour, hora.Value.Minute);
        }

        private void calendario_DateChanged(object sender, DateRangeEventArgs e)
        {
            hora.Value = new DateTime(
                calendario.SelectionRange.Start.Year,
                calendario.SelectionRange.Start.Month, 
                calendario.SelectionRange.Start.Day, 
                hora.Value.Hour, hora.Value.Minute, 0);
            this.hora_ValueChanged(sender, e);


        }

        private void rbNãoEspecificada_CheckedChanged(object sender, EventArgs e)
        {
            lstAgenda.SelectedIndex = -1;
            lstContexto.SelectedIndex = -1;
            lstDuração.SelectedIndex = -1;
            lstEnergia.SelectedIndex = -1;
            lstPrioridade.SelectedIndex = -1;
            lstRotina.SelectedIndex = -1;
            grbPróximaAção.BackColor = grbRotinas.BackColor = grbAgenda.BackColor = Color.White;
            grbAgenda.Enabled = grbPróximaAção.Enabled = grbRotinas.Enabled = false;
        }

        private void rbAguardando_CheckedChanged(object sender, EventArgs e)
        {
            lstAgenda.SelectedIndex = -1;
            lstContexto.SelectedIndex = -1;
            lstDuração.SelectedIndex = -1;
            lstEnergia.SelectedIndex = -1;
            lstPrioridade.SelectedIndex = -1;
            lstRotina.SelectedIndex = -1;
            grbPróximaAção.BackColor = grbRotinas.BackColor = grbAgenda.BackColor = Color.White;
            grbAgenda.Enabled = grbPróximaAção.Enabled = grbRotinas.Enabled = false;
        }

        private void rbPróximaAção_CheckedChanged(object sender, EventArgs e)
        {
            lstAgenda.SelectedIndex = -1;
            lstRotina.SelectedIndex = -1;
            grbAgenda.Enabled = grbRotinas.Enabled = false;
            grbPróximaAção.BackColor = grbRotinas.BackColor = grbAgenda.BackColor = Color.White;
            grbPróximaAção.Enabled = true;
            grbPróximaAção.BackColor = colorSelection;
        }

        private void rbRotina_CheckedChanged(object sender, EventArgs e)
        {
            lstAgenda.SelectedIndex = -1;
            lstContexto.SelectedIndex = -1;
            lstDuração.SelectedIndex = -1;
            lstEnergia.SelectedIndex = -1;
            lstPrioridade.SelectedIndex = -1;
            grbAgenda.Enabled = grbPróximaAção.Enabled = false;
            grbPróximaAção.BackColor = grbRotinas.BackColor = grbAgenda.BackColor = Color.White;
            grbRotinas.Enabled = true;
            grbRotinas.BackColor = colorSelection;
        }

        private void rbAgenda_CheckedChanged(object sender, EventArgs e)
        {
            lstAgenda.SelectedIndex = -1;
            lstContexto.SelectedIndex = -1;
            lstDuração.SelectedIndex = -1;
            lstEnergia.SelectedIndex = -1;
            lstPrioridade.SelectedIndex = -1;
            lstRotina.SelectedIndex = -1;
            grbPróximaAção.Enabled = grbRotinas.Enabled = false;
            grbPróximaAção.BackColor = grbRotinas.BackColor = grbAgenda.BackColor = Color.White;
            grbAgenda.Enabled = true;
            grbAgenda.BackColor = colorSelection;
            lstAgenda.SelectedIndex = 0;
        }
    }
}
