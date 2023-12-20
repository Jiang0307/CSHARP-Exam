namespace Exam
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.產能效率 = new System.Windows.Forms.ToolStripButton();
            this.機台監測_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.電力監測_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.預防保養_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.生產總表_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.user_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.setup_toolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.新增機台_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.機台保養狀況填寫_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.產線生產細項填寫_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exit_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowDrop = true;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.產能效率,
            this.機台監測_toolStripButton,
            this.電力監測_toolStripButton,
            this.預防保養_toolStripButton,
            this.生產總表_toolStripButton,
            this.user_toolStripButton,
            this.setup_toolStripDropDownButton,
            this.exit_toolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.MinimumSize = new System.Drawing.Size(0, 80);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1111, 80);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoSize = false;
            this.toolStripLabel1.Image = global::Exam.Properties.Resources.NCKU;
            this.toolStripLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(248, 77);
            // 
            // 產能效率
            // 
            this.產能效率.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.產能效率.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.產能效率.ForeColor = System.Drawing.Color.Gold;
            this.產能效率.Image = ((System.Drawing.Image)(resources.GetObject("產能效率.Image")));
            this.產能效率.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.產能效率.Name = "產能效率";
            this.產能效率.Size = new System.Drawing.Size(150, 77);
            this.產能效率.Text = "產能效率";
            this.產能效率.Click += new System.EventHandler(this.產能效率_toolStripButton_Click);
            // 
            // 機台監測_toolStripButton
            // 
            this.機台監測_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.機台監測_toolStripButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.機台監測_toolStripButton.ForeColor = System.Drawing.Color.Gold;
            this.機台監測_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("機台監測_toolStripButton.Image")));
            this.機台監測_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.機台監測_toolStripButton.Name = "機台監測_toolStripButton";
            this.機台監測_toolStripButton.Size = new System.Drawing.Size(150, 77);
            this.機台監測_toolStripButton.Text = "機台監測";
            this.機台監測_toolStripButton.Click += new System.EventHandler(this.機台監測_toolStripButton_Click);
            // 
            // 電力監測_toolStripButton
            // 
            this.電力監測_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.電力監測_toolStripButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.電力監測_toolStripButton.ForeColor = System.Drawing.Color.Gold;
            this.電力監測_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("電力監測_toolStripButton.Image")));
            this.電力監測_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.電力監測_toolStripButton.Name = "電力監測_toolStripButton";
            this.電力監測_toolStripButton.Size = new System.Drawing.Size(150, 77);
            this.電力監測_toolStripButton.Text = "電力監測";
            this.電力監測_toolStripButton.ToolTipText = "電力監測";
            this.電力監測_toolStripButton.Click += new System.EventHandler(this.電力監測_toolStripButton_Click);
            // 
            // 預防保養_toolStripButton
            // 
            this.預防保養_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.預防保養_toolStripButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.預防保養_toolStripButton.ForeColor = System.Drawing.Color.Gold;
            this.預防保養_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("預防保養_toolStripButton.Image")));
            this.預防保養_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.預防保養_toolStripButton.Name = "預防保養_toolStripButton";
            this.預防保養_toolStripButton.Size = new System.Drawing.Size(150, 77);
            this.預防保養_toolStripButton.Text = "預防保養";
            this.預防保養_toolStripButton.Click += new System.EventHandler(this.預防保養_toolStripButton_Click);
            // 
            // 生產總表_toolStripButton
            // 
            this.生產總表_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.生產總表_toolStripButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.生產總表_toolStripButton.ForeColor = System.Drawing.Color.Gold;
            this.生產總表_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("生產總表_toolStripButton.Image")));
            this.生產總表_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.生產總表_toolStripButton.Name = "生產總表_toolStripButton";
            this.生產總表_toolStripButton.Size = new System.Drawing.Size(150, 77);
            this.生產總表_toolStripButton.Text = "生產總表";
            this.生產總表_toolStripButton.ToolTipText = "生產總表";
            this.生產總表_toolStripButton.Click += new System.EventHandler(this.生產總表_toolStripButton_Click);
            // 
            // user_toolStripButton
            // 
            this.user_toolStripButton.AutoSize = false;
            this.user_toolStripButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.user_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.user_toolStripButton.Image = global::Exam.Properties.Resources.USER_240_200_0_;
            this.user_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.user_toolStripButton.Name = "user_toolStripButton";
            this.user_toolStripButton.Size = new System.Drawing.Size(23, 77);
            this.user_toolStripButton.Click += new System.EventHandler(this.user_toolStripButton_Click);
            // 
            // setup_toolStripDropDownButton
            // 
            this.setup_toolStripDropDownButton.AutoSize = false;
            this.setup_toolStripDropDownButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.setup_toolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.setup_toolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增機台_ToolStripMenuItem,
            this.機台保養狀況填寫_ToolStripMenuItem,
            this.產線生產細項填寫_ToolStripMenuItem});
            this.setup_toolStripDropDownButton.Image = global::Exam.Properties.Resources.SETUP_240_200_0_;
            this.setup_toolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.setup_toolStripDropDownButton.Name = "setup_toolStripDropDownButton";
            this.setup_toolStripDropDownButton.Size = new System.Drawing.Size(29, 77);
            // 
            // 新增機台_ToolStripMenuItem
            // 
            this.新增機台_ToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.新增機台_ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.新增機台_ToolStripMenuItem.ForeColor = System.Drawing.Color.Gold;
            this.新增機台_ToolStripMenuItem.Name = "新增機台_ToolStripMenuItem";
            this.新增機台_ToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.新增機台_ToolStripMenuItem.Text = "新增機台";
            // 
            // 機台保養狀況填寫_ToolStripMenuItem
            // 
            this.機台保養狀況填寫_ToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.機台保養狀況填寫_ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.機台保養狀況填寫_ToolStripMenuItem.ForeColor = System.Drawing.Color.Gold;
            this.機台保養狀況填寫_ToolStripMenuItem.Name = "機台保養狀況填寫_ToolStripMenuItem";
            this.機台保養狀況填寫_ToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.機台保養狀況填寫_ToolStripMenuItem.Text = "機台保養狀況填寫";
            // 
            // 產線生產細項填寫_ToolStripMenuItem
            // 
            this.產線生產細項填寫_ToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.產線生產細項填寫_ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.產線生產細項填寫_ToolStripMenuItem.ForeColor = System.Drawing.Color.Gold;
            this.產線生產細項填寫_ToolStripMenuItem.Name = "產線生產細項填寫_ToolStripMenuItem";
            this.產線生產細項填寫_ToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.產線生產細項填寫_ToolStripMenuItem.Text = "產線生產細項填寫";
            // 
            // exit_toolStripButton
            // 
            this.exit_toolStripButton.AutoSize = false;
            this.exit_toolStripButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.exit_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exit_toolStripButton.Image = global::Exam.Properties.Resources.EXIT_240_200_0_;
            this.exit_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exit_toolStripButton.Name = "exit_toolStripButton";
            this.exit_toolStripButton.Size = new System.Drawing.Size(23, 77);
            this.exit_toolStripButton.Text = "exit_toolStripButton";
            this.exit_toolStripButton.Click += new System.EventHandler(this.exit_toolStripButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.panel1.Location = new System.Drawing.Point(0, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1111, 701);
            this.panel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 775);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton 產能效率;
        private System.Windows.Forms.ToolStripButton 機台監測_toolStripButton;
        private System.Windows.Forms.ToolStripButton 電力監測_toolStripButton;
        private System.Windows.Forms.ToolStripButton 預防保養_toolStripButton;
        private System.Windows.Forms.ToolStripButton 生產總表_toolStripButton;
        private System.Windows.Forms.ToolStripButton user_toolStripButton;
        private System.Windows.Forms.ToolStripButton exit_toolStripButton;
        private System.Windows.Forms.ToolStripDropDownButton setup_toolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem 新增機台_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 機台保養狀況填寫_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 產線生產細項填寫_ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
    }
}

