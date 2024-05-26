using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETUT_PROJE
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=BERAT\\SQLEXPRESS;Initial Catalog=projeEtut;Integrated Security=True");
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblogrenci", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView3.DataSource = dt; 
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğrencileri getirme sırasında hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
