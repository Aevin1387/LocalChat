namespace LocalChat
{
  partial class GetPassword
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
      this.lbRequest = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.tbPass = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // lbRequest
      // 
      this.lbRequest.AutoSize = true;
      this.lbRequest.Location = new System.Drawing.Point(13, 13);
      this.lbRequest.Name = "lbRequest";
      this.lbRequest.Size = new System.Drawing.Size(245, 13);
      this.lbRequest.TabIndex = 0;
      this.lbRequest.Text = "Please enter a password for the conversation with ";
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(46, 102);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(211, 102);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // tbPass
      // 
      this.tbPass.Location = new System.Drawing.Point(16, 30);
      this.tbPass.MaxLength = 140;
      this.tbPass.Multiline = true;
      this.tbPass.Name = "tbPass";
      this.tbPass.Size = new System.Drawing.Size(306, 66);
      this.tbPass.TabIndex = 3;
      this.tbPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPass_KeyPress);
      // 
      // GetPassword
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(334, 137);
      this.Controls.Add(this.tbPass);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.lbRequest);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "GetPassword";
      this.Text = "Password for conversation with";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbRequest;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox tbPass;
  }
}