using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Exam
{
    public partial class 預防保養 : UserControl
    {
        Panel parent_copy;
        bool change_to_history = false;
        public 預防保養(Panel parent)
        {
            InitializeComponent();
            parent_copy = parent;
        }
        DataSet ds = new DataSet();

        private void fill_comboBox()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";
            // SQL 查詢語句，假設機台編號存儲在名為 Machines 的表中
            string query = "SELECT DISTINCT 機台編號 FROM 機台健康度記錄表";

            // 創建 SqlConnection 和 SqlCommand 對象
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // 打開連接
                connection.Open();

                // 創建 SqlDataAdapter 和 DataSet，填充 ComboBox
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "機台健康度記錄表");

                // 將機台編號添加到 ComboBox 中
                id_comboBox.DataSource = dataSet.Tables["機台健康度記錄表"];
                id_comboBox.DisplayMember = "機台編號";
                id_comboBox.ValueMember = "機台編號";
            }
        }

        private void fill_table()
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";
                // 建立DataAdapter物件，用來取得員工資料表
                // 將資料表放入ds(DataSet)物件中
                string cmd = "SELECT Id,  機台編號 , ROUND(((CAST(建議保養時數 AS FLOAT) - 已運行時數) / CAST(建議保養時數 AS FLOAT)) * 100, 1) AS 機台健康度, 已運行時數, 建議保養時數 FROM 機台健康度記錄表;";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd, cn);
                adapter.Fill(ds, "機台健康度記錄表");

                dataGridView1.DataSource = ds.Tables["機台健康度記錄表"];
            }
        }

        private void 預防保養_Load(object sender, EventArgs e)
        {
            fill_comboBox();
            fill_table();
            change_to_history = true;
        }

        private void id_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(change_to_history == true)
            {
                string id = id_comboBox.SelectedValue.ToString();
                預防保養歷史記錄 new_UI = new 預防保養歷史記錄(id);
                parent_copy.Controls.Clear();
                parent_copy.Controls.Add(new_UI);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // 取得機台編號欄位的值
                string id = selectedRow.Cells["機台編號"].Value.ToString();
                預防保養歷史記錄 new_UI = new 預防保養歷史記錄(id);
                parent_copy.Controls.Clear();
                parent_copy.Controls.Add(new_UI);
            }
        }
    }
}
