using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VERİDATAPROJE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        OracleConnection baglanti;         // combobox bölümünün
        OracleCommand Oraclekomut;
        OracleDataAdapter da;
        DataSet ds;  
                                         //  kayıt getir bölümünün
        static string conString = "Data source = ORA_DEV_ODSDV ; user Id = egitim ; Password= egitim1 ";
        OracleConnection connection = new OracleConnection(conString);

        private void button1_Click(object sender, EventArgs e)   // Veritabanı Kaynak kodun getirilmesi
        {
            try
            {
                string connectionString = "Data source = ORA_DEV_ODSDV ; user Id = egitim ; Password= egitim1";
                OracleConnection connection = new OracleConnection(connectionString);

                connection.Open();
              
                string tablo = textBox1.Text;
               // string Şema = textBox3.Text;                                                                        //KAYDI TEXTBOXTAN ALIP ONA GÖRE LİSTELEME
                string sorgu = "SELECT REPLACE(dbms_metadata.get_ddl(object_type => 'TABLE',name => '" + tablo + "',schema => '' ),'TBS_TMPUSER','TBS_DSUSER' ) from dual ";
                
                // object clob -->>    select dbms_metadata.get_ddl(object_type => 'PROCEDURE',name => 'ADD_JOB_HISTORY',schema => 'EGITIM') FROM DUAL

                OracleCommand command = new OracleCommand();
                command.CommandText = sorgu;
                command.Connection = connection;

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;

                DataTable dtListe = new DataTable();
                adapter.Fill(dtListe);
                dataGridView1.DataSource = dtListe;

                connection.Close();
                MessageBox.Show("İşlem Başarılı!");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)          // Veritabanı  tabloya ALGORİTMA çekme STAJER1 BAGLI
        {
          kayitGetir();
        }
        private void kayitGetir()
        {

            connection.Open();
            // string tablo = textBox1.Text;     // KAYDI TEXTBOXTAN ALMA
            string tablo = textBox1.Text;
            string kayit = "SELECT dbms_metadata.get_ddl(object_type => 'PROCEDURE', name => '" + tablo + "', schema => '') from dual ;  ";
            OracleCommand komut = new OracleCommand(kayit, connection);
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
         }

        private void button3_Click(object sender, EventArgs e)                              // Taşıma işlemi - veritabına gönderme 
        {
            try
            {
                string conString = "Data source = ORA_TST_ODSTEST ; user Id = egitim ; Password= egitim1 ";
                OracleConnection baglantim = new OracleConnection(conString);

                baglantim.Open();

                String Veri = richTextBox1.Text;
                string sorgu = " " + Veri;

                OracleCommand komut = new OracleCommand(sorgu, baglantim);
                komut.ExecuteNonQuery();
                richTextBox1.Clear();

                MessageBox.Show("Taşıma İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
                
            }

        }
        private void Form1_Load(object sender, EventArgs e)                         
        {

          /*// combobox veri çekme index
            OracleConnection verialma = new OracleConnection();
            verialma.ConnectionString = "Data source = ORA_DEV_ODSDV ; user Id = egitim ; Password= egitim1";
            OracleCommand kmt = new OracleCommand();
            kmt.CommandText = "  SELECT * FROM all_indexes ";
            kmt.Connection = verialma;
            kmt.CommandType = CommandType.Text;

            OracleDataReader dy;
            verialma.Open();
            dy = kmt.ExecuteReader();
            while (dy.Read())
            {
                comboBox2.Items.Add(dy["INDEX_NAME"]);
            }
            verialma.Close();
            */


            //  TABLO COMBOBOX
            OracleCommand kmt = new OracleCommand();
            kmt.CommandText = "   SELECT * FROM USER_TABLES   ";
            kmt.Connection = connection;
            kmt.CommandType = CommandType.Text;

            OracleDataReader dy;
            connection.Open();
            dy = kmt.ExecuteReader();
            while (dy.Read())
            {
                comboBox1.Items.Add(dy["TABLE_NAME"]);
            }
            connection.Close();

///////////////////////////////////////////////////////////////////////////////////////////////////

            // COMBOBOX SEÇENEK BÖLÜMÜ TABLOLAR
            
          /*  baglanti = new OracleConnection("Data source = ORA_DEV_ODSDV ; user Id = egitim ; Password= egitim1 ");
            baglanti.Open();
            DataTable dt = baglanti.GetSchema("tables");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i]["TABLE_NAME"]);

            }
            baglanti.Close();*/


         /*   //  index göre arama combobox2
            baglanti = new OracleConnection("Data source = ORA_DEV_ODSDV ; user Id = egitim ; Password= egitim1 ");
            baglanti.Open();
            DataTable dr = baglanti.GetSchema("indexes");

            for (int i = 0; i < dr.Rows.Count; i++)
            {
                comboBox2.Items.Add(dr.Rows[i]["INDEX_NAME"]);

            }
            baglanti.Close();*/



            // İNDEX COMBOBOX
            OracleCommand km = new OracleCommand();
            km.CommandText = "   SELECT * FROM USER_INDEXES   ";
            km.Connection = connection;
            km.CommandType = CommandType.Text;

            OracleDataReader dn;
            connection.Open();
            dn = km.ExecuteReader();
            while (dn.Read())
            {
                comboBox2.Items.Add(dn["INDEX_NAME"]);
            }
            connection.Close();

            //  TABLO COMBOBOX
            OracleCommand kt = new OracleCommand();
            kt.CommandText = "   SELECT * FROM USER_OBJECTS WHERE OBJECT_TYPE='PROCEDURE'   ";
            kt.Connection = connection;
            kt.CommandType = CommandType.Text;

            OracleDataReader dc;
            connection.Open();
            dc = kt.ExecuteReader();
            while (dc.Read())
            {
                comboBox4.Items.Add(dc["OBJECT_NAME"]);
            }
            connection.Close();

            ///////////////////////////////////////////////////////////////////////
            // Label ile araya çizgi çekmek gird ile textbox arasına 
            label5.AutoSize = false;
            label5.Width = 3;
            label5.Height = 90;
            label5.BorderStyle = BorderStyle.Fixed3D;

            label6.AutoSize = false;
            label6.Width = 3;
            label6.Height = 90;
            label6.BorderStyle = BorderStyle.Fixed3D;

            label7.AutoSize = false;
            label7.Width = 950;
            label7.Height = 3;
            label7.BorderStyle = BorderStyle.Fixed3D;

            //////////////////  çixgi ekleme ////////

            /// ıp adres gösterme

            foreach (System.Net.IPAddress adres in System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()))
            {
                  label8.Text = " " + adres;
            }
            // saat label
            timer1.Enabled = true;
         }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)  // GridView deki verinin textboxa geçirilmesi
        {
          int secili = dataGridView1.SelectedCells[0].RowIndex; 
          richTextBox1.Text = dataGridView1.Rows[secili].Cells[0].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)   // veri arama
        {

          /*  OracleConnection baglantim = new OracleConnection("Data source = mdmDEVSTAJ ; user Id = stajer1 ; Password= 123456");
            baglantim.Open();
            int srg = int.Parse(textBox1.Text);
            string sorgu =  "Select * from COMPUTERS WHERE COMPUTER_NAME LIKE ('"  + srg +   "')";
            OracleDataAdapter adap = new OracleDataAdapter(sorgu,baglantim);
            DataSet ds = new DataSet();
            adap.Fill(ds, "COMPUTERS");
            this.dataGridView1.DataSource = ds.Tables[0];
            baglantim.Close();  */
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.CharacterCasing = CharacterCasing.Upper;

        /* OracleConnection baglantim = new OracleConnection("Data source = mdmDEVSTAJ ; user Id = stajer1 ; Password= 123456");
           OracleDataAdapter  da = new OracleDataAdapter("SELECT * FROM COMPUTERS  WHERE COMPUTER_NAME LİKE '" + textBox1.Text +   "&'",  baglantim);
           DataSet ds = new DataSet();
           baglantim.Open();
           da.Fill(ds, "COMPUTERS");
           dataGridView1.DataSource = ds.Tables["COMPUTERS"];
           baglantim.Close(); */
            }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)      // VERİ ARAMA
        { 
            /* connection.Open();
            DataTable tbl = new DataTable();
            string vara, cumle;
            vara = textBox1.Text;
            cumle = "SELECT * from COMPUTERS where COMPUTER_NAME like '&" + textBox1.Text + "&' ";
            OracleDataAdapter adptr = new OracleDataAdapter(cumle, connection);
            adptr.Fill(tbl);
            connection.Close();
            dataGridView1.DataSource = tbl;  */
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)      // COMBOBOX1 SEÇENEK BÖLÜMÜ
        {
            try
            {
                 string conString = "Data source = ORA_DEV_ODSDV ; user Id = egitim ; Password= egitim1 ";
                 OracleConnection connection = new OracleConnection(conString);

                string tablo = comboBox1.Text;
                string sorgu = "select * from " + tablo;
                da = new OracleDataAdapter(sorgu, connection);
                ds = new DataSet();
                connection.Open();
                da.Fill(ds, tablo);
                dataGridView1.DataSource = ds.Tables[tablo];
                connection.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)     // COMBOBOX2 SEÇENEK BÖLÜMÜ
        {}

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {}

        private void button4_Click_1(object sender, EventArgs e)    // Tablo silme işlemi
        {
            try
            {
                string connectionString = "Data source = ORA_TST_ODSTEST ; user Id = egitim ; Password= egitim1";
                OracleConnection connection = new OracleConnection(connectionString);

                connection.Open();

                OracleCommand tbl = new OracleCommand(string.Format("DROP TABLE {0}", textBox1.Text), connection);
                tbl.ExecuteNonQuery();
                richTextBox1.Clear();

                connection.Close();
                MessageBox.Show("Silme İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)  // Forma eklenen zaman tarih 
        {
            label10.Text = DateTime.Now.ToString();
        }

        private void button5_Click(object sender, EventArgs e)   // index verilerini datagride yazdırma butonu
        {
            string connectionString = "Data source = ORA_DEV_ODSDV ; user Id = egitim ; Password= egitim1";
            OracleConnection connectionn = new OracleConnection(connectionString);
            connectionn.Open();

            string kayit = "select * from all_indexes ";
            OracleCommand komut = new OracleCommand(kayit, connectionn);
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connectionn.Close();
        }
        
        private void button6_Click(object sender, EventArgs e)   // Form2 yi açma butonu
        {
            Form2 ff = new Form2();
            ff.Show();
        }

        private void button7_Click(object sender, EventArgs e)   // BAğlantılı tabloları listeleme butonu
        {
            Form3 ff = new Form3();
            ff.Show();
        }
        
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////

        private void button8_Click(object sender, EventArgs e)   // UPDATE İŞLEMİ BUTONU
        {
            try
            {
                string connectionString = "Data source = ORA_TST_ODSTEST ; user Id = egitim ; Password= egitim1";
                OracleConnection connection = new OracleConnection(connectionString);

                connection.Open();

                OracleCommand tbl = new OracleCommand(string.Format("ALTER TABLE {0} RENAME TO {0}YEDEK", textBox1.Text), connection);
                tbl.ExecuteNonQuery();

                connection.Close();
                MessageBox.Show("RENAME İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
                connection.Open();

                OracleCommand tbl = new OracleCommand(string.Format("ALTER TABLE {0}YEDEK  RENAME TO {0} ", textBox1.Text), connection);
                tbl.ExecuteNonQuery();

                connection.Close();
                this.Close();
                throw hata;
            }
            try
            {
                string conString = "Data source = ORA_TST_ODSTEST ; user Id = egitim ; Password= egitim1 ";
                OracleConnection baglantim = new OracleConnection(conString);
                baglantim.Open();

                String Veri = richTextBox1.Text;
                string sorgu = "  " + Veri;

                OracleCommand komut = new OracleCommand(sorgu, baglantim);
                komut.ExecuteNonQuery();
                richTextBox1.Clear();

                MessageBox.Show("Taşıma İşlemi Gerçekleşti.");
            }
            catch (Exception hataM)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hataM.Message);
                connection.Open();

                OracleCommand tbl = new OracleCommand(string.Format("ALTER TABLE {0}YEDEK  RENAME TO {0}", textBox1.Text), connection);
                tbl.ExecuteNonQuery();

                connection.Close();
                this.Close();
                throw hataM;
            }

            try
            {
                string connectionString = "Data source = ORA_TST_ODSTEST ; user Id = egitim ; Password= egitim1";
                OracleConnection connection = new OracleConnection(connectionString);

                connection.Open();

                OracleCommand tbl = new OracleCommand(string.Format("INSERT INTO {0} SELECT * FROM {0}YEDEK COMMİT", textBox1.Text), connection);
                tbl.ExecuteNonQuery();

                connection.Close();
                MessageBox.Show("İNSERT İşlemi Gerçekleşti.");
            }
            catch (Exception hataMes)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hataMes.Message);

                connection.Open();

                OracleCommand tbl = new OracleCommand(string.Format("drop TABLE {0}YEDEK", textBox1.Text), connection);
                tbl.ExecuteNonQuery();

                connection.Close();
                this.Close();
                throw hataMes;
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hataMes.Message);

            }
            try
            {
                string connectionString = "Data source = ORA_TST_ODSTEST ; user Id = egitim ; Password= egitim1";
                OracleConnection connection = new OracleConnection(connectionString);

                connection.Open();
                OracleCommand tbl = new OracleCommand(string.Format("drop TABLE {0}YEDEK", textBox1.Text), connection);
                tbl.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("SİLME İşlemi Gerçekleşti.");
            }
            catch (Exception hataS)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hataS.Message);
                connection.Open();

                OracleCommand tbl = new OracleCommand(string.Format("ALTER TABLE {0}YEDEK  RENAME TO {0}", textBox1.Text), connection);
                tbl.ExecuteNonQuery();

                connection.Close();
                this.Close();
                throw hataS;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string connectionString = "Data source = ORA_DEV_ODSDV ; user Id = egitim ; Password= egitim1";
            OracleConnection connectionn = new OracleConnection(connectionString);
            connectionn.Open();

            string kayit = "select * from all_tables ";
            OracleCommand komut = new OracleCommand(kayit, connectionn);
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connectionn.Close();
        }
    }  
       }

        
    


           
        
        




        
    

