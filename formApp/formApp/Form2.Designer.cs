
namespace formApp
{
    partial class Form2
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
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTimer = new System.Windows.Forms.TextBox();
            this.txtFinalTimer = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.mID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblSelectedMachineID = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.loadingPictureBox = new System.Windows.Forms.PictureBox();
            this.lblMachineId = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.loadingPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(701, 109);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(96, 35);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(599, 109);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(595, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter Timer (Seconds)\r\nUpdate Status   Check Status\r\n";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtTimer
            // 
            this.txtTimer.Location = new System.Drawing.Point(599, 76);
            this.txtTimer.Name = "txtTimer";
            this.txtTimer.Size = new System.Drawing.Size(96, 26);
            this.txtTimer.TabIndex = 3;
            // 
            // txtFinalTimer
            // 
            this.txtFinalTimer.Location = new System.Drawing.Point(701, 76);
            this.txtFinalTimer.Name = "txtFinalTimer";
            this.txtFinalTimer.Size = new System.Drawing.Size(96, 26);
            this.txtFinalTimer.TabIndex = 4;
            this.txtFinalTimer.TextChanged += new System.EventHandler(this.txtFinalTimer_TextChanged);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.mID,
            this.mName,
            this.status});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(560, 538);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged_2);
            // 
            // mID
            // 
            this.mID.Text = "ID";
            this.mID.Width = 79;
            // 
            // mName
            // 
            this.mName.Text = "MachineName";
            this.mName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mName.Width = 141;
            // 
            // status
            // 
            this.status.Text = "Status";
            this.status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.status.Width = 124;
            // 
            // lblSelectedMachineID
            // 
            this.lblSelectedMachineID.AutoSize = true;
            this.lblSelectedMachineID.Location = new System.Drawing.Point(599, 12);
            this.lblSelectedMachineID.Name = "lblSelectedMachineID";
            this.lblSelectedMachineID.Size = new System.Drawing.Size(94, 20);
            this.lblSelectedMachineID.TabIndex = 6;
            this.lblSelectedMachineID.Text = "Machine ID:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // loadingPictureBox
            // 
            this.loadingPictureBox.Image = global::formApp.Properties.Resources.loadingIcon_gif;
            this.loadingPictureBox.Location = new System.Drawing.Point(803, 76);
            this.loadingPictureBox.Name = "loadingPictureBox";
            this.loadingPictureBox.Size = new System.Drawing.Size(75, 75);
            this.loadingPictureBox.TabIndex = 7;
            this.loadingPictureBox.TabStop = false;
            // 
            // lblMachineId
            // 
            this.lblMachineId.Location = new System.Drawing.Point(697, 12);
            this.lblMachineId.Name = "lblMachineId";
            this.lblMachineId.Size = new System.Drawing.Size(51, 20);
            this.lblMachineId.TabIndex = 0;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(599, 147);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 20);
            this.lblResult.TabIndex = 8;
            this.lblResult.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(599, 146);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(99, 45);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblMachineId);
            this.Controls.Add(this.loadingPictureBox);
            this.Controls.Add(this.lblSelectedMachineID);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.txtFinalTimer);
            this.Controls.Add(this.txtTimer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loadingPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTimer;
        private System.Windows.Forms.TextBox txtFinalTimer;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader mID;
        private System.Windows.Forms.ColumnHeader mName;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.Label lblSelectedMachineID;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox loadingPictureBox;
        private System.Windows.Forms.Label lblMachineId;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnRefresh;
    }
}