
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
            this.userEmail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Activity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblTimerCHange = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblConnect = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSignOut = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblLoginError = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.auditEventTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.auditAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.auditServerPrincipleName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.auditClientIp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.auditAddInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblAuditLogError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dateTimeCol,
            this.userEmail,
            this.Activity});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 175);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1148, 250);
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
            // userEmail
            // 
            this.userEmail.Text = "LoggedInUserEmail";
            this.userEmail.Width = 150;
            // 
            // Activity
            // 
            this.Activity.Text = "Activity";
            this.Activity.Width = 570;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(22, 63);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(0, 20);
            this.lblDateTime.TabIndex = 6;
            // 
            // lblTimerCHange
            // 
            this.lblTimerCHange.AutoSize = true;
            this.lblTimerCHange.Location = new System.Drawing.Point(22, 22);
            this.lblTimerCHange.Name = "lblTimerCHange";
            this.lblTimerCHange.Size = new System.Drawing.Size(91, 20);
            this.lblTimerCHange.TabIndex = 7;
            this.lblTimerCHange.Text = "IP Address:";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(116, 22);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(140, 26);
            this.txtHost.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Port:";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(310, 22);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(100, 26);
            this.txtIp.TabIndex = 11;
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(26, 102);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.ReadOnly = true;
            this.txtLogin.Size = new System.Drawing.Size(362, 26);
            this.txtLogin.TabIndex = 12;
            this.txtLogin.TextChanged += new System.EventHandler(this.txtLogin_TextChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(416, 18);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(93, 38);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
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
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSignOut
            // 
            this.btnSignOut.Location = new System.Drawing.Point(514, 18);
            this.btnSignOut.Name = "btnSignOut";
            this.btnSignOut.Size = new System.Drawing.Size(106, 38);
            this.btnSignOut.TabIndex = 16;
            this.btnSignOut.Text = "Disconnect";
            this.btnSignOut.UseVisualStyleBackColor = true;
            this.btnSignOut.Click += new System.EventHandler(this.btnSignOut_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Enabled = false;
            this.btnLogin.Location = new System.Drawing.Point(416, 94);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(93, 42);
            this.btnLogin.TabIndex = 17;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblLoginError
            // 
            this.lblLoginError.AutoSize = true;
            this.lblLoginError.Location = new System.Drawing.Point(622, 108);
            this.lblLoginError.Name = "lblLoginError";
            this.lblLoginError.Size = new System.Drawing.Size(0, 20);
            this.lblLoginError.TabIndex = 18;
            // 
            // btnLogOut
            // 
            this.btnLogOut.Enabled = false;
            this.btnLogOut.Location = new System.Drawing.Point(514, 94);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(100, 40);
            this.btnLogOut.TabIndex = 19;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "User activity / log record";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 473);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Audit Login";
            // 
            // btnQuery
            // 
            this.btnQuery.Enabled = false;
            this.btnQuery.Location = new System.Drawing.Point(107, 458);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(81, 35);
            this.btnQuery.TabIndex = 23;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.auditEventTime,
            this.auditAction,
            this.auditServerPrincipleName,
            this.auditClientIp,
            this.auditAddInfo});
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(16, 496);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(1148, 300);
            this.listView2.TabIndex = 24;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // auditEventTime
            // 
            this.auditEventTime.Text = "Event_Time";
            this.auditEventTime.Width = 150;
            // 
            // auditAction
            // 
            this.auditAction.Text = "Action_ID";
            this.auditAction.Width = 80;
            // 
            // auditServerPrincipleName
            // 
            this.auditServerPrincipleName.Text = "Server_Principle_Name";
            this.auditServerPrincipleName.Width = 220;
            // 
            // auditClientIp
            // 
            this.auditClientIp.Text = "Client_IP";
            this.auditClientIp.Width = 90;
            // 
            // auditAddInfo
            // 
            this.auditAddInfo.Text = "Additional_Information";
            this.auditAddInfo.Width = 200;
            // 
            // lblAuditLogError
            // 
            this.lblAuditLogError.AutoSize = true;
            this.lblAuditLogError.Location = new System.Drawing.Point(194, 312);
            this.lblAuditLogError.Name = "lblAuditLogError";
            this.lblAuditLogError.Size = new System.Drawing.Size(0, 20);
            this.lblAuditLogError.TabIndex = 25;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 815);
            this.Controls.Add(this.lblAuditLogError);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.lblLoginError);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnSignOut);
            this.Controls.Add(this.lblConnect);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.lblTimerCHange);
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
        private System.Windows.Forms.ColumnHeader userEmail;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblTimerCHange;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.ColumnHeader dateTimeCol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.ColumnHeader Activity;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnSignOut;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblLoginError;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader auditEventTime;
        private System.Windows.Forms.ColumnHeader auditAction;
        private System.Windows.Forms.ColumnHeader auditServerPrincipleName;
        private System.Windows.Forms.ColumnHeader auditClientIp;
        private System.Windows.Forms.ColumnHeader auditAddInfo;
        private System.Windows.Forms.Label lblAuditLogError;
    }
}