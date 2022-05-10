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
using W = Microsoft.Office.Interop.Word;
using System.IO;
using DataTable = System.Data.DataTable;

namespace GESTION_DE_COMMUNE_ELKSIBA
{
    public partial class Stagiaire : Form
    {
        public Stagiaire()
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
            //object matchKashida = false;
            //object matchDiactitics = false;
            //object matchAlefHamza = false;
            //object matchControl = false;
            //object read_only = false;
            //object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref findText,
                        ref matchCase, ref matchWholeWord,
                        ref matchWildCards, ref matchSoundLike,
                        ref nmatchAllForms, ref forward,
                        ref wrap, ref format, ref replaceWithText,
                        ref replace /*ref matchKashida,*/
                        //ref matchDiactitics, /*ref matchAlefHamza,*/
                        /*ref matchControl)*/);
        }

        Connexion con = new Connexion();

        public void RmpGrid()
        {
            con.Connecter();
            if (con.dt.Rows.Count != 0)
            {
                con.dt.Rows.Clear();
            }
            con.cmd.CommandText = "select IdStagiaire,CIN,NomStg as 'Nom',PrenomStg as 'Prenom',GenreStg as 'Genre',DateNaiss as 'Date Naissance',Saison,DateDebut 'Date Début',DateFin 'Date Fin' from Stagiaire";
            con.cmd.Connection = con.cnx;
            con.dr = con.cmd.ExecuteReader();
            con.dt.Load(con.dr);
            dataGridView1.DataSource = con.dt;
            dataGridView1.Refresh();
            con.dr.Close();
            con.Deconnecter();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox1.Enabled = true;
        }

        private void Stagiaire_Load(object sender, EventArgs e)
        {
            RmpGrid();
            dataGridView1.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Rechercher
            if (TextBox1.Text != "")
            {
                DataTable dtR = new DataTable();
                if (dtR.Rows != null)
                {
                    dtR.Clear();
                }
                con.Connecter();
                switch (comboBox1.Text)
                {
                    case "CIN":
                        con.cmd.CommandText = "select IdStagiaire,CIN,NomStg as 'Nom',PrenomStg as 'Prenom',GenreStg as 'Genre',DateNaiss as 'Date Naissance',Saison,DateDebut 'Date Debut',DateFin 'Date Fin' from Stagiaire where upper(CIN) Like '%" + TextBox1.Text.ToUpper() + "%'";
                        break;
                    case "Nom":
                        con.cmd.CommandText = "select IdStagiaire,CIN,NomStg as 'Nom',PrenomStg as 'Prenom',GenreStg as 'Genre',DateNaiss as 'Date Naissance',Saison,DateDebut 'Date Debut',DateFin 'Date Fin' from Stagiaire where upper(NomStg) Like '%" + TextBox1.Text.ToUpper() + "%'";
                        break;
                    case "Prenom":
                        con.cmd.CommandText = "select IdStagiaire,CIN,NomStg as 'Nom',PrenomStg as 'Prenom',GenreStg as 'Genre',DateNaiss as 'Date Naissance',Saison,DateDebut 'Date Debut',DateFin 'Date Fin' from Stagiaire where upper(PrenomStg) Like '%" + TextBox1.Text.ToUpper() + "%'";
                        break;
                    case "Genre":
                        con.cmd.CommandText = "select IdStagiaire,CIN,NomStg as 'Nom',PrenomStg as 'Prenom',GenreStg as 'Genre',DateNaiss as 'Date Naissance',Saison,DateDebut 'Date Debut',DateFin 'Date Fin' from Stagiaire where upper(GenreStg) Like '%" + TextBox1.Text.ToUpper() + "%'";
                        break;
                    case "Saison":
                        con.cmd.CommandText = "select IdStagiaire,CIN,NomStg as 'Nom',PrenomStg as 'Prenom',GenreStg as 'Genre',DateNaiss as 'Date Naissance',Saison,DateDebut 'Date Debut',DateFin 'Date Fin' from Stagiaire where Saison Like '%" + TextBox1.Text + "%'";
                        break;
                }
                con.cmd.Connection = con.cnx;
                con.dr = con.cmd.ExecuteReader();
                dtR.Load(con.dr);
                dataGridView1.DataSource = dtR;
                con.dr.Close();
                con.Deconnecter();
            }
            else
            {
                RmpGrid();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CRUDStagiaire F = new CRUDStagiaire(this);
            F.label10.Visible = true;
            F.textBox5.Visible = true;
            F.BtnAjouter.Visible = false;
            F.BtnModifier.Visible = true;
            F.textBox5.Enabled = false;
            F.textBox5.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            F.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            F.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            F.textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            F.dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            F.textBox4.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            F.dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            F.dateTimePicker3.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            string ch= dataGridView1.CurrentRow.Cells[4].Value.ToString();
            if(ch=="Femme")
            {
                F.radioButton1.Checked = true;
            }else
            {
                F.radioButton2.Checked = true;
            }
            F.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("vous voulez supprimer ce stagiaire","confirmation",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                con.cmd.CommandText = "delete from Stagiaire where IdStagiaire='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                con.cmd.Connection = con.cnx;
                con.cmd.ExecuteNonQuery();
                MessageBox.Show("Ce satgiaire est supprimer avec succes");
                RmpGrid();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            CRUDStagiaire F = new CRUDStagiaire(this);
            F.label10.Visible = false;
            F.textBox5.Visible = false;
            F.BtnModifier.Visible = false;
            F.BtnAjouter.Visible = true;
            F.Show();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 37)
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime DateNaissance = DateTime.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            DateTime DateDébut = DateTime.Parse(dataGridView1.CurrentRow.Cells[7].Value.ToString());
            DateTime DateFin = DateTime.Parse(dataGridView1.CurrentRow.Cells[8].Value.ToString());
            string dateN= DateNaissance.ToString("dd/MM/yyyy");
            string dateD = DateDébut.ToString("dd/MM/yyyy");
            string dateF = DateFin.ToString("dd/MM/yyyy");
            W.Application app = new W.Application();
            Document doc = new Document();
            doc = app.Documents.Add(Template: Path.Combine(Environment.CurrentDirectory, "ATTESTATION DE STAGE.docx"));
            //Find and replace:
            this.FindAndReplace(app, "saison", dataGridView1.CurrentRow.Cells[6].Value.ToString());
            this.FindAndReplace(app, "nom", dataGridView1.CurrentRow.Cells[2].Value.ToString().ToUpper()+' '+ dataGridView1.CurrentRow.Cells[3].Value.ToString().ToUpper());
            this.FindAndReplace(app, "cin", dataGridView1.CurrentRow.Cells[1].Value.ToString().ToUpper());
            this.FindAndReplace(app, "DateNaissance",dateN);
            this.FindAndReplace(app, "DateDébut", dateD);
            this.FindAndReplace(app, "DateFin", dateF);
            if(dataGridView1.CurrentRow.Cells[4].Value.ToString()=="Homme")
            {
                this.FindAndReplace(app, "GENRE", "Mr");
            }else
            {
                this.FindAndReplace(app, "GENRE", "Mlle");
            }
            app.Visible = true;
            #region Print Document :
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
