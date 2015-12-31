namespace Grabber
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
            this.btnGo = new System.Windows.Forms.Button();
            this.ddlDrive = new System.Windows.Forms.ComboBox();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPic = new System.Windows.Forms.Label();
            this.lblVid = new System.Windows.Forms.Label();
            this.bwCopy = new System.ComponentModel.BackgroundWorker();
            this.ddlPhone = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(240, 177);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // ddlDrive
            // 
            this.ddlDrive.FormattingEnabled = true;
            this.ddlDrive.Location = new System.Drawing.Point(24, 13);
            this.ddlDrive.Name = "ddlDrive";
            this.ddlDrive.Size = new System.Drawing.Size(121, 21);
            this.ddlDrive.TabIndex = 3;
            this.ddlDrive.SelectedIndexChanged += new System.EventHandler(this.ddlDrive_SelectedIndexChanged);
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(11, 110);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(571, 23);
            this.pbStatus.Step = 1;
            this.pbStatus.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(24, 140);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "lblStatus";
            // 
            // lblPic
            // 
            this.lblPic.AutoSize = true;
            this.lblPic.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPic.Location = new System.Drawing.Point(182, 13);
            this.lblPic.Name = "lblPic";
            this.lblPic.Size = new System.Drawing.Size(73, 29);
            this.lblPic.TabIndex = 6;
            this.lblPic.Text = "lblPic";
            // 
            // lblVid
            // 
            this.lblVid.AutoSize = true;
            this.lblVid.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVid.Location = new System.Drawing.Point(181, 44);
            this.lblVid.Name = "lblVid";
            this.lblVid.Size = new System.Drawing.Size(74, 29);
            this.lblVid.TabIndex = 7;
            this.lblVid.Text = "lblVid";
            // 
            // bwCopy
            // 
            this.bwCopy.WorkerReportsProgress = true;
            this.bwCopy.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCopy_DoWork);
            this.bwCopy.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCopy_RunWorkerCompleted);
            // 
            // ddlPhone
            // 
            this.ddlPhone.FormattingEnabled = true;
            this.ddlPhone.Items.AddRange(new object[] {
            "Nicki",
            "Sean"});
            this.ddlPhone.Location = new System.Drawing.Point(27, 201);
            this.ddlPhone.Name = "ddlPhone";
            this.ddlPhone.Size = new System.Drawing.Size(121, 21);
            this.ddlPhone.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 256);
            this.Controls.Add(this.ddlPhone);
            this.Controls.Add(this.lblVid);
            this.Controls.Add(this.lblPic);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.ddlDrive);
            this.Controls.Add(this.btnGo);
            this.Name = "Form1";
            this.Text = "Media Grabber";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ComboBox ddlDrive;
        private System.Windows.Forms.ProgressBar pbStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPic;
        private System.Windows.Forms.Label lblVid;
        private System.ComponentModel.BackgroundWorker bwCopy;
        private System.Windows.Forms.ComboBox ddlPhone;
    }
}

