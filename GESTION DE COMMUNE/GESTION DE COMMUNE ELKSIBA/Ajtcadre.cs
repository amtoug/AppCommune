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
    public partial class Ajtcadre : Form
    {
        private Form F;
        public Ajtcadre(Form F)
        {
            InitializeComponent();
            this.F = F;
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
            con.cmd.CommandText = "select count(*) from Encadrer where IdStagiaire='" + textBox1.Text + "' and Matr='"+textBox5.Text+"'";
            con.cmd.Connection = con.cnx;
            nb = (int)con.cmd.ExecuteScalar();
            return nb;
        }
        public bool Ajouter()
        {
            if (Exsist() == 0)
            {
                con.cmd.CommandText = "insert into Encadrer values('"+textBox1.Text+"','"+textBox5.Text+"')";
                con.cmd.Connection = con.cnx;
                con.cmd.ExecuteNonQuery();
                return true;
            }
            return false;
        }
        private void Ajtcadre_Load(object sender, EventArgs e)
        {
            con.Connecter();
        }

        private void BtnSupp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Titre_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Ajtcadre_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 F = new Form2(this);
            F.dataGridView2.Visible = false;
            F.TextBox2.Visible = false;
            F.Button3.Visible = false;
            F.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 F = new Form2(this);
            F.Show();
            F.dataGridView1.Visible = false;
            F.TextBox1.Visible = false;
            F.button2.Visible = false;
        }

        private void BtnAjt_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("les champs est obligatoire", "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Ajouter() == true)
                {
                    MessageBox.Show("Ce stage est ajouté avec succes");
                    (F as Stage).RmpGrid();
                }
                else
                {
                    MessageBox.Show("Ce stagiaire est déjà Encadré a cette employée dans cette période", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }
    }
}
