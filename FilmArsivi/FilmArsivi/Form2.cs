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

namespace FilmArsivi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-K8PC86I\\SQLEXPRESS;Initial Catalog=FilmArsiv;Integrated Security=True");

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Listele
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT * FROM Arsiv";
            komut.Connection = baglanti;

            SqlDataAdapter adaptor = new SqlDataAdapter(komut);
            DataTable tablo = new DataTable();

            adaptor.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void button2_Click(object sender, EventArgs e) //Sil
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            string cellValue = Convert.ToString(selectedRow.Cells["id"].Value);

            String sorgu = $"Delete From Arsiv Where id= {cellValue}";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Film Silindi");

        }

        int i = 0;

        private void button3_Click(object sender, EventArgs e) //Düzenle
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            string cellValue = Convert.ToString(selectedRow.Cells["id"].Value);

            string kayıtduzenle = ($"Update Arsiv  Set FilmAdı=@FilmAdı, FilmSuresi=@FilmSuresi, FilmTuru=@FilmTuru, Yonetmen=@Yonetmen, Imdb=@Imdb, Konu=@Konu where id={cellValue}");

            SqlCommand komut = new SqlCommand( kayıtduzenle, baglanti);
            komut.Parameters.AddWithValue("@FilmAdı", textBox1.Text);
            komut.Parameters.AddWithValue("@FilmSuresi", textBox2.Text);
            komut.Parameters.AddWithValue("@FilmTuru", textBox3.Text);
            komut.Parameters.AddWithValue("@Yonetmen", textBox4.Text);
            komut.Parameters.AddWithValue("@Imdb", textBox5.Text);
            komut.Parameters.AddWithValue("@Konu", textBox6.Text);
            


            baglanti.Open();
            komut.ExecuteNonQuery();
            MessageBox.Show("Düzenlendi");
            baglanti.Close();
            

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();

        }
    }
}
