
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.mID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblTimerCHange = new System.Windows.Forms.Label();
            this.txtTimer = new System.Windows.Forms.TextBox();
            this.btnTimerSet = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(593, 22);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(0, 20);
            this.lblDateTime.TabIndex = 6;
            // 
            // lblTimerCHange
            // 
            this.lblTimerCHange.AutoSize = true;
            this.lblTimerCHange.Location = new System.Drawing.Point(597, 46);
            this.lblTimerCHange.Name = "lblTimerCHange";
            this.lblTimerCHange.Size = new System.Drawing.Size(193, 20);
            this.lblTimerCHange.TabIndex = 7;
            this.lblTimerCHange.Text = "Change Timer (Seconds): ";
            // 
            // txtTimer
            // 
            this.txtTimer.Location = new System.Drawing.Point(788, 46);
            this.txtTimer.Name = "txtTimer";
            this.txtTimer.Size = new System.Drawing.Size(100, 26);
            this.txtTimer.TabIndex = 8;
            // 
            // btnTimerSet
            // 
            this.btnTimerSet.Location = new System.Drawing.Point(613, 87);
            this.btnTimerSet.Name = "btnTimerSet";
            this.btnTimerSet.Size = new System.Drawing.Size(75, 33);
            this.btnTimerSet.TabIndex = 9;
            this.btnTimerSet.Text = "Set";
            this.btnTimerSet.UseVisualStyleBackColor = true;
            this.btnTimerSet.Click += new System.EventHandler(this.btnTimerSet_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.btnTimerSet);
            this.Controls.Add(this.txtTimer);
            this.Controls.Add(this.lblTimerCHange);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.listView1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader mID;
        private System.Windows.Forms.ColumnHeader mName;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblTimerCHange;
        private System.Windows.Forms.TextBox txtTimer;
        private System.Windows.Forms.Button btnTimerSet;
    }
}