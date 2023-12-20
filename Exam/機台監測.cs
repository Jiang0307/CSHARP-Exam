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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Exam
{
    public partial class 機台監測 : UserControl
    {
        public 機台監測()
        {
            InitializeComponent();
        }

        private void 機台監測_Load(object sender, EventArgs e)
        {
            //dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Red;

            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";
                SqlDataAdapter daCT = new SqlDataAdapter("GetMonitorData", cn);
                DataSet ds = new DataSet();
                daCT.Fill(ds, "機台監測資料表");
                comboBox1.DataSource = ds;
                comboBox1.DisplayMember = "機台監測資料表.機台編號";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";
                SqlDataAdapter daCT = new SqlDataAdapter("GetMonitorData", cn);
                DataSet ds = new DataSet();
                daCT.Fill(ds, "機台監測資料表");

                // 確保選中的 ComboBox 項目不為空
                if (comboBox1.SelectedItem is DataRowView selectedRow)
                {
                    // 獲取機台編號
                    string selectedMachineId = selectedRow["機台編號"].ToString();

                    // 使用機台編號找到對應的運作狀態
                    DataRow[] rows = ds.Tables["機台監測資料表"].Select($"機台編號 = '{selectedMachineId}'");

                    // 確保找到了相應的資料
                    if (rows.Length > 0)
                    {
                        // 設定 label8 的 Text 為對應的運作狀態
                        label8.Text = rows[0]["運作狀態"].ToString();
                        label7.Text = rows[0]["機台稼動率"].ToString() + "%";
                    }
                }
            }

            label4.Text = comboBox1.Text + "(機台型號)";
            if (comboBox1.Text[0] == 'A')
            {
                pictureBox1.Image = new Bitmap("Machine-A.png");
            }
            if (comboBox1.Text[0] == 'B')
            {
                pictureBox1.Image = new Bitmap("Machine-B.png");
            }
            if (comboBox1.Text[0] == 'C')
            {
                pictureBox1.Image = new Bitmap("Machine-C.png");
            }

            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";

                // 創建 SqlCommand 對象，並指定使用的儲存過程
                SqlCommand cmd = new SqlCommand("GetMonitorData2", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // 添加輸入參數，這裡假設 comboBox1 存在並且是用來顯示機台編號的
                cmd.Parameters.Add("@SelectedMachineId", SqlDbType.NVarChar, 50).Value = comboBox1.Text;

                // 創建 SqlDataAdapter 並填充 DataSet
                SqlDataAdapter daCT = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                daCT.Fill(ds, "機台監測資料表2");

                // 將 DataSet 中的資料繫結到 DataGridView
                dataGridView1.DataSource = ds.Tables["機台監測資料表2"];
            }

            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|Test.mdf;" +
                    "Integrated Security=True";

                // 創建 SqlCommand 對象，並指定使用的儲存過程
                SqlCommand cmd = new SqlCommand("GetMonitorData3", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // 添加輸入參數，這裡假設 comboBox1 存在並且是用來顯示機台編號的
                cmd.Parameters.Add("@SelectedMachineId", SqlDbType.NVarChar, 50).Value = comboBox1.Text;

                // 創建 SqlDataAdapter 並填充 DataSet
                SqlDataAdapter daCT = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                daCT.Fill(ds, "機台監測資料表3");

                // 將 DataSet 中的資料繫結到 DataGridView
                dataGridView2.DataSource = ds.Tables["機台監測資料表3"];
            }
        }
    }
}
