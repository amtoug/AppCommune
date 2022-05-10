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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //changer la position
        [DllImport("User32.dll", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hwnd, int msg, int Wparam, int lparam);
        //afficher form pour panel
        public void AjtFormPanel(Form Frm)
        {
            if (this.panel2.Controls.Count > 0)
                this.panel2.Controls.RemoveAt(0);
            //Form Frm = F as Form;
            Frm.TopLevel = false;
            Frm.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(Frm);
            this.panel2.Tag = Frm;
            Frm.Show();
        }
        private void Colseicon_Click(object sender, EventArgs e)
        {
            EMPLOYEES E = new EMPLOYEES();
            Application.Exit();
        }

        private void Miniicone_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            maximizeicon.Visible = true;
            Miniicone.Visible = false;
        }

        private void maximizeicon_Click(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            maximizeicon.Visible = false;
            Miniicone.Visible = true;
        }

        private void minimiz_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AjtFormPanel(new EMPLOYEES());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AjtFormPanel(new Home());
        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            //Home Frm = new Home();
            //Frm.RmpGrid();
            AjtFormPanel(new Home());
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AjtFormPanel(new Stagiaire());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AjtFormPanel(new Stage());
        }
    }
}
