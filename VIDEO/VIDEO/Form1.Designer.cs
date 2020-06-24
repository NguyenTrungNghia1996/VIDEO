namespace VIDEO
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.listFile = new System.Windows.Forms.ListBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnNext = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.btnMarker = new System.Windows.Forms.Button();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblStop = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(293, 60);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(1008, 614);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            // 
            // listFile
            // 
            this.listFile.FormattingEnabled = true;
            this.listFile.Location = new System.Drawing.Point(12, 60);
            this.listFile.Name = "listFile";
            this.listFile.Size = new System.Drawing.Size(275, 615);
            this.listFile.TabIndex = 1;
            this.listFile.SelectedIndexChanged += new System.EventHandler(this.listFile_SelectedIndexChanged);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(11, 680);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 36);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnBack
            // 
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(362, 680);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(47, 36);
            this.btnBack.TabIndex = 3;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnPause
            // 
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(415, 680);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(47, 36);
            this.btnPause.TabIndex = 5;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnNext
            // 
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(468, 680);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(47, 36);
            this.btnNext.TabIndex = 7;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI Emoji", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(1083, 677);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(218, 49);
            this.lblTime.TabIndex = 8;
            this.lblTime.Text = "Time: 00:00";
            // 
            // lblName
            // 
            this.lblName.AccessibleRole = System.Windows.Forms.AccessibleRole.SplitButton;
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI Historic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(289, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 25);
            this.lblName.TabIndex = 9;
            // 
            // btnMarker
            // 
            this.btnMarker.Location = new System.Drawing.Point(293, 680);
            this.btnMarker.Name = "btnMarker";
            this.btnMarker.Size = new System.Drawing.Size(63, 36);
            this.btnMarker.TabIndex = 10;
            this.btnMarker.Text = "Marker";
            this.btnMarker.UseVisualStyleBackColor = true;
            this.btnMarker.Click += new System.EventHandler(this.btnMarker_Click);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(1163, 28);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(35, 13);
            this.lblStart.TabIndex = 12;
            this.lblStart.Text = "label2";
            // 
            // lblStop
            // 
            this.lblStop.AutoSize = true;
            this.lblStop.Location = new System.Drawing.Point(1266, 27);
            this.lblStop.Name = "lblStop";
            this.lblStop.Size = new System.Drawing.Size(35, 13);
            this.lblStop.TabIndex = 13;
            this.lblStop.Text = "label2";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(1074, 28);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(35, 13);
            this.lblID.TabIndex = 14;
            this.lblID.Text = "label2";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1335, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(335, 615);
            this.dataGridView1.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1682, 732);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblStop);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.btnMarker);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.listFile);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.ListBox listFile;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnMarker;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblStop;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

