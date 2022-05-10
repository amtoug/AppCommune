using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using W= Microsoft.Office.Interop.Word;
using System.IO;
using DataTable = System.Data.DataTable;

namespace GESTION_DE_COMMUNE_ELKSIBA
{
    public partial class EMPLOYEES : Form
    {
        public EMPLOYEES()
        {
            InitializeComponent();
        }

        private void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllForms = false;
            object forward = true;
            object format = false;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref findText,
                        ref matchCase, ref matchWholeWord,
                        ref matchWildCards, ref matchSoundLike,
                        ref nmatchAllForms, ref forward,
                        ref wrap, ref format, ref replaceWithText,
                        ref replace);
        }

        Connexion con = new Connexion();
        //Rmplier Grid1
        public void RmpGrid()
        {
            con.Connecter();
            if(con.dt.Rows.Count!=0)
            {
                con.dt.Rows.Clear();
            }
            con.cmd.CommandText = "select Matr,NomEmp 'Nom',PrenomEmp 'Prenom',GenreEmp 'Genre',NumCompte from Employé";
            con.cmd.Connection = con.cnx;
            con.dr = con.cmd.ExecuteReader();
            con.dt.Load(con.dr);
            dataGridView1.DataSource = con.dt;
            dataGridView1.Refresh();
            con.dr.Close();
            con.Deconnecter();
        }

        public void EMPLOYEES_Load(object sender, EventArgs e)
        {
            con.Connecter();
            RmpGrid();
        }

        private void Supp_Click(object sender, EventArgs e)
        {
            this.Close();
            Home FH = new Home();
            FH.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btnAjt_Click(object sender, EventArgs e)
        {
            CRUDEMP Frm = new CRUDEMP(this);
            Frm.BtnModifier.Visible = false;
            Frm.BtnAjouter.Visible = true;
            Frm.Show();
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            CRUDEMP Frm = new CRUDEMP(this);
            Frm.BtnModifier.Visible=true;
            Frm.BtnAjouter.Visible=false;
            Frm.textBox1.Enabled = false;
            Frm.textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Frm.textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Frm.textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Frm.textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[3].Value.ToString()=="Femme")
            {
                Frm.radioButton1.Checked = true;
            }else
            {
                Frm.radioButton2.Checked = true;
            }
            Frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("vous voulez supprimer ce employé", "confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                con.cmd.CommandText = "delete from Employé where Matr='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                con.cmd.Connection = con.cnx;
                con.cmd.ExecuteNonQuery();
                MessageBox.Show("Ce employée est supprimer avec succes");
                RmpGrid();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Rechercher
            if (textBox1.Text!="")
            {
                DataTable dtR = new DataTable();
                if(dtR.Rows!=null)
                {
                    dtR.Clear();
                }
                con.Connecter();
                switch (comboBox1.Text)
                {
                    case "Nom":
                        con.cmd.CommandText = "select Matr,NomEmp 'Nom',PrenomEmp 'Prenom',GenreEmp 'Genre',NumCompte  from Employé where upper(NomEmp) Like '%" + textBox1.Text.ToUpper() + "%'";
                        break;
                    case "Prenom":
                        con.cmd.CommandText = "select Matr,NomEmp 'Nom',PrenomEmp 'Prenom',GenreEmp 'Genre',NumCompte  from Employé where upper(PrenomEmp) Like '%" + textBox1.Text.ToUpper() + "%'";
                        break;
                    case "Genre":
                        con.cmd.CommandText = "select Matr,NomEmp 'Nom',PrenomEmp 'Prenom',GenreEmp 'Genre',NumCompte  from Employé where upper(GenreEmp) Like '%" + textBox1.Text.ToUpper() + "%'";
                        break;
                    case "Matr":
                        con.cmd.CommandText = "select Matr,NomEmp 'Nom',PrenomEmp 'Prenom',GenreEmp 'Genre',NumCompte  from Employé where upper(Matr) Like '%" + textBox1.Text.ToUpper() + "%'";
                        break;
                }
                con.cmd.Connection = con.cnx;
                con.dr = con.cmd.ExecuteReader();
                dtR.Load(con.dr);
                dataGridView1.DataSource = dtR;
                con.dr.Close();
                con.Deconnecter();
            }else
            {
                RmpGrid();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 37)
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            W.Application app = new W.Application();
            Document doc = new Document();
            doc = app.Documents.Add(Template: Path.Combine(Environment.CurrentDirectory, "ATTESTATION DE TRAVAIL.docx"));
            //Find and replace:
            this.FindAndReplace(app, "NOM", dataGridView1.CurrentRow.Cells[2].Value.ToString().ToUpper() + ' ' + dataGridView1.CurrentRow.Cells[1].Value.ToString().ToUpper());
            this.FindAndReplace(app, "MATR", dataGridView1.CurrentRow.Cells[0].Value.ToString());
            if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "Homme")
            {
                this.FindAndReplace(app, "GENRE", "Mr");
            }
            else
            {
                this.FindAndReplace(app, "GENRE", "Mlle");
            }
            app.Visible = true;
            
            object copies = "1";
            object pages = "1";
            object range = W.WdPrintOutRange.wdPrintCurrentPage;
            object items = W.WdPrintOutItem.wdPrintDocumentContent;
            object pageType = W.WdPrintOutPages.wdPrintAllPages;
            object oTrue = true;
            object oFalse = false;

            W.Document document = doc;
            int dialogResult = app.Dialogs[WdWordDialog.wdDialogFilePrint].Show();
            //app.Visible = false;
            if (dialogResult == 1)
            {
                document.PrintOut(
                ref oTrue, ref oFalse, ref range,
                ref items, ref copies, ref pages, ref pageType, ref oFalse, ref oTrue);
            }
            //app.Quit();
            //app.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            W.Application app = new W.Application();
            Document doc = new Document();
            doc = app.Documents.Add(Template: Path.Combine(Environment.CurrentDirectory, "ORDRE DE VIREMENT IRREVOCABLE.docx"));
            //Find and replace:
            this.FindAndReplace(app, "NOM", dataGridView1.CurrentRow.Cells[2].Value.ToString().ToUpper() + ' ' + dataGridView1.CurrentRow.Cells[1].Value.ToString().ToUpper());
            this.FindAndReplace(app, "MATR", dataGridView1.CurrentRow.Cells[0].Value.ToString());
            this.FindAndReplace(app, "NumCompt", dataGridView1.CurrentRow.Cells[4].Value.ToString());
            if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "Homme")
            {
                this.FindAndReplace(app, "GENRE", "Mr");
            }
            else
            {
                this.FindAndReplace(app, "GENRE", "Mlle");
            }
            app.Visible = true;
            #region Print
            object copies = "1";
            object pages = "1";
            object range = W.WdPrintOutRange.wdPrintCurrentPage;
            object items = W.WdPrintOutItem.wdPrintDocumentContent;
            object pageType = W.WdPrintOutPages.wdPrintAllPages;
            object oTrue = true;
            object oFalse = false;

            W.Document document = doc;
            int dialogResult = app.Dialogs[WdWordDialog.wdDialogFilePrint].Show();
            //app.Visible = false;
            if (dialogResult == 1)
            {
                document.PrintOut(
                ref oTrue, ref oFalse, ref range,
                ref items, ref copies, ref pages, ref pageType, ref oFalse, ref oTrue);
            }
            #endregion
            //app.Visible = false;
        }
    }
}
