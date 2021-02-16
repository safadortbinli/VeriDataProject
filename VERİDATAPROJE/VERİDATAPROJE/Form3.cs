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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string connectionString = "Data source = ORA_TST_ODSTEST ; user Id = egitim ; Password= egitim1";
            OracleConnection connectionn = new OracleConnection(connectionString);
            connectionn.Open();

            string kayit = "select * from dba_constraints  where constraint_name like '%FK'  ";
            OracleCommand komut = new OracleCommand(kayit, connectionn);
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connectionn.Close();
        }
    }
}
