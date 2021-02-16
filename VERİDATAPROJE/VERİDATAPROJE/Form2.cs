using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VERİDATAPROJE
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string conString = "Data source = ORA_DEV_ODSDV ; user Id = egitim ; Password= egitim1 ";
                OracleConnection baglantim = new OracleConnection(conString);
                baglantim.Open();
                String Veri = richTextBox1.Text;
                string sorgu = "  " + Veri;

                OracleCommand komut = new OracleCommand(sorgu, baglantim);
                komut.ExecuteNonQuery();

                OracleDataAdapter da = new OracleDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglantim.Close();
                MessageBox.Show("Kayıt getirildi.");
            }

            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
      }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.AutoSize = false;
            label1.Width = 450;
            label1.Height = 3;
            label1.BorderStyle = BorderStyle.Fixed3D;

            foreach (System.Net.IPAddress adres in System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()))
            {

                label4.Text = " " + adres;
            }
            // saat label
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }
    }
}
