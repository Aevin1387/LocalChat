using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalChat
{
  public partial class Username : Form
  {
    public String UsernameText;
    
    public Username()
    {
      InitializeComponent();
    }

    private void tbUsername_TextChanged(object sender, EventArgs e)
    {
      if (tbUsername.Text.Length > 0)
        btnAccept.Enabled = true;
      else
        btnAccept.Enabled = false;
    }

    private void btnAccept_Click(object sender, EventArgs e)
    {
      UsernameText = tbUsername.Text;
      if (tbUsername.TextLength > 0)
      {
        this.DialogResult = DialogResult.OK;
        this.Close();
      } 
      else
        MessageBox.Show("Please enter a username!", "Username invalid", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void tbUsername_KeyPress(object sender, KeyPressEventArgs e) {
      if (!(Char.IsLetterOrDigit(e.KeyChar) || Char.IsControl(e.KeyChar)))
        e.Handled = true;
      else if (e.KeyChar == (Char)Keys.Return)
        btnAccept_Click(sender, null);
    }
  }
}
