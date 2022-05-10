using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace GESTION_DE_COMMUNE_ELKSIBA
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        Connexion con = new Connexion();
        public int NombreTotalLigne(string table)
        {
            int nb;
            con.cmd.CommandText = "select count(*) from " + table;
            con.cmd.Connection = con.cnx;
            nb = (int)con.cmd.ExecuteScalar();
            return nb;
        }
        //les stagiaires maintenant
        public void NombreStagiaireMaintenant()
        {
            int nb;
            con.cmd.CommandText = "select count(*) from Stagiaire where DateDebut<='" + DateTime.Now.ToString() + "' and DateFin>='"+ DateTime.Today.ToString()+"'";
            con.cmd.Connection = con.cnx;
            nb = (int)con.cmd.ExecuteScalar();
            label7.Text = nb.ToString() + " Maintenant";
        }
        //les stage cadrer maintenant 
        public void NbStageCadrerMaintenant()
        {
            int nb;
            con.cmd.CommandText = "select count(*) from Stagiaire inner join Encadrer on Stagiaire.IdStagiaire=Encadrer.IdStagiaire where DateDebut<='" + DateTime.Now.ToString() + "' and DateFin>='" + DateTime.Today.ToString() + "'";
            con.cmd.Connection = con.cnx;
            nb = (int)con.cmd.ExecuteScalar();
            label10.Text = nb.ToString() + " Maintenant";
        }
        //Rmplier Grid1
        public void RmpGrid()
        {
            if(con.dt!=null)
            {
                con.dt.Rows.Clear();
            }
            con.cmd.CommandText = "select Employé.Matr as 'Matricule',NomEmp+' '+PrenomEmp as 'Encadré par',Stagiaire.IdStagiaire as 'ID',NomStg+' '+PrenomStg as 'Stagiaire',DateDebut as'Date Debut',DateFin as'Date Fin'" +
                                  " from Stagiaire left join Encadrer on Stagiaire.IdStagiaire=Encadrer.IdStagiaire left join Employé on Employé.Matr = Encadrer.Matr"+
                                  " where DateDebut<=GETDATE() and DateFin>=GETDATE()";
            con.cmd.Connection = con.cnx;
            con.dr = con.cmd.ExecuteReader();
            con.dt.Load(con.dr);
            dataGridView1.DataSource = con.dt;
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[2].Width = 35;
            dataGridView1.Columns[4].Width = 75;
            dataGridView1.Columns[5].Width = 75;
            dataGridView1.Columns[1].Width = dataGridView1.Columns[3].Width;
            con.dr.Close();
        }
        private void Home_Load_1(object sender, EventArgs e)
        {
            con.Connecter();
            label4.Text = NombreTotalLigne("Employé").ToString();
            label6.Text = NombreTotalLigne("Stagiaire").ToString();
            label9.Text = NombreTotalLigne("Encadrer").ToString();
            NombreStagiaireMaintenant();
            NbStageCadrerMaintenant();
            RmpGrid();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
            label2.Text = DateTime.Now.ToString("dddd d MMMM yyyy");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
