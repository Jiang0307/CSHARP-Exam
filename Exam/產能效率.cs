using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using System.Collections;

namespace Exam
{

    public partial class 產能效率 : UserControl
    {
        public 產能效率()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet();
        List<Machine> allMachines = new List<Machine>();

        private void 產能效率_Load(object sender, EventArgs e)
        {
            Dictionary<string, int> statusCounts = new Dictionary<string, int> { {"正常生產", 0}, { "待單停機", 0}, {"待料中", 0}, {"維修中", 0}, { "故障", 0}};
            
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";
            //==========================================

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 使用 SqlCommand 執行存儲過程
                using (SqlCommand command = new SqlCommand("GetLatest", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // 使用 SqlDataAdapter 來填充一個 DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable resultTable = new DataTable();
                        adapter.Fill(resultTable);

                        // 處理結果集
                        foreach (DataRow row in resultTable.Rows)
                        {
                            string machineNumber = row["機台編號"].ToString();
                            string operationStatus = row["運作狀態"].ToString();

                            //Console.WriteLine($"機台編號: {row["機台編號"].ToString()}, 運作狀態: {row["運作狀態"].ToString()}");
                            
                            // 更新 Dictionary 的計數
                            if (statusCounts.ContainsKey(operationStatus))
                                statusCounts[operationStatus]++;
                            
                            Machine info = new Machine();
                            info.MachineNumber = machineNumber;
                            info.MachineStatus = operationStatus;
                            info.MachineId = row["Id"].ToString();
                            info.MachineDateTime = DateTime.Parse(row["時間"].ToString());
                            info.MachineProductionNumber = row["製令單號"].ToString();

                            if (machineNumber[0] == 'A')
                                info.MachineImage = Properties.Resources.Machine_A;
                            else if (machineNumber[0] == 'B')
                                info.MachineImage = Properties.Resources.Machine_B;
                            else if (machineNumber[0] == 'C')
                                info.MachineImage = Properties.Resources.Machine_C;

                            allMachines.Add(info);
                        }
                    }
                }
            }
            正常生產_button2.Text = statusCounts["正常生產"].ToString();
            待單停機_button2.Text = statusCounts["待單停機"].ToString();
            待料中_button2.Text = statusCounts["待料中"].ToString();
            維修中_button2.Text = statusCounts["維修中"].ToString();
            故障停機_button2.Text = statusCounts["故障"].ToString();
        }
        private Image ResizeImage(Image image, int width, int height)
        {
            // 使用 Graphics 類別來伸縮圖片
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(image, 0, 0, width, height);
            }
            return result;
        }

        private string get_time(string original)
        {
            string pattern = @"\b\d{2}:\d{2}:\d{2}\b";
            Match match = Regex.Match(original, pattern);
            string result = match.Value;
            return result;
        }

        private string get_目標生產數量(string machineNumber) 
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL 查詢
                string query = "SELECT 目標數量 FROM 生產製造心跳表 WHERE 機台編號 = @MachineNumber";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 使用參數化查詢以防 SQL 注入
                    command.Parameters.AddWithValue("@MachineNumber", machineNumber);

                    // 執行查詢
                    object result = command.ExecuteScalar();
                    return result.ToString();
                }
            }

        }

        // 更新 [生產製造心跳表] 的函數
        static void UpdateProductionHeartbeatTable(string machineNumber, int totalProduction)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";
            // SQL 更新語句
            string updateQuery = @"
            UPDATE
                [生產製造心跳表]
            SET
                實際數量 = @TotalProduction
            WHERE
                機台編號 = @MachineNumber;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    connection.Open();
                    // 使用參數化查詢以防 SQL 注入
                    updateCommand.Parameters.AddWithValue("@MachineNumber", machineNumber);
                    updateCommand.Parameters.AddWithValue("@TotalProduction", totalProduction);



                    // 執行更新語句
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    /*
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"機台 {machineNumber} 的總生產數量已更新為 {totalProduction}");
                    }
                    else
                    {
                        Console.WriteLine($"機台 {machineNumber} 更新失敗");
                    }*/
                }
            }
        }

        private string get_實際生產數量(string machineNum)
        {
            string output = "";
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL 查詢，獲取每個機台的總生產數量
                string selectQuery = @"
                SELECT
                    機台編號,
                    SUM(生產數量) AS 實際生產數量
                FROM
                    [即時生產資訊表]
                GROUP BY
                    機台編號;"
                ;

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string machineNumber = reader["機台編號"].ToString();
                            int totalProduction = Convert.ToInt32(reader["實際生產數量"]);
                            // 更新到 [生產製造心跳表]
                            UpdateProductionHeartbeatTable(machineNumber, totalProduction);
                        }
                    }
                }

                string selectQuery2 = $"SELECT 實際數量 FROM [生產製造心跳表] WHERE 機台編號 = '{machineNum}'";
                using (SqlCommand command = new SqlCommand(selectQuery2, connection))
                {
                    // 執行查詢，並取得實際生產數量
                    object result = command.ExecuteScalar();
                    output = result.ToString();
                }
            }
            return output;
        }

        private string get_製令生產進度(string machineNum)
        {
            double targetProduction = 0;
            double actualProduction = 0;

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL 查詢，取得目標生產數量和實際生產數量
                string query = $"SELECT 目標數量, 實際數量 FROM [生產製造心跳表] WHERE 機台編號 = '{machineNum}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 執行查詢，並取得目標生產數量和實際生產數量
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // 檢查目標生產數量和實際生產數量是否不為 DBNull.Value，並轉換為數字型別
                            targetProduction = reader["目標數量"] != DBNull.Value ? Convert.ToDouble(reader["目標數量"]) : 0;
                            actualProduction = reader["實際數量"] != DBNull.Value ? Convert.ToDouble(reader["實際數量"]) : 0;
                        }
                    }
                }
            }

            // 防止分母為零的情況，避免除以零錯誤
            if (actualProduction != 0)
            {
                double productionProgress = actualProduction / targetProduction * 100;
                productionProgress = Math.Round(productionProgress, 1); // 四捨五入到小數點後一位
                return productionProgress.ToString()+"%";
            }
            else
            {
                return "0%";
            }
        }
        
        private DateTime GetNowTimeFromRealtimeProductionInfoTable(string machineNumber, SqlConnection connection)
        {
            // SQL 查詢，取得即時生產資訊表中的現在時間
            string query = $"SELECT TOP 1 時間 FROM [即時生產資訊表] WHERE 機台編號 = '{machineNumber}' ORDER BY 時間 DESC";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // 執行查詢，並取得最新的時間
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDateTime(result);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        private string get_產線生產效率(string machineNumber)
        {
            // 取得機台的實際生產數量、開始生產時間和生產週期時間
            double actualProduction = 0;
            DateTime startProductionTime = DateTime.MinValue;
            double productionCycleTime = 0;
            double productionEfficiency = 0;
            string result = "";

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Test.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL 查詢，取得機台的生產相關資訊
                string query = $"SELECT 實際數量, 開始生產時間, 生產週期 FROM [生產製造心跳表] WHERE 機台編號 = '{machineNumber}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 執行查詢，並取得相關資訊
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // 檢查欄位是否存在並轉換為數字型別
                            actualProduction = reader["實際數量"] != DBNull.Value ? Convert.ToDouble(reader["實際數量"]) : 0;
                            startProductionTime = reader["開始生產時間"] != DBNull.Value ? Convert.ToDateTime(reader["開始生產時間"]) : DateTime.MinValue;
                            productionCycleTime = reader["生產週期"] != DBNull.Value ? Convert.ToDouble(reader["生產週期"]) : 0;
                        }
                    }
                }

                // 取得現在的時間
                DateTime now = GetNowTimeFromRealtimeProductionInfoTable(machineNumber, connection);

                if (actualProduction > 0 && startProductionTime != DateTime.MinValue && productionCycleTime > 0 && now != DateTime.MinValue)
                {
                    // 計算生產週期倍數
                    double productionCycleMultiplier = (now - startProductionTime).TotalSeconds / productionCycleTime;

                    // 計算產線生產效率
                    productionEfficiency = actualProduction / productionCycleMultiplier*100;
                    productionEfficiency = Math.Round(productionEfficiency, 2); // 四捨五入到小數點後一位

                    if(productionEfficiency < 0.1)
                    {
                        Double temp = 0.00;
                        result = temp.ToString() + "%";
                    }
                    else
                        result = productionEfficiency.ToString()+"%";
                }
                else
                {
                    Double temp = 0.00;
                    result = temp.ToString()+"%";
                }

                string updateQuery = @"UPDATE [生產製造心跳表] SET 生產效率 = @ProductionEfficiency WHERE 機台編號 = @MachineNumber;";
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    // 使用參數化查詢以防 SQL 注入
                    updateCommand.Parameters.AddWithValue("@MachineNumber", machineNumber);
                    updateCommand.Parameters.AddWithValue("@ProductionEfficiency", productionEfficiency/100);

                    // 執行更新語句
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    /*
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"機台 {machineNumber} 的生產效率已更新為 {result}");
                    }
                    else
                    {
                        Console.WriteLine($"機台 {machineNumber} 更新生產效率失敗");
                    }*/
                }
                return result;
            }
        }

        static int GetGoodQuantity(string machineNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Test.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT SUM(良品數量) FROM 即時生產品質記錄表 WHERE 機台編號 = '{machineNumber}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 執行查詢，並取得結果
                    object result = command.ExecuteScalar();

                    // 如果結果不為 DBNull，轉換為整數返回
                    return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }
        }

        static int GetTotalQuantity(string machineNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Test.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 假設 Quality 表有總數量的欄位為 TotalQuantity，且有 機台編號 欄位
                string query = $"SELECT 實際數量 FROM 生產製造心跳表 WHERE 機台編號 = '{machineNumber}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 執行查詢，並取得結果
                    object result = command.ExecuteScalar();

                    // 如果結果不為 DBNull，轉換為整數返回
                    return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }
        }

        static void UpdateYieldRate(string machineNumber, double yieldRate)
        {
            yieldRate = Convert.ToDouble(yieldRate);
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Test.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 假設 Heartbeat 表有直通率的欄位為 YieldRate，且有 MachineNumber 欄位
                string query = $"UPDATE 生產製造心跳表 SET 直通率 = '{yieldRate}' WHERE 機台編號 = '{machineNumber}'";
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // 執行更新
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex) 
                {

                }
            }
        }
        
        private string get_產品直通率(string machineNum)
        {
            int goodQuantity = GetGoodQuantity(machineNum);
            int totalQuantity = GetTotalQuantity(machineNum);

            double yieldRateValue = (double)goodQuantity / totalQuantity;
            string yieldRate = yieldRateValue.ToString("P2"); // 格式化為百分比字串


            UpdateYieldRate(machineNum, yieldRateValue);

            return yieldRate;
        }

        private void UpdateMachineUtilization(string machineNumber, double machineUtilization)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Test.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE [生產製造心跳表] SET 機台稼動率 = @MachineUtilization WHERE 機台編號 = @MachineNumber";

                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@MachineUtilization", machineUtilization/100);
                    updateCommand.Parameters.AddWithValue("@MachineNumber", machineNumber);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        private string get_機台稼動率(string machineNum)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|Test.mdf;" + "Integrated Security=True";
                SqlDataAdapter daCT = new SqlDataAdapter("GetMonitorData", cn);
                DataSet ds = new DataSet();
                daCT.Fill(ds, "機台監測資料表");

                // 使用機台編號找到對應的運作狀態
                DataRow[] rows = ds.Tables["機台監測資料表"].Select($"機台編號 = '{machineNum}'");
                double result = double.Parse(rows[0]["機台稼動率"].ToString());
                UpdateMachineUtilization(machineNum, result);

                return result.ToString("0.00") + "%"; // 使用格式字串顯示兩位小數
            }
        }

        private double GetProductionEfficiency(string machineNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Test.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT 生產效率 FROM [生產製造心跳表] WHERE 機台編號 = '{machineNumber}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDouble(result) : 0.0;
                }
            }
        }

        private double GetPassRate(string machineNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Test.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT 直通率 FROM [生產製造心跳表] WHERE 機台編號 = '{machineNumber}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDouble(result) : 0.0;
                }
            }
        }

        private double GetMachineUtilization(string machineNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Test.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT 機台稼動率 FROM [生產製造心跳表] WHERE 機台編號 = '{machineNumber}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDouble(result) : 0.0;
                }
            }
        }

        private string get_OEE(string machineNum)
        {
            double productionEfficiency = GetProductionEfficiency(machineNum);
            double passRate = GetPassRate(machineNum);
            double machineUtilization = GetMachineUtilization(machineNum);
            double oee = productionEfficiency * passRate * machineUtilization;

            string result = oee.ToString("P2"); // 格式化為百分比字串
            return result;
        }

        private void add_to_panel(List<Machine> Machines, int squareSize, Color color)
        {
            // 清空 flowLayoutPanel1 中的所有控制項
            flowLayoutPanel1.Controls.Clear();

            foreach (var Machine in Machines)
            {
                // 建立 Panel 用於包含 PictureBox、Label 和 RichTextBox
                Panel panel = new Panel();
                panel.Dock = DockStyle.Top;
                panel.AutoSize = true;

                // 建立 PictureBox 顯示機台圖片
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = ResizeImage(Machine.MachineImage, squareSize, squareSize);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Width = 300;
                pictureBox.Height = pictureBox.Height * 3;
                pictureBox.Dock = DockStyle.Top;  // 將 PictureBox 放在 Panel 的上方

                // 建立 Label 顯示機台編號
                System.Windows.Forms.Label label = new System.Windows.Forms.Label();
                label.Text = $"機台型號 : {Machine.MachineNumber}";
                label.Font = new Font("微軟正黑體", 14, FontStyle.Bold);
                label.BackColor = Color.FromArgb(60, 70, 80);
                label.ForeColor = Color.White;
                label.TextAlign = ContentAlignment.MiddleCenter; // 文字置中
                label.AutoSize = true; // 自動調整大小以適應文字內容
                label.Top = pictureBox.Bottom + 20;
                label.Left += 40;

                // 建立 RichTextBox 顯示機台狀態
                string info = $"機台目前狀態 : \t{Machine.MachineStatus}\n";
                info += $"生產製令單號 : \t{Machine.MachineProductionNumber}\n";
                info += $"開始生產時間 : \t{get_time(Machine.MachineDateTime.ToString())}\n";
                info += $"目標生產數量 : \t{get_目標生產數量(Machine.MachineNumber.ToString())}\n";
                info += $"實際生產數量 : \t{get_實際生產數量(Machine.MachineNumber.ToString())}\n";
                info += $"製令生產進度 : \t{get_製令生產進度(Machine.MachineNumber.ToString())}\n";
                info += $"產線生產效率 : \t{get_產線生產效率(Machine.MachineNumber.ToString())}\n";
                info += $"產品直通率   : \t{get_產品直通率(Machine.MachineNumber.ToString())}\n";
                info += $"機台稼動率   : \t{get_機台稼動率(Machine.MachineNumber.ToString())}\n";
                info += $"OEE          : \t{get_OEE(Machine.MachineNumber.ToString())}\n";

                RichTextBox richTextBox = new RichTextBox();
                richTextBox.Text = info;
                richTextBox.ReadOnly = true;
                richTextBox.Width = pictureBox.Image.Width; // 設定 RichTextBox 的寬度
                richTextBox.Height = 200; // 設定 RichTextBox 的高度
                richTextBox.Top = label.Bottom + 20;
                richTextBox.BackColor = Color.FromArgb(60, 70, 80);
                richTextBox.ForeColor = Color.White;
                richTextBox.Font = new Font("微軟正黑體", 10, FontStyle.Bold);

                int index = richTextBox.Text.IndexOf(Machine.MachineStatus);
                richTextBox.Select(index, Machine.MachineStatus.Length);
                richTextBox.SelectionColor = Color.Black;
                richTextBox.SelectionBackColor = color;

                // Set the color based on conditions for each metric
                SetMetricColor(richTextBox, get_產線生產效率(Machine.MachineNumber.ToString()), 85, 80);
                SetMetricColor(richTextBox, get_產品直通率(Machine.MachineNumber.ToString()), 85, 80);
                SetMetricColor(richTextBox, get_機台稼動率(Machine.MachineNumber.ToString()), 85, 80);
                SetMetricColor(richTextBox, get_OEE(Machine.MachineNumber.ToString()), 85, 80);
                Console.WriteLine("\n");

                panel.Controls.Add(pictureBox);
                panel.Controls.Add(label);
                panel.Controls.Add(richTextBox);

                // 將 Panel 加入 flowLayoutPanel1
                flowLayoutPanel1.Controls.Add(panel);
            }
        }
        private void SetMetricColor(RichTextBox richTextBox, string metricValue, int highThreshold, int lowThreshold)
        {
            //Console.WriteLine($"metricValue : {metricValue}");

            // 找到所有匹配的索引位置
            List<int> indices = GetAllIndices(richTextBox.Text, metricValue);

            foreach (int startIndex in indices)
            {
                int length = metricValue.Length;

                // 計算"\n"的數量
                int newlineCount = richTextBox.Text.Substring(0, startIndex).Split('\n').Length - 1;

                if (startIndex >= 0 && newlineCount > 5)
                {
                    // 暫存目前選擇的文字顏色
                    Color originalColor = richTextBox.SelectionColor;

                    // 設置新的文字顏色
                    if (metricValue == "0%" || metricValue == "0.00%")
                    {
                        richTextBox.Select(startIndex, length);
                        richTextBox.SelectionColor = Color.Red;
                    }
                    else
                    {
                        metricValue = metricValue.Replace("%", "");
                        double value;

                        if (double.TryParse(metricValue, out value))
                        {
                            if (value > highThreshold)
                            {
                                richTextBox.Select(startIndex, length);
                                richTextBox.SelectionColor = Color.Green;
                            }
                            else if (value >= lowThreshold)
                            {
                                richTextBox.Select(startIndex, length);
                                richTextBox.SelectionColor = Color.Yellow;
                            }
                            else
                            {
                                richTextBox.Select(startIndex, length);
                                richTextBox.SelectionColor = Color.Red;
                            }
                        }
                    }

                    // 還原回原始的文字顏色
                    richTextBox.SelectionStart = startIndex + length;
                    richTextBox.SelectionLength = 0;
                    richTextBox.SelectionColor = originalColor;
                }
            }
        }

        private List<int> GetAllIndices(string source, string match)
        {
            List<int> indices = new List<int>();
            int index = 0;

            while ((index = source.IndexOf(match, index)) != -1)
            {
                indices.Add(index);
                index += match.Length;
            }

            return indices;
        }


        private void 正常生產(object sender, EventArgs e)
        {
            List<Machine> machine_list = allMachines.Where(m => m.MachineStatus == "正常生產").ToList();
            add_to_panel(machine_list, Properties.Resources.Machine_C.Height , 正常生產_button1.BackColor);
        }

        private void 待料中(object sender, EventArgs e)
        {
            List<Machine> machine_list = allMachines.Where(m => m.MachineStatus == "待料中").ToList();
            add_to_panel(machine_list, Properties.Resources.Machine_C.Height , 待料中_button1.BackColor);
        }
        private void 故障停機(object sender, EventArgs e)
        {
            List<Machine> machine_list = allMachines.Where(m => m.MachineStatus == "故障").ToList();
            add_to_panel(machine_list, Properties.Resources.Machine_C.Height , 故障停機_button1.BackColor);
        }
        private void 維修中(object sender, EventArgs e)
        {
            List<Machine> machine_list = allMachines.Where(m => m.MachineStatus == "維修中").ToList();
            add_to_panel(machine_list, Properties.Resources.Machine_C.Height , 維修中_button1.BackColor);
        }
        private void 待單停機(object sender, EventArgs e)
        {
            List<Machine> machine_list = allMachines.Where(m => m.MachineStatus == "待單停機").ToList();
            add_to_panel(machine_list, Properties.Resources.Machine_C.Height , 待單停機_button1.BackColor);
        }
    }

    public class Machine
    {
        public string MachineId { get; set; }
        public string MachineNumber { get; set; }
        public Image MachineImage { get; set; }
        public string MachineStatus { get; set; }
        public string MachineProductionNumber { get; set; }
        public DateTime MachineDateTime { get; set; }
    }
}
