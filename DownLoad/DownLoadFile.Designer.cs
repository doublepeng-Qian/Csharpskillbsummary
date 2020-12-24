namespace DownLoad
{
    partial class DownLoadFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReference = new System.Windows.Forms.Button();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.pbDownLoad = new System.Windows.Forms.ProgressBar();
            this.lbProcess = new System.Windows.Forms.ListBox();
            this.cmsClearAll = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmClaerAll = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCurrentVer = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.cmsClearAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoadFile.Location = new System.Drawing.Point(590, 12);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(99, 42);
            this.btnLoadFile.TabIndex = 0;
            this.btnLoadFile.Text = "加载文件";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(695, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(99, 42);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "开始烧录";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbFilePath
            // 
            this.tbFilePath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFilePath.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tbFilePath.Location = new System.Drawing.Point(0, 23);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(583, 23);
            this.tbFilePath.TabIndex = 2;
            this.tbFilePath.Text = "请加载烧录文件...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "烧录文件路径：";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbProcess, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrentVer, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 467);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReference);
            this.panel1.Controls.Add(this.lblPercentage);
            this.panel1.Controls.Add(this.pbDownLoad);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnLoadFile);
            this.panel1.Controls.Add(this.tbFilePath);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 105);
            this.panel1.TabIndex = 0;
            // 
            // btnReference
            // 
            this.btnReference.Enabled = false;
            this.btnReference.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReference.Location = new System.Drawing.Point(695, 60);
            this.btnReference.Name = "btnReference";
            this.btnReference.Size = new System.Drawing.Size(99, 42);
            this.btnReference.TabIndex = 6;
            this.btnReference.Text = "复位";
            this.btnReference.UseVisualStyleBackColor = true;
            this.btnReference.Click += new System.EventHandler(this.btnReference_Click);
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.BackColor = System.Drawing.Color.Transparent;
            this.lblPercentage.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPercentage.Location = new System.Drawing.Point(589, 72);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(31, 19);
            this.lblPercentage.TabIndex = 5;
            this.lblPercentage.Text = "0%";
            // 
            // pbDownLoad
            // 
            this.pbDownLoad.Location = new System.Drawing.Point(0, 66);
            this.pbDownLoad.Name = "pbDownLoad";
            this.pbDownLoad.Size = new System.Drawing.Size(583, 25);
            this.pbDownLoad.TabIndex = 4;
            // 
            // lbProcess
            // 
            this.lbProcess.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbProcess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbProcess.ContextMenuStrip = this.cmsClearAll;
            this.lbProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProcess.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbProcess.FormattingEnabled = true;
            this.lbProcess.ItemHeight = 19;
            this.lbProcess.Location = new System.Drawing.Point(3, 140);
            this.lbProcess.Name = "lbProcess";
            this.lbProcess.Size = new System.Drawing.Size(794, 324);
            this.lbProcess.TabIndex = 1;
            // 
            // cmsClearAll
            // 
            this.cmsClearAll.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmClaerAll});
            this.cmsClearAll.Name = "cmsClearAll";
            this.cmsClearAll.Size = new System.Drawing.Size(125, 26);
            this.cmsClearAll.Text = "清除全部";
            // 
            // tsmClaerAll
            // 
            this.tsmClaerAll.Name = "tsmClaerAll";
            this.tsmClaerAll.Size = new System.Drawing.Size(124, 22);
            this.tsmClaerAll.Text = "清除全部";
            this.tsmClaerAll.Click += new System.EventHandler(this.tsmClaerAll_Click);
            // 
            // lblCurrentVer
            // 
            this.lblCurrentVer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCurrentVer.AutoSize = true;
            this.lblCurrentVer.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentVer.Location = new System.Drawing.Point(3, 114);
            this.lblCurrentVer.Name = "lblCurrentVer";
            this.lblCurrentVer.Size = new System.Drawing.Size(131, 19);
            this.lblCurrentVer.TabIndex = 2;
            this.lblCurrentVer.Text = "当前软件版本：----";
            // 
            // DownLoadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 467);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DownLoadFile";
            this.Text = "DownLoadFile";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DownLoadFile_FormClosed);
            this.Load += new System.EventHandler(this.DownLoadFile_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cmsClearAll.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbProcess;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.ProgressBar pbDownLoad;
        private System.Windows.Forms.Button btnReference;
        private System.Windows.Forms.Label lblCurrentVer;
        private System.Windows.Forms.ContextMenuStrip cmsClearAll;
        private System.Windows.Forms.ToolStripMenuItem tsmClaerAll;
    }
}