namespace H31DHTMgr
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ButtonSearch = new System.Windows.Forms.Button();
            this.Button_NEXTPAGE = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.MainStatusText = new System.Windows.Forms.Label();
            this.MainProgressBar = new System.Windows.Forms.ProgressBar();
            this.RecvTimer = new System.Windows.Forms.Timer(this.components);
            this.m_data = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MainLogText = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Button_About = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox_OrderBy = new System.Windows.Forms.ComboBox();
            this.comboBox_Type = new System.Windows.Forms.ComboBox();
            this.textBox_SearchIndex = new System.Windows.Forms.TextBox();
            this.Button_PrePAGE = new System.Windows.Forms.Button();
            this.textBox_Search = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_Torrent = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBox_Lanague = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_data)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonSearch
            // 
            this.ButtonSearch.Location = new System.Drawing.Point(311, 13);
            this.ButtonSearch.Name = "ButtonSearch";
            this.ButtonSearch.Size = new System.Drawing.Size(56, 23);
            this.ButtonSearch.TabIndex = 0;
            this.ButtonSearch.Text = "搜索";
            this.ButtonSearch.UseVisualStyleBackColor = true;
            this.ButtonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // Button_NEXTPAGE
            // 
            this.Button_NEXTPAGE.Location = new System.Drawing.Point(478, 12);
            this.Button_NEXTPAGE.Name = "Button_NEXTPAGE";
            this.Button_NEXTPAGE.Size = new System.Drawing.Size(52, 23);
            this.Button_NEXTPAGE.TabIndex = 1;
            this.Button_NEXTPAGE.Text = "下一页";
            this.Button_NEXTPAGE.UseVisualStyleBackColor = true;
            this.Button_NEXTPAGE.Click += new System.EventHandler(this.Button_NEXTPAGE_Click);
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(126, 12);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(57, 23);
            this.ButtonStart.TabIndex = 2;
            this.ButtonStart.Text = "开始";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // MainStatusText
            // 
            this.MainStatusText.AutoSize = true;
            this.MainStatusText.Location = new System.Drawing.Point(772, 18);
            this.MainStatusText.Name = "MainStatusText";
            this.MainStatusText.Size = new System.Drawing.Size(65, 12);
            this.MainStatusText.TabIndex = 6;
            this.MainStatusText.Text = "文件处理：";
            // 
            // MainProgressBar
            // 
            this.MainProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainProgressBar.Location = new System.Drawing.Point(3, 513);
            this.MainProgressBar.Name = "MainProgressBar";
            this.MainProgressBar.Size = new System.Drawing.Size(1149, 14);
            this.MainProgressBar.TabIndex = 7;
            // 
            // RecvTimer
            // 
            this.RecvTimer.Interval = 3000;
            this.RecvTimer.Tick += new System.EventHandler(this.RecvTimer_Tick);
            // 
            // m_data
            // 
            this.m_data.AllowUserToAddRows = false;
            this.m_data.AllowUserToDeleteRows = false;
            this.m_data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.m_data.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.m_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_data.Location = new System.Drawing.Point(3, 50);
            this.m_data.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.m_data.Name = "m_data";
            this.m_data.ReadOnly = true;
            this.m_data.RowTemplate.Height = 23;
            this.m_data.Size = new System.Drawing.Size(1149, 457);
            this.m_data.TabIndex = 8;
            this.m_data.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_data_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MainLogText);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 530);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(1155, 198);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "处理日志";
            // 
            // MainLogText
            // 
            this.MainLogText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLogText.Location = new System.Drawing.Point(3, 14);
            this.MainLogText.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.MainLogText.Multiline = true;
            this.MainLogText.Name = "MainLogText";
            this.MainLogText.ReadOnly = true;
            this.MainLogText.Size = new System.Drawing.Size(1149, 181);
            this.MainLogText.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.MainProgressBar, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.m_data, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1155, 728);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Button_About);
            this.panel1.Controls.Add(this.MainStatusText);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1149, 44);
            this.panel1.TabIndex = 10;
            // 
            // Button_About
            // 
            this.Button_About.Location = new System.Drawing.Point(1076, 9);
            this.Button_About.Name = "Button_About";
            this.Button_About.Size = new System.Drawing.Size(64, 27);
            this.Button_About.TabIndex = 11;
            this.Button_About.Text = "关于";
            this.Button_About.UseVisualStyleBackColor = true;
            this.Button_About.Click += new System.EventHandler(this.ButtonAbout_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox_Lanague);
            this.groupBox2.Controls.Add(this.comboBox_OrderBy);
            this.groupBox2.Controls.Add(this.comboBox_Type);
            this.groupBox2.Controls.Add(this.textBox_SearchIndex);
            this.groupBox2.Controls.Add(this.Button_PrePAGE);
            this.groupBox2.Controls.Add(this.textBox_Search);
            this.groupBox2.Controls.Add(this.ButtonSearch);
            this.groupBox2.Controls.Add(this.Button_NEXTPAGE);
            this.groupBox2.Location = new System.Drawing.Point(222, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(544, 40);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "搜索";
            // 
            // comboBox_OrderBy
            // 
            this.comboBox_OrderBy.FormattingEnabled = true;
            this.comboBox_OrderBy.Items.AddRange(new object[] {
            "ID",
            "次数",
            "时间"});
            this.comboBox_OrderBy.Location = new System.Drawing.Point(189, 14);
            this.comboBox_OrderBy.Name = "comboBox_OrderBy";
            this.comboBox_OrderBy.Size = new System.Drawing.Size(47, 20);
            this.comboBox_OrderBy.TabIndex = 11;
            // 
            // comboBox_Type
            // 
            this.comboBox_Type.FormattingEnabled = true;
            this.comboBox_Type.Items.AddRange(new object[] {
            "视频",
            "音乐",
            "书籍",
            "程序",
            "图片",
            "其它"});
            this.comboBox_Type.Location = new System.Drawing.Point(133, 14);
            this.comboBox_Type.Name = "comboBox_Type";
            this.comboBox_Type.Size = new System.Drawing.Size(55, 20);
            this.comboBox_Type.TabIndex = 7;
            // 
            // textBox_SearchIndex
            // 
            this.textBox_SearchIndex.Location = new System.Drawing.Point(370, 12);
            this.textBox_SearchIndex.Name = "textBox_SearchIndex";
            this.textBox_SearchIndex.Size = new System.Drawing.Size(43, 21);
            this.textBox_SearchIndex.TabIndex = 10;
            this.textBox_SearchIndex.Text = "1";
            // 
            // Button_PrePAGE
            // 
            this.Button_PrePAGE.Location = new System.Drawing.Point(419, 12);
            this.Button_PrePAGE.Name = "Button_PrePAGE";
            this.Button_PrePAGE.Size = new System.Drawing.Size(53, 23);
            this.Button_PrePAGE.TabIndex = 9;
            this.Button_PrePAGE.Text = "上一页";
            this.Button_PrePAGE.UseVisualStyleBackColor = true;
            this.Button_PrePAGE.Click += new System.EventHandler(this.Button_PrePAGE_Click);
            // 
            // textBox_Search
            // 
            this.textBox_Search.Location = new System.Drawing.Point(6, 13);
            this.textBox_Search.Name = "textBox_Search";
            this.textBox_Search.Size = new System.Drawing.Size(125, 21);
            this.textBox_Search.TabIndex = 8;
            this.textBox_Search.Text = "美女";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox_Torrent);
            this.groupBox3.Controls.Add(this.ButtonStart);
            this.groupBox3.Location = new System.Drawing.Point(12, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(193, 41);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "监控数据";
            // 
            // checkBox_Torrent
            // 
            this.checkBox_Torrent.AutoSize = true;
            this.checkBox_Torrent.Checked = true;
            this.checkBox_Torrent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Torrent.Location = new System.Drawing.Point(6, 18);
            this.checkBox_Torrent.Name = "checkBox_Torrent";
            this.checkBox_Torrent.Size = new System.Drawing.Size(114, 16);
            this.checkBox_Torrent.TabIndex = 3;
            this.checkBox_Torrent.Text = "保留Torrent文件";
            this.checkBox_Torrent.UseVisualStyleBackColor = true;
            // 
            // comboBox_Lanague
            // 
            this.comboBox_Lanague.FormattingEnabled = true;
            this.comboBox_Lanague.Items.AddRange(new object[] {
            "日韩等语",
            "中英文"});
            this.comboBox_Lanague.Location = new System.Drawing.Point(239, 14);
            this.comboBox_Lanague.Name = "comboBox_Lanague";
            this.comboBox_Lanague.Size = new System.Drawing.Size(72, 20);
            this.comboBox_Lanague.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 728);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "H31DHTMgr";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_data)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonSearch;
        private System.Windows.Forms.Button Button_NEXTPAGE;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Label MainStatusText;
        private System.Windows.Forms.ProgressBar MainProgressBar;
        private System.Windows.Forms.Timer RecvTimer;
        private System.Windows.Forms.DataGridView m_data;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox MainLogText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_Search;
        private System.Windows.Forms.ComboBox comboBox_Type;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox_Torrent;
        private System.Windows.Forms.TextBox textBox_SearchIndex;
        private System.Windows.Forms.Button Button_PrePAGE;
        private System.Windows.Forms.ComboBox comboBox_OrderBy;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button Button_About;
        private System.Windows.Forms.ComboBox comboBox_Lanague;
    }
}

