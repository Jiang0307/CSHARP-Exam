using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam
{
    public partial class 預防保養歷史記錄 : UserControl
    {
        string selected_id;
        public 預防保養歷史記錄(string id)
        {
            InitializeComponent();
            selected_id = id;
        }


        private void 預防保養歷史記錄_Load(object sender, EventArgs e)
        {
            //label2.Text = selected_id;
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";
                cn.Open();

                SqlCommand cmd = new SqlCommand($"SELECT 時間, 編號, 保養人, 料號, 備註 FROM 設備保養歷史清單 WHERE 編號 = '{selected_id}'", cn);
                SqlDataReader dr = cmd.ExecuteReader();

                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (i == 0)
                        richTextBox1.Text += "".PadRight(15); // 空白填充

                    richTextBox1.Text += dr.GetName(i).PadRight(30); // 調整填充寬度
                }
                richTextBox1.Text += "\n-------------------------------------------------------------------------------------------------------------------\n";

                while (dr.Read())
                {
                    string temp = dr["時間"].ToString();
                    int spaceIndex = temp.IndexOf(' ');
                    string formattedDate = temp.Substring(0, spaceIndex);

                    richTextBox1.Text += formattedDate.PadRight(30); // 調整填充寬度
                    richTextBox1.Text += dr["編號"].ToString().PadRight(30);
                    richTextBox1.Text += dr["保養人"].ToString().PadRight(30);
                    richTextBox1.Text += dr["料號"].ToString().PadRight(30);
                    richTextBox1.Text += dr["備註"].ToString() + "\n";
                }
            }
        }
    }
}