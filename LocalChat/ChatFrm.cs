using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;


namespace LocalChat
{
  public partial class ChatFrm : Form
  {   
    public String myUsername;

    public User chatUser;
    public UserListFrm userList;
    


    public String AddUserTime(String uName, bool ThisUser)
    {
      int begin = tbMessages.Text.Length;
      DateTime time = DateTime.Now;
      String tString = time.ToString("hh:mm:ss tt");
      String nameField = uName + " (" + tString + "): ";

      // Handle memory
      tString = null;

      return nameField;
    }

    public void SendMsg(Message toSend) {
      String userTime;

      userTime = AddUserTime(myUsername, true);
      DisplayMsg(userTime, toSend.messageStr, myUsername);
      SendMessage sending = new SendMessage(toSend, chatUser, myUsername, 
                                            this);
      chatUser.touchAwaitingMACK(true, ref sending, 0);

      // Handle memory
      userTime = null;
    }

    private void DisplayMsg(String userTime, String msg, String user) {
      lock (this) {
        int begin = tbMessages.Text.Length;
        int userLength = userTime.Length;

        tbMessages.SelectionStart = begin;
        tbMessages.SelectionLength = 0;

        
        Color userColor;
        if(user.Equals(myUsername))
        {
          userColor = Color.Purple;
        }
        else{
          userColor = Color.Blue;
        }
        tbMessages.SelectionColor = userColor;
        tbMessages.AppendText(userTime);

        tbMessages.SelectionStart = tbMessages.Text.Length;
        tbMessages.SelectionLength = 0;
        tbMessages.SelectionColor = Color.Black;
        tbMessages.AppendText(msg + "\r\n");

      }
    }

    public ChatFrm(User pChatUser, String thisUser, UserListFrm userList)
    {
      InitializeComponent();
      myUsername = thisUser;
      this.chatUser = pChatUser;
      this.lblUserName.Text = pChatUser.username;
      this.Text = pChatUser.username;
      this.userList = userList;
      

    }

    public void MessageError(SendMessage errorMsg){
      Control sync = this.ActiveControl;

      if (sync != null && sync.InvokeRequired)
      {
        sync.Invoke((Action)delegate { this.MessageError(errorMsg);  }, null);
        return;
      }

      int begin = tbMessages.Text.Length;
      String messageStr = errorMsg.toSend.messageStr;
      String error = "Error sending message: " + messageStr + 
                         "\r\nEnding chat.";
      int errorLength = error.Length;
      tbMessages.Text += error;
      tbMessages.Select(begin, errorLength);
      tbMessages.SelectionColor = Color.Red;
      tbUserMessage.ReadOnly = true;

      this.userList.UserList.Remove(chatUser);
      // Handle memory
      messageStr = null;
      error = null;
    }

    private void tbUserMessage_KeyPress(object sender, KeyPressEventArgs e) {
      if (e.KeyChar == (char)Keys.Return) {
        
        Message toSend = new Message(tbUserMessage.Text, chatUser);
        SendMsg(toSend);
        tbUserMessage.ResetText();
        tbUserMessage.SelectionStart = 0;
        e.Handled = true;

        // Memory handle
        toSend = null;
      }
    }

    

    public void printMsg(MessageEventArgs e)
    {
      if (e.messageSet)
      {
        
        Message receivedMsg = e.newMessage;
        String user = receivedMsg.sndUser.username;
        String time = AddUserTime(user, false);
        String messageStr;

        if (chatUser.isPasswordProtected())
        {
          String encStr = receivedMsg.messageStr;
          messageStr = Message.xorMsg(encStr, chatUser.getPassword());
        }
        else
        {
          messageStr = receivedMsg.messageStr;
        }
        DisplayMsg(time, messageStr, user);

        // Memory handle
        time = null;
      }
      
    }

    

    private void ChatFrm_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.Hide();
      e.Cancel = true;
    }
  }
}
