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
using System.Text.RegularExpressions;

namespace GESTION_DE_COMMUNE_ELKSIBA
{
    public partial class CRUDStagiaire : Form
    {
        private Form FS;
        public Regex rg = new Regex("^[0-9]{4}/[0-9]{4}$");
        public CRUDStagiaire(Form F)
        {
            InitializeComponent();
            this.FS = F;
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
            con.cmd.CommandText = "select count(*) from Stagiaire where CIN='" + textBox1.Text + "' and DateDebut='"+dateTimePicker2.Value+ "' and DateFin='"+dateTimePicker3.Value+"'";
            con.cmd.Connection = con.cnx;
            nb = (int)con.cmd.ExecuteScalar();
            MessageBox.Show(nb.ToString());
            return nb;
        }

        //Ajouter
        public bool Ajouter()
        {
            if (Exsist() == 0)
            {
                if (radioButton1.Checked == true)
                {
                    con.cmd.CommandText = "insert into Stagiaire values('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','Femme','"+dateTimePicker1.Value+"','"+textBox4.Text+"','"+dateTimePicker2.Value+"','"+dateTimePicker3.Value+"')";
                }
                else
                {
                    con.cmd.CommandText = "insert into Stagiaire values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','Homme','" + dateTimePicker1.Value + "','" + textBox4.Text + "','" + dateTimePicker2.Value + "','" + dateTimePicker3.Value + "')";
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
            string Genre;
            if (radioButton1.Checked == true)
            {
                Genre = "Femme";
            }
            else
            {
                Genre = "Homme";
            }
            con.cmd.CommandText = "update Stagiaire set CIN='" + textBox1.Text + "',NomStg='" + textBox2.Text + "',PrenomStg='" + textBox3.Text + "',GenreStg='"+Genre+"',DateNaiss='" + dateTimePicker1.Value + "',Saison='" + textBox4.Text + "',DateDebut='" + dateTimePicker2.Value + "',DateFin='" + dateTimePicker3.Value + "' where IdStagiaire='" + textBox5.Text + "'";
            con.cmd.Connection = con.cnx;
            con.cmd.ExecuteNonQuery();
        }
        private void CRUDStagiaire_Load(object sender, EventArgs e)
        {
            con.Connecter();
        }

        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("les champs est obligatoire", "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (rg.IsMatch(textBox4.Text) == false)
                {
                    MessageBox.Show("la saisie de saison incorrecte(Ex:2020/2021)", "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dateTimePicker2.Value.Date >= dateTimePicker3.Value.Date)
                {
                    MessageBox.Show("La date de début est complètement inférieure à la date de fin", "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Ajouter() == true)
                {
                    MessageBox.Show("Ce stagiaire est ajouté avec succes");
                    (FS as Stagiaire).RmpGrid();
                }
                else
                {
                    MessageBox.Show("Ce stagiaire est déjà exists dans cette période", "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnModifier_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("les champs est obligatoire", "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (rg.IsMatch(textBox4.Text) == false)
                {
                    MessageBox.Show("la saisie de saison incorrecte(Ex:2020/2021)", "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (dateTimePicker2.Value.Date >= dateTimePicker3.Value.Date)
                {
                    MessageBox.Show("La date de début est complètement inférieure à la date de fin", "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Modifier();
                    MessageBox.Show("Ce stagiaire est modifier avec succes");
                    (FS as Stagiaire).RmpGrid();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CRUDStagiaire_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Titre_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BtnSupp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 37)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 37)
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
