using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GESTION_DE_COMMUNE_ELKSIBA
{
    public partial class Form2 : Form
    {
        private Form f;
        public Form2(Form F)
        {
            InitializeComponent();
            this.f = F;
        }
        Connexion con = new Connexion();
        public void RmpGridStg()
        {
            DataTable dt = new DataTable();
            con.Connecter();
            if (dt.Rows.Count != 0)
            {
                dt.Rows.Clear();
            }
            con.cmd.CommandText = "select IdStagiaire 'ID',CIN,NomStg+' '+PrenomStg 'Nom et prenom',DateDebut 'Date Début',DateFin 'Date Fin' from Stagiaire";
            con.cmd.Connection = con.cnx;
            con.dr = con.cmd.ExecuteReader();
            dt.Load(con.dr);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            con.dr.Close();
            con.Deconnecter();
        }
        public void RmpGridEmp()
        {
            con.Connecter();
            if (con.dt.Rows.Count != 0)
            {
                con.dt.Rows.Clear();
            }
            con.cmd.CommandText = "select Matr,NomEmp+' '+PrenomEmp 'Nom et prenom' from Employé";
            con.cmd.Connection = con.cnx;
            con.dr = con.cmd.ExecuteReader();
            con.dt.Load(con.dr);
            dataGridView2.DataSource = con.dt;
            dataGridView2.Refresh();
            con.dr.Close();
            con.Deconnecter();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            con.Connecter();
            RmpGridStg();
            RmpGridEmp();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            //Rechercher Stagiaire
            if (TextBox1.Text != "")
            {
                DataTable dtR = new DataTable();
                if (dtR.Rows != null)
                {
                    dtR.Clear();
                }
                con.Connecter();
                con.cmd.CommandText = "select IdStagiaire 'ID',CIN,NomStg+' '+PrenomStg 'Nom et prenom',DateDebut 'Date Début',DateFin 'Date Fin' from Stagiaire where upper(NomStg+' '+PrenomStg) Like '%" + TextBox1.Text.ToUpper() + "%'";
                con.cmd.Connection = con.cnx;
                con.dr = con.cmd.ExecuteReader();
                dtR.Load(con.dr);
                dataGridView1.DataSource = dtR;
                con.dr.Close();
                con.Deconnecter();
            }
            else
            {
                RmpGridStg();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string DD = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value.ToString()).ToString("dd-MM-yyyy");
            string DF = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value.ToString()).ToString("dd-MM-yyyy");
            (f as Ajtcadre).textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            (f as Ajtcadre).textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            (f as Ajtcadre).textBox3.Text = DD;
            (f as Ajtcadre).textBox4.Text = DF;
            this.Close();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            //Rechercher Emploiyee
            if (TextBox2.Text != "")
            {
                DataTable data = new DataTable();
                if (data.Rows != null)
                {
                    data.Clear();
                }
                con.Connecter();
                con.cmd.CommandText = "select Matr,NomEmp+' '+PrenomEmp 'Nom et prenom' from Employé where upper(NomEmp+' '+PrenomEmp) Like '%" + TextBox2.Text.ToUpper() + "%'";
                con.cmd.Connection = con.cnx;
                con.dr = con.cmd.ExecuteReader();
                data.Load(con.dr);
                dataGridView2.DataSource = data;
                con.dr.Close();
                con.Deconnecter();
            }
            else
            {
                RmpGridEmp();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (f as Ajtcadre).textBox5.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            (f as Ajtcadre).textBox6.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            this.Close();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 37)
            {
                e.Handled = true;
            }
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 37)
            {
                e.Handled = true;
            }
        }
    }
}
