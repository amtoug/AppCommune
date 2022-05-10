using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GESTION_DE_COMMUNE_ELKSIBA
{
    public partial class CRUDEMP : Form
    {
        private Form FE;
        public CRUDEMP(Form F)
        {
            InitializeComponent();
            this.FE = F;
        }
        Connexion con = new Connexion();

        [DllImport("User32.dll", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hwnd, int msg, int Wparam, int lparam);

        //Exsists 
        public int Exsist()
        {
            int nb;
            con.cmd.CommandText = "select count(*) from Employé where Matr='" + textBox1.Text + "'";
            con.cmd.Connection = con.cnx;
            nb = (int)con.cmd.ExecuteScalar();
            return nb;
        }

        //Ajouter
        public bool Ajouter()
        {
            if (Exsist() == 0)
            {
                if (radioButton1.Checked == true)
                {
                    con.cmd.CommandText = "insert into Employé values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','Femme','" + textBox4.Text + "')";
                }
                else
                {
                    con.cmd.CommandText = "insert into Employé values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','Homme','" + textBox4.Text + "')";
                }
                con.cmd.Connection = con.cnx;
                con.cmd.ExecuteNonQuery();
                return true;
            }
            return false;
        }
        //Modifier
        public void Modifier()
        {
            if (radioButton1.Checked == true)
            {
                con.cmd.CommandText = "update Employé set NomEmp='"+textBox2.Text+ "',PrenomEmp='"+textBox3.Text+ "',GenreEmp='Femme',NumCompte='"+textBox4.Text+ "' where Matr='"+textBox1.Text+"'";
            }
            else
            {
                con.cmd.CommandText = "update Employé set NomEmp='" + textBox2.Text + "',PrenomEmp='" + textBox3.Text + "',GenreEmp='Homme',NumCompte='" + textBox4.Text + "' where Matr='" + textBox1.Text + "'";
            }
            con.cmd.Connection = con.cnx;
            con.cmd.ExecuteNonQuery();
        }
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" )
                {
                    MessageBox.Show("les champs est obligatoire", "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Ajouter() == true)
                {
                    MessageBox.Show("Ce employée est ajouté avec succes");
                    (FE as EMPLOYEES).RmpGrid();
                }
                else
                {
                    MessageBox.Show("ce matricule est déjà exsists", "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void BtnSupp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CRUDEMP_Load(object sender, EventArgs e)
        {
            con.Connecter();
        }

        private void CRUDEMP_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" )
            {
                MessageBox.Show("les champs est obligatoire", "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }else
            {
                Modifier();
                MessageBox.Show("Ce employée est ajouté avec succes");
                (FE as EMPLOYEES).RmpGrid();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Nombre*
            if (e.KeyChar<48||e.KeyChar>57)
            {
                e.Handled = true;
            }
            if(e.KeyChar==8)
            {
                e.Handled = false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==37)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 37)
            {
                e.Handled = true;
            }
        }
    }
}
