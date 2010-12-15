namespace LocalChat
{
    partial class UserListFrm
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
          this.label1 = new System.Windows.Forms.Label();
          this.lbUsers = new System.Windows.Forms.ListBox();
          this.newUserCheckTimer = new System.Windows.Forms.Timer(this.components);
          this.cmUser = new System.Windows.Forms.ContextMenuStrip(this.components);
          this.startEncryptedMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.cmUser.SuspendLayout();
          this.SuspendLayout();
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(8, 7);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(64, 13);
          this.label1.TabIndex = 0;
          this.label1.Text = "Local users:";
          // 
          // lbUsers
          // 
          this.lbUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.lbUsers.FormattingEnabled = true;
          this.lbUsers.Location = new System.Drawing.Point(11, 23);
          this.lbUsers.Name = "lbUsers";
          this.lbUsers.Size = new System.Drawing.Size(184, 446);
          this.lbUsers.TabIndex = 1;
          this.lbUsers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lbUsers_KeyPress);
          this.lbUsers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbUsers_DoubleClick);
          this.lbUsers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbUsers_MouseDown);
          // 
          // newUserCheckTimer
          // 
          this.newUserCheckTimer.Tick += new System.EventHandler(this.newUserCheckTimer_Tick);
          // 
          // cmUser
          // 
          this.cmUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startEncryptedMessageToolStripMenuItem});
          this.cmUser.Name = "cmUser";
          this.cmUser.Size = new System.Drawing.Size(204, 48);
          this.cmUser.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmUser_ItemClicked);
          
          // 
          // startEncryptedMessageToolStripMenuItem
          // 
          this.startEncryptedMessageToolStripMenuItem.Name = "startEncryptedMessageToolStripMenuItem";
          this.startEncryptedMessageToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
          this.startEncryptedMessageToolStripMenuItem.Text = "Start encrypted message";
          // 
          // UserListFrm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(207, 481);
          this.Controls.Add(this.lbUsers);
          this.Controls.Add(this.label1);
          this.Name = "UserListFrm";
          this.Text = "LocalChat";
          this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserListFrm_FrmClsing);
          this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.USERLISTFRM_FRMCLSD);
          this.cmUser.ResumeLayout(false);
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.Timer newUserCheckTimer;
        private System.Windows.Forms.ContextMenuStrip cmUser;
        private System.Windows.Forms.ToolStripMenuItem startEncryptedMessageToolStripMenuItem;
    }
}

