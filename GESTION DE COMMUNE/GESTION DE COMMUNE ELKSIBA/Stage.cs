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
    public partial class Stage : Form
    {
        public Stage()
        {
            InitializeComponent();
        }

        Connexion con = new Connexion();
        public void RmpGrid()
        {
            con.Connecter();
            if (con.dt.Rows.Count != 0)
            {
                con.dt.Rows.Clear();
            }
            con.cmd.CommandText = "select Encadrer.IdStagiaire,NomStg+' '+PrenomStg as 'Stagiaire',Encadrer.Matr,NomEmp+' '+PrenomEmp 'Encadré par',DateDebut as 'Date Début',DateFin 'Date Fin'" +
                                  "from Encadrer inner join Employé on Encadrer.Matr=Employé.Matr" +
                                  "              inner join Stagiaire on Encadrer.IdStagiaire=Stagiaire.IdStagiaire";
            con.cmd.Connection = con.cnx;
            con.dr = con.cmd.ExecuteReader();
            con.dt.Load(con.dr);
            dataGridView1.DataSource = con.dt;
            dataGridView1.Refresh();
            con.dr.Close();
            con.Deconnecter();
        }

        private void Cadrer_Load(object sender, EventArgs e)
        {
            con.Connecter();
            RmpGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (dt.Rows != null)
            {
                dt.Clear();
            }
            con.Connecter();
            con.cmd.CommandText = "select Encadrer.IdStagiaire,NomStg+' '+PrenomStg as 'Stagiaire',Encadrer.Matr,NomEmp+' '+PrenomEmp 'Encadré par',DateDebut as 'Date Début',DateFin 'Date Fin' " +
                                  "from Encadrer inner join Employé on Encadrer.Matr=Employé.Matr " +
                                  "              inner join Stagiaire on Encadrer.IdStagiaire=Stagiaire.IdStagiaire " +
                                  "where DateDebut>='"+dateTimePicker1.Value+ "' and DateFin<='"+dateTimePicker2.Value+"'";
            con.cmd.Connection = con.cnx;
            con.dr = con.cmd.ExecuteReader();
            dt.Load(con.dr);
            dataGridView1.DataSource = dt;
            con.dr.Close();
            con.Deconnecter();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RmpGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("vous voulez supprimer ce stage", "confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                con.cmd.CommandText = "delete from Encadrer where Matr='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "' and IdStagiaire='"+ dataGridView1.CurrentRow.Cells[0].Value.ToString()+"'";
                con.cmd.Connection = con.cnx;
                con.cmd.ExecuteNonQuery();
                MessageBox.Show("Ce stage est supprimer avec succes");
                RmpGrid();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ajtcadre F = new Ajtcadre(this);
            F.Show();
        }
    }
}