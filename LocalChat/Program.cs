using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LocalChat
{
  static class Program
  {
    static String username;
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Username usernameForm = new Username();
      
      if(usernameForm.ShowDialog() == DialogResult.OK){
        username = usernameForm.UsernameText;
        Application.Run(new UserListFrm(username));
      }

      Application.Exit();
    }
  }
}
