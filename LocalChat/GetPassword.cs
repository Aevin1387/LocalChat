using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace LocalChat
{
  public partial class GetPassword : Form
  {
    public String password;
    public GetPassword(String username)
    {
      InitializeComponent();
      this.Text += username;
      lbRequest.Text += username;
    }

    private void checkText()
    {
      
      if (tbPass.Text.Length == 140)
      {
        password = tbPass.Text;
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      else
      {
        MessageBox.Show("Password too short", "Error: The password must be " +
                      "140 characters.",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      checkText();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    

    private void tbPass_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!char.IsLetterOrDigit(e.KeyChar))
      {
        if (e.KeyChar == (Char)Keys.Return)
        {
          checkText();
          e.Handled = true;
        }
        e.Handled = true;
      }
    }

    
  }
}
