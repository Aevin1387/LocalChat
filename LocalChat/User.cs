using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace LocalChat
{
  public class User
  {
    public IPAddress userAddress { get; set; }
    public String username { get; set; }
    public List<Message> messages { get; set; }
    public SequenceID sequence;
    public List<int> receivedSequences = new List<int>();
    public List<SendMessage> awaitingMACK = new List<SendMessage>();
    private ChatFrm chatWindow { get; set; }
    public MessageListWithChangedEvent receivedMessages =
      new MessageListWithChangedEvent();
    private String password { get; set; }
    private bool passwordProtected { get; set; }

    public User(IPAddress userAddress, String username)
    {
      this.userAddress = userAddress;
      this.username = username;
      this.sequence = new SequenceID();
      this.chatWindow = null;
      receivedMessages.Changed += 
        new ChangedEventHandler(newMessage);
    }

    public void openChat(ref ChatFrm chatWindow)
    {
      if (this.chatWindow != null && this.chatWindow.Visible)
      {
        this.chatWindow.Activate();
        chatWindow = null;
      }
      else
      {
        this.chatWindow = chatWindow;
        //chatWindow.Show();

      }
    }

    public ChatFrm getChat()
    {
      return chatWindow;
    }

    public bool chatExists()
    {
      if (this.chatWindow != null)
        return true;
      else
        return false;
    }

    public bool chatHidden()
    {
      if (this.chatWindow != null && !this.chatWindow.Visible)
        return true;
      else
        return false;
    }

    public void removeChat()
    {
      if (chatWindow != null)
      {
        chatWindow.Hide();
        chatWindow.Enabled = false;
        chatWindow.Close();
        chatWindow = null;
      }
    }

    public void newMessage(object sender, MessageEventArgs e)
    { 
      Control sync = chatWindow.ActiveControl;

      if (sync != null && sync.InvokeRequired)
      {
        sync.Invoke((Action)delegate { chatWindow.printMsg(e); }, null);
        return;
      }
      chatWindow.printMsg(e);
    }

    public bool touchAwaitingMACK(bool write, ref SendMessage message, int id)
    {
      lock (this)
      {
        if (write)
        {
          awaitingMACK.Add(message);
          return false;
        }
        else
        {
          foreach (SendMessage waiting in awaitingMACK)
          {
            if (waiting.iSequenceID == id)
            {
              waiting.MsgRcvd();
              awaitingMACK.Remove(waiting);
              return true;
            }
          }
          return false;
        }
      }
    }

    public override string ToString()
    {
      return this.username;
    }

    public bool isPasswordProtected()
    {
      return passwordProtected;
    }

    public void setPassword(String password)
    {
      this.passwordProtected = true;
      this.password = password;
    }

    public String getPassword()
    {
      return this.password;
    }
  }
}
