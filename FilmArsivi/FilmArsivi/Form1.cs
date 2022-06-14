using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //Sql kütüphanesi//

namespace FilmArsivi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-K8PC86I\\SQLEXPRESS;Initial Catalog=FilmArsiv;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Ekle
        {
            
            SqlCommand komut = new SqlCommand("insert into Arsiv (FilmAdı,FilmSuresi,FilmTuru,Yonetmen,Imdb,Konu) values (@Filmadı,@FilmSuresi,@FilmTuru,@Yonetmen,@Imdb,@Konu)", baglanti);

            komut.Parameters.AddWithValue("@FilmAdı", textBox1.Text);
            komut.Parameters.AddWithValue("@FilmSuresi", textBox2.Text);
            komut.Parameters.AddWithValue("@FilmTuru", textBox3.Text);
            komut.Parameters.AddWithValue("@Yonetmen", textBox4.Text);
            komut.Parameters.AddWithValue("@Imdb", textBox5.Text);
            komut.Parameters.AddWithValue("@Konu", textBox6.Text);
            

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Film Eklendi");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 ikinciform = new Form2();
            ikinciform.Show();
        }
    }
    }

