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

namespace pastaneapp
{
    public partial class Form1 : Form
    {
    
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data source=DESKTOP-NH3FRG4;Initial Catalog=testmaliyet;Integrated Security=True");
        void MalzemeListe()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLMALZEMELER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void UrunListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLURUNLER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void Kasa()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLKASA", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void Malzemeler()
        {
            baglanti.Open();
            SqlDataAdapter komut = new SqlDataAdapter("Select id, AD From TBLMALZEMELER", baglanti);
            DataTable dt = new DataTable();
            komut.Fill(dt);
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "AD";
            comboBox2.DataSource = dt;
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MalzemeListe();
            Urunler();
            Malzemeler();
            //comboBox1.ValueMember = "URUNID";
            //comboBox1.DisplayMember = "AD";
            //comboBox2.ValueMember = "MALZEMEID";
            //comboBox2.DisplayMember = "AD";

        }
    
    
        void Urunler()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBLURUNLER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.ValueMember = "URUNID";
            comboBox1.DisplayMember = "AD";
            comboBox1.DataSource = dt;
            baglanti.Close();

        }
        

        private void button6_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            try
            {
                SqlCommand komut = new SqlCommand("INSERT INTO TBLFIRIN(URUNID,MALZEMEID,MIKTAR,MALIYET) VALUES (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(comboBox1.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p2", int.Parse(comboBox2.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p3", decimal.Parse(textBox13.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(textBox12.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Malzeme eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata " + ex, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            baglanti.Close();

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            UrunListesi();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Kasa();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MalzemeListe();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLMALZEMELER(AD,STOK,FIYAT,NOTLAR)VALUES(@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox2.Text);
            komut.Parameters.AddWithValue("@p2", decimal.Parse(textBox3.Text));
            komut.Parameters.AddWithValue("@p3", decimal.Parse(textBox4.Text));
            komut.Parameters.AddWithValue("@p4", textBox5.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Malzeme eklenmiştir", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MalzemeListe();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLURUNLER(AD)VALUES(@p1)", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox9.Text);
            //komut.Parameters.AddWithValue("@p2", decimal.Parse(textBox8.Text));
            //komut.Parameters.AddWithValue("@p3", decimal.Parse(textBox7.Text));
            //komut.Parameters.AddWithValue("@p4", decimal.Parse(textBox6.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Eklenmiştir", "Information", MessageBoxButtons.OK);
            UrunListesi();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            double maliyet;
            if (textBox13.Text == "")
            {
                textBox13.Text = "0";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLMALZEMELER", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label14.Text = dr[3].ToString();
            }
            baglanti.Close();
        }
    }
}
