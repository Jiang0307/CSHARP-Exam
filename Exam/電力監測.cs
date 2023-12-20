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

namespace Exam
{
    public partial class 電力監測 : UserControl
    {
        public 電力監測()
        {
            InitializeComponent();
        }

        private void 電力監測_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|Test.mdf;" +
                    "Integrated Security=True";
                SqlDataAdapter daCT = new SqlDataAdapter("GetCTData", cn);
                DataSet ds = new DataSet();
                daCT.Fill(ds, "各CT即時電量數據資料表");
                // ComboBox控制項資料繫結
                comboBox1.DataSource = ds;
                comboBox1.DisplayMember = "各CT即時電量數據資料表.機台編號";
                comboBox2.DataSource = ds;
                comboBox2.DisplayMember = "各CT即時電量數據資料表.比流器編號";
                // DataGridView控制項資料繫結
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "各CT即時電量數據資料表";
            }
        }
    }
}
