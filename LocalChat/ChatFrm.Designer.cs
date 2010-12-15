


namespace LocalChat
{
    partial class ChatFrm
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
          this.lblUserName = new System.Windows.Forms.Label();
          this.tbMessages = new System.Windows.Forms.RichTextBox();
          this.tbUserMessage = new System.Windows.Forms.TextBox();
          this.SuspendLayout();
          // 
          // lblUserName
          // 
          this.lblUserName.AutoSize = true;
          this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lblUserName.Location = new System.Drawing.Point(11, 6);
          this.lblUserName.Name = "lblUserName";
          this.lblUserName.Size = new System.Drawing.Size(73, 17);
          this.lblUserName.TabIndex = 0;
          this.lblUserName.Text = "userName";
          // 
          // tbMessages
          // 
          this.tbMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.tbMessages.BackColor = System.Drawing.SystemColors.Window;
          this.tbMessages.Location = new System.Drawing.Point(12, 26);
          this.tbMessages.Name = "tbMessages";
          this.tbMessages.ReadOnly = true;
          this.tbMessages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
          this.tbMessages.Size = new System.Drawing.Size(314, 281);
          this.tbMessages.TabIndex = 3;
          this.tbMessages.Text = "";
          // 
          // tbUserMessage
          // 
          this.tbUserMessage.AcceptsReturn = true;
          this.tbUserMessage.AcceptsTab = true;
          this.tbUserMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.tbUserMessage.Location = new System.Drawing.Point(12, 313);
          this.tbUserMessage.MaxLength = 140;
          this.tbUserMessage.MinimumSize = new System.Drawing.Size(4, 67);
          this.tbUserMessage.Multiline = true;
          this.tbUserMessage.Name = "tbUserMessage";
          this.tbUserMessage.Size = new System.Drawing.Size(314, 67);
          this.tbUserMessage.TabIndex = 2;
          this.tbUserMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbUserMessage_KeyPress);
          // 
          // ChatFrm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(338, 392);
          this.Controls.Add(this.tbMessages);
          this.Controls.Add(this.tbUserMessage);
          this.Controls.Add(this.lblUserName);
          this.MinimumSize = new System.Drawing.Size(200, 300);
          this.Name = "ChatFrm";
          this.Text = "ChatFrm";
          this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatFrm_FormClosing);
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.RichTextBox tbMessages;
        private System.Windows.Forms.TextBox tbUserMessage;
    }
}