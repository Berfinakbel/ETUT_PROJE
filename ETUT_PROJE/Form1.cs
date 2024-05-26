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
using System.IO;


namespace ETUT_PROJE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection ("Data Source=BERAT\\SQLEXPRESS;Initial Catalog=projeEtut;Integrated Security=True");

        void derslistesi ()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select  * From tbldersler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbDers.ValueMember = "DERSID";
            CmbDers.DisplayMember = "DERSAD";
            CmbDers.DataSource = dt;

        }
        // Etüt Listesi

        void etutlistesi()
        {
            SqlDataAdapter da3 = new SqlDataAdapter("execute etut",baglanti);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            dataGridView1.DataSource = dt3;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            derslistesi();
            etutlistesi();
        }

        private void CmbDers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da2 = new SqlDataAdapter("Select * From tblogretmen where bransıd=" + CmbDers.SelectedValue, baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            dt2.Columns.Add("ADSOYAD", typeof(string), "AD + ' ' + SOYAD");
            CmbOgretmen.ValueMember = "OGRETMENID";
            CmbOgretmen.DisplayMember = "ADSOYAD";
            CmbOgretmen.DataSource = dt2;

            if(CmbOgretmen.SelectedValue == null) 
            {
                CmbOgretmen.Text = "";
            }
        }

        private void BtnEtutOlustur_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tbletut(dersıd,ogretmenıd,tarıh,saat) values (@p1,@p2,@p3,@p4)",baglanti);
            komut.Parameters.AddWithValue("@p1", CmbDers.SelectedValue);
            komut.Parameters.AddWithValue("@p2", CmbOgretmen.SelectedValue);
            komut.Parameters.AddWithValue("@p3", MskTarih.Text);
            komut.Parameters.AddWithValue("@p4", MskSaat.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Etüt Oluşturuldu.","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            TxtEtutid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void BtnEtutVer_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update tbletut set ogrıd = @p1,durum = @p2 where ıd = @p3",baglanti);
            komut.Parameters.AddWithValue("@p1", TxtOgrenciid.Text);
            komut.Parameters.AddWithValue("@p2","True");
            komut.Parameters.AddWithValue("@p3", TxtEtutid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Etüt Öğrenciye Verildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void BtnOgrenciEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tblogrencı (ad, soyad, fotograf, sınıf, telefon) values (@p1,@p2,@p3,@p4,@p5)",baglanti);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", pictureBox1.ImageLocation);
            komut.Parameters.AddWithValue("@p4", TxtSinif.Text);
            komut.Parameters.AddWithValue("@p5", MskTelefon.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Kaydedildi.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnOgretmenEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tblogretmen (ad, soyad, bransıd) values (@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtOgretmenAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtOgretmenSoyad.Text);
            komut.Parameters.AddWithValue("@p3", CmbOgretmenDersAdı.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğretmen Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnDersEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tbldersler (dersad) values (@p1)",baglanti);
            komut.Parameters.AddWithValue("@p1", TxtDers.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ders Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnOgrGoruntule_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(); // Form2'yi oluşturun
            form2.Show(); // Form2'yi gösterin
            this.Hide(); // Form1'i gizleyin


        }
    }
}
