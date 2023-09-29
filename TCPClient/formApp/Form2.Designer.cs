
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
            this.dateTimeCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clientIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ComType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.event_Info = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblConnect = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblLoginError = new System.Windows.Forms.Label();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.lblAuditLogError = new System.Windows.Forms.Label();
            this.lblMachineID = new System.Windows.Forms.Label();
            this.lblMachine_password = new System.Windows.Forms.Label();
            this.lblMachineStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblIntervalError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dateTimeCol,
            this.clientIP,
            this.ComType,
            this.event_Info});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 175);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1148, 495);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged_2);
            // 
            // dateTimeCol
            // 
            this.dateTimeCol.Text = "Date_Time";
            this.dateTimeCol.Width = 150;
            // 
            // clientIP
            // 
            this.clientIP.Text = "Client_IP";
            this.clientIP.Width = 150;
            // 
            // ComType
            // 
            this.ComType.Text = "ComType";
            this.ComType.Width = 100;
            // 
            // event_Info
            // 
            this.event_Info.Text = "Event_Info";
            this.event_Info.Width = 550;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(532, 0);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(0, 20);
            this.lblDateTime.TabIndex = 6;
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(532, 112);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(0, 20);
            this.lblLogin.TabIndex = 14;
            // 
            // lblConnect
            // 
            this.lblConnect.AutoSize = true;
            this.lblConnect.Location = new System.Drawing.Point(536, 112);
            this.lblConnect.Name = "lblConnect";
            this.lblConnect.Size = new System.Drawing.Size(0, 20);
            this.lblConnect.TabIndex = 15;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.t_Tick);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(0, 0);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 29;
            // 
            // lblLoginError
            // 
            this.lblLoginError.Location = new System.Drawing.Point(0, 0);
            this.lblLoginError.Name = "lblLoginError";
            this.lblLoginError.Size = new System.Drawing.Size(100, 23);
            this.lblLoginError.TabIndex = 28;
            // 
            // lblMachineName
            // 
            this.lblMachineName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineName.Location = new System.Drawing.Point(13, 77);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(324, 32);
            this.lblMachineName.TabIndex = 26;
            this.lblMachineName.Text = "Machine Name:";
            // 
            // lblAuditLogError
            // 
            this.lblAuditLogError.AutoSize = true;
            this.lblAuditLogError.Location = new System.Drawing.Point(194, 312);
            this.lblAuditLogError.Name = "lblAuditLogError";
            this.lblAuditLogError.Size = new System.Drawing.Size(0, 20);
            this.lblAuditLogError.TabIndex = 25;
            // 
            // lblMachineID
            // 
            this.lblMachineID.AutoSize = true;
            this.lblMachineID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineID.Location = new System.Drawing.Point(12, 9);
            this.lblMachineID.Name = "lblMachineID";
            this.lblMachineID.Size = new System.Drawing.Size(112, 25);
            this.lblMachineID.TabIndex = 30;
            this.lblMachineID.Text = "MachineID:";
            // 
            // lblMachine_password
            // 
            this.lblMachine_password.AutoSize = true;
            this.lblMachine_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachine_password.Location = new System.Drawing.Point(12, 43);
            this.lblMachine_password.Name = "lblMachine_password";
            this.lblMachine_password.Size = new System.Drawing.Size(189, 25);
            this.lblMachine_password.TabIndex = 31;
            this.lblMachine_password.Text = "Machine Password: ";
            // 
            // lblMachineStatus
            // 
            this.lblMachineStatus.AutoSize = true;
            this.lblMachineStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineStatus.Location = new System.Drawing.Point(14, 112);
            this.lblMachineStatus.Name = "lblMachineStatus";
            this.lblMachineStatus.Size = new System.Drawing.Size(159, 25);
            this.lblMachineStatus.TabIndex = 34;
            this.lblMachineStatus.Text = "Machine Status: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 35;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1053, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 44);
            this.button1.TabIndex = 36;
            this.button1.Text = "Set";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtInterval
            // 
            this.txtInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInterval.Location = new System.Drawing.Point(929, 112);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(118, 28);
            this.txtInterval.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(674, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 25);
            this.label2.TabIndex = 38;
            this.label2.Text = "HeartBeat Interval (secs): ";
            // 
            // lblIntervalError
            // 
            this.lblIntervalError.AutoSize = true;
            this.lblIntervalError.Location = new System.Drawing.Point(929, 151);
            this.lblIntervalError.Name = "lblIntervalError";
            this.lblIntervalError.Size = new System.Drawing.Size(0, 20);
            this.lblIntervalError.TabIndex = 39;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 815);
            this.Controls.Add(this.lblIntervalError);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMachineStatus);
            this.Controls.Add(this.lblMachine_password);
            this.Controls.Add(this.lblMachineID);
            this.Controls.Add(this.lblAuditLogError);
            this.Controls.Add(this.lblMachineName);
            this.Controls.Add(this.lblLoginError);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblConnect);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.listView1);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Form2";
            this.Text = "TCPClient";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader clientIP;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.ColumnHeader dateTimeCol;
        private System.Windows.Forms.ColumnHeader ComType;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblLoginError;
        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.Label lblAuditLogError;
        private System.Windows.Forms.Label lblMachineID;
        private System.Windows.Forms.Label lblMachine_password;
        private System.Windows.Forms.Label lblMachineStatus;
        private System.Windows.Forms.ColumnHeader event_Info;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIntervalError;
    }
}