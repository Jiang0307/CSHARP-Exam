using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(60, 70, 80);
            toolStrip1.BackColor = Color.FromArgb(60,70,80);

        }

        private void 產能效率_toolStripButton_Click(object sender, EventArgs e)
        {
            產能效率 new_UI = new 產能效率();
            panel1.Controls.Clear();
            panel1.Controls.Add(new_UI);
        }

        private void 機台監測_toolStripButton_Click(object sender, EventArgs e)
        {
            機台監測 new_UI = new 機台監測();
            panel1.Controls.Clear();
            panel1.Controls.Add(new_UI);
        }

        private void 電力監測_toolStripButton_Click(object sender, EventArgs e)
        {
            電力監測 new_UI = new 電力監測();
            panel1.Controls.Clear();
            panel1.Controls.Add(new_UI);
        }

        private void 預防保養_toolStripButton_Click(object sender, EventArgs e)
        {
            預防保養 new_UI = new 預防保養(panel1);
            panel1.Controls.Clear();
            panel1.Controls.Add(new_UI);
        }

        private void 生產總表_toolStripButton_Click(object sender, EventArgs e)
        {
            生產總表 new_UI = new 生產總表();
            panel1.Controls.Clear();
            panel1.Controls.Add(new_UI);
        }

        private void user_toolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void exit_toolStripButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
