using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Supreme
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        /// //////////////////////////////////////SERVER BAĞLANTI YERİ////////////////////////////////////
        SqlConnection Con = new SqlConnection(@"");
        /// //////////////////////////////////////SERVER BAĞLANTI YERİ////////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            if (kullaniciTB.Text == "" || SifreTB.Text == "")
            {
                MessageBox.Show("Kullanıcı adı ve şifre giriniz !");
            }
            else if (kullaniciTB.Text == "Admin" || kullaniciTB.Text == "admin" && SifreTB.Text == "1185")
            {
                Menu menü = new Menu();
                menü.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Doğru kullanıcı adı ve şifre giriniz !");
            }
        }

        private void SifreTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (kullaniciTB.Text == "" || SifreTB.Text == "")
                {
                    MessageBox.Show("Kullanıcı adı ve şifre giriniz !");
                }
                else if (kullaniciTB.Text == "Admin" || kullaniciTB.Text == "admin" && SifreTB.Text == "1185")
                {
                    Menu menü = new Menu();
                    menü.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Doğru kullanıcı adı ve şifre giriniz !");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Giris_Load(object sender, EventArgs e)
        {
   
        }

        private void Giris_Load_1(object sender, EventArgs e)
        {
           Process process = new Process();
           process.StartInfo.FileName = @"I:\BigCon\Gym\Supreme\Resources\Baslat.bat";          
           process.Start();
        }
    }
}
