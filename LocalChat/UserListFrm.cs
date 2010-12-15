using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace LocalChat
{
  public partial class UserListFrm : Form
  {
    Socket bCastSocket;
    IPEndPoint bCastEP;
    IPEndPoint bCastRCVEP;
    IPEndPoint msgRCVEP;
    UdpClient bCastReceive;
    UdpClient msgClient;

    
    public List<User> UserList;
    List<User> newUsers = new List<User>();
    ChatListWithChangedEvent newChats;


    String username;
    SequenceID bCastSeq;
    List<IPAddress> BACKlist = new List<IPAddress>();
    
    Boolean timerSet = false;
    System.Timers.Timer userBACKTimer;
    Thread lThread;
    Thread rcvThread;
    Boolean lThreadKill = false;
    Boolean rcvThreadKill = false;

    public void initializeSockets(){
      // Initialize socket for Broadcast sending
      bCastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 
                               ProtocolType.Udp);
      bCastSocket.EnableBroadcast = true;
      IPAddress broadcast = IPAddress.Broadcast;
      bCastEP = new IPEndPoint(broadcast, StaticVars.bCastPort);

      // Initialize socket for receiving Broadcast packets
      try {
        bCastReceive = new UdpClient(StaticVars.bCastPort);
      }
      catch (SocketException) {
        MessageBox.Show("Failed to bind Broadcast Port 8888. Please make " + 
                        "sure it is not in use.", "Error: Failed to bind port",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
      }

      // Initialize a socket for sending/receiving packets
      try {
        msgClient = new UdpClient(StaticVars.msgPort);
      }
      catch (SocketException) {
        MessageBox.Show("Failed to bind Message Port  Please make sure it " +
                        "is not in use.", "Error: Failed to bind port 9999",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
      }
      

      bCastRCVEP = new IPEndPoint(IPAddress.Any, 0);
      // Separate endpoint for messages in case of receiving bCast &&
      // msg at the same time
      msgRCVEP = new IPEndPoint(IPAddress.Any, 0);
      bCastSeq = new SequenceID();
    }

    private void broadcastSend()
    {
      char[] message = Message.setupPacket("BCST", username,
        "", '\0', "");

      bCastSocket.SendTo(Encoding.ASCII.GetBytes(message), bCastEP);

      // Memory manage
      message = null;
    }

    public User checkAndAddUser(String uName, IPAddress rcvIP) {
      foreach (User client in UserList) {
        if (client.username == uName)
          return client;
      }
      foreach (User client in newUsers)
      {
        if (client.username == uName)
          return client;
      }
      User newClient = new User(rcvIP, uName);
      newUsers.Add(newClient);
      return newClient;
    }

   

    private void broadcastListen()
    {
      while (true) {
        bCastReceive.Client.ReceiveTimeout = 100;
        try {
          byte[] rcv = bCastReceive.Receive(ref bCastRCVEP);
          char[] rcvCStr = Encoding.ASCII.GetChars(rcv, 0, rcv.Length);
          String rcvStr = new String(rcvCStr);
          String uName = rcvStr.Substring(4, 32).Trim('\0');

          if (rcvStr.StartsWith("BCST") && uName != username) {
            IPAddress rcvIP = bCastRCVEP.Address;
            Random timer = new Random();
            checkAndAddUser(uName, rcvIP);
            BACKlist.Add(rcvIP);

            if (!timerSet) {
              userBACKTimer.Interval = timer.Next(0, 1000);
              userBACKTimer.Start();
              timerSet = true;
            }

            // Manage memory
            timer = null;
          }
          else if (rcvStr.StartsWith("BACK") && 
                uName != username) {
            IPAddress rcvIP = bCastRCVEP.Address;
            checkAndAddUser(uName, rcvIP);
          }

          // Manage memory
          rcvCStr = null;
          rcvStr = null;
          uName = null;
          rcv = null;
        }

        // Catch the exception for when the receive times out
        // If the thread needs to be killed, exit the thread
        catch (SocketException) {
          if (lThreadKill)
            Application.ExitThread();
        }
      }
    }

    private void threadSafeActivateChat(ChatFrm thisChat)
    {
      Control sync = this.ActiveControl;
      if(sync != null && sync.InvokeRequired){
        sync.Invoke((Action) delegate { threadSafeActivateChat(thisChat); });
        return;
      }
      if(!thisChat.Visible)
        this.Show();
      thisChat.Activate();
    }

    /*private void createChat(User chatuser, String username){
      if(mainControl != null && mainControl.InvokeRequired){
        mainControl.Invoke(createChat)*/

    private void messageListen()
    {

      
      while (true)
      {
        msgClient.Client.ReceiveTimeout = 100;
        try
        {
          byte[] rcv = msgClient.Receive(ref msgRCVEP);
          
            char[] rcvCStr = Encoding.ASCII.GetChars(rcv, 0, rcv.Length);
            String rcvStr = new String(rcvCStr);
            String uName = rcvStr.Substring(4, 32).Trim('\0');
            bool isEncrypted = false;
            if(rcvStr.Substring(41, 1)[0] != '\0')
             isEncrypted = (Int32.Parse(rcvStr.Substring(41, 1)) == 1);

            if (rcvStr.StartsWith("MESG"))
            {
              IPAddress rcvIP = msgRCVEP.Address;
              User chatUser = checkAndAddUser(uName, rcvIP);
              ChatFrm thisChat = null;
              bool chatOpen = false;

              int sequenceID = Int32.Parse(rcvStr.Substring(36, 5));
              char[] mackMSGStr = Message.setupPacket("MACK", username,
                                           sequenceID.ToString(), '0', "");
              IPEndPoint sendTo = new IPEndPoint(rcvIP, StaticVars.msgPort);
              UdpClient sendMACK = new UdpClient();
              sendMACK.Send(Encoding.ASCII.GetBytes(mackMSGStr),
                            mackMSGStr.Length, sendTo);
              
              if(chatUser.chatExists() && !chatUser.chatHidden()){
                thisChat = chatUser.getChat();
                threadSafeActivateChat(thisChat);
                chatOpen = true;
              }
              
              if(!chatOpen){  
                thisChat = new ChatFrm(chatUser, username, this);
                newChats.Add(thisChat);
                chatUser.openChat(ref thisChat);

              }
              if (isEncrypted && !chatUser.isPasswordProtected())
              {
                GetPassword window = new GetPassword(chatUser.username);
                while (window.ShowDialog() != DialogResult.OK) ;
                chatUser.setPassword(window.password);
              }

              String messageStr = rcvStr.Substring(42, 140).Trim('\0');
              Message receivedMessage = new Message(messageStr, 
                                                    thisChat.chatUser);

              

              
              if (!chatUser.receivedSequences.Contains(sequenceID))
              {
                chatUser.receivedSequences.Add(sequenceID);
                chatUser.receivedMessages.Add(receivedMessage);
              }
              
              
            }

            else if (rcvStr.StartsWith("MACK"))
            {
              foreach (User user in UserList)
              {
                if (user.username == uName)
                {
                  int sequenceID = Int32.Parse(rcvStr.Substring(36, 5));
                  SendMessage temp = null;
                  user.touchAwaitingMACK(false, ref temp, sequenceID);
                  break;
                }
              }
            }

            // Manage memory
            rcvCStr = null;
            rcvStr = null;
            uName = null;
          rcv = null;
        }

        // Catch the exception for when the receive times out
        // If the thread needs to be killed, exit the thread
        catch (SocketException)
        {
          if (rcvThreadKill)
            Application.ExitThread();
        }
      }
    }

    public void setupUserList() {
      UserList = new List<User>();
      lbUsers.DataSource = UserList;
      lbUsers.DisplayMember = "username";
    }

    public void setupBACKTimer(){
      userBACKTimer = new System.Timers.Timer();
      userBACKTimer.Elapsed += new 
        ElapsedEventHandler(userBACKTimer_Elapse);
      userBACKTimer.AutoReset = true;
    }

    public UserListFrm(String uName)
    {
      InitializeComponent();
      initializeSockets();
      username = uName;
      setupUserList();
      setupBACKTimer();
      newChats = new ChatListWithChangedEvent();

      broadcastSend();
      lThread = new Thread(new ThreadStart(this.broadcastListen));
      lThread.Name = "ListenThread";
      lThread.Start();
      while (!lThread.IsAlive) ;
      Thread.Sleep(1);
      newUserCheckTimer.Enabled = true;
      newUserCheckTimer.Interval = 100;
      newUserCheckTimer.Start();
      rcvThread = new Thread(new ThreadStart(this.messageListen));
      rcvThread.Name = "ReceiveThread";
      rcvThread.Start();

      while (!rcvThread.IsAlive) ;
      Thread.Sleep(1);
      newChats.Changed += new ChangedChatEventHandler(newChatEvent);
      
    }

    private void userBACKTimer_Elapse(object sender, EventArgs e)
    {
      lock (this)
      {
        if (BACKlist.Count > 0)
        {
          char[] message = Message.setupPacket("BACK", username, 
            "", '\0', "");
          IPEndPoint BACKEP = new IPEndPoint(BACKlist[0], 
                                             StaticVars.bCastPort);
          bCastSocket.SendTo(
            Encoding.ASCII.GetBytes(message), BACKEP);
          BACKEP = null;
          BACKlist.RemoveAt(0);
          if (BACKlist.Count() > 0)
          {
            Random timer = new Random();
            userBACKTimer.Interval = timer.Next(0, 1000);
          }
          else
          {
            userBACKTimer.Stop();
            timerSet = false;
          }
          message = null;
        }
      }
    }

    private void newUserCheckTimer_Tick(object sender, EventArgs e)
    {
        if (newUsers.Count != 0)
        {
          foreach (User newUser in newUsers)
          {
            if (newUser.username != username){
              UserList.Add(newUser);
              ((CurrencyManager)
                lbUsers.BindingContext[UserList]).Refresh();
            }
          }
          newUsers.Clear();
        }

        ((CurrencyManager)lbUsers.BindingContext[UserList]).Refresh();
    }

    private void newChatEvent(object sender, ChatEventArgs e)
    {
      Control sync = this.ActiveControl;
      if (sync != null && sync.InvokeRequired)
      {
        sync.Invoke((Action) delegate { newChatEvent(sender, e); });
        return;
      }


      e.newChat.Show();
      
    }

    private void USERLISTFRM_FRMCLSD(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }

    private void UserListFrm_FrmClsing(object sender, 
                       FormClosingEventArgs e)
    {

      lThreadKill = true;           
      lThread.Abort();
      rcvThreadKill = true;
      rcvThread.Abort();

    }

    private void lbUsers_DoubleClick(object sender, MouseEventArgs e)
    {
      Point p = Cursor.Position;
      p = lbUsers.PointToClient(p);
      int selectedIndex = lbUsers.IndexFromPoint(p);

      if (selectedIndex != -1)
      {
        // Check if a chat window is already open
        String selectedUsername = lbUsers.Items[selectedIndex].ToString();
        foreach (User user in UserList)
        {
          if (user.username == selectedUsername)
          {
            if (user.chatExists())
            {
              if (user.chatHidden())
              {
                user.getChat().Show();
              }
              user.getChat().Activate();
            }
            else
            {
              ChatFrm newChat = new ChatFrm(user, username, this);
              user.openChat(ref newChat);
              newChat.Show();
            }
          }
        }
      }

    }

    private void lbUsers_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == (char)Keys.Return)
      {
        int selectedIndex = lbUsers.SelectedIndex;
        if (selectedIndex != -1)
        {
          String selectedUsername = lbUsers.Items[selectedIndex].ToString();
          foreach (User user in UserList)
          {
            if (user.username == selectedUsername)
            {
              if (user.chatExists())
              {
                if (user.chatHidden())
                {
                  user.getChat().Show();
                }
                user.getChat().Activate();
              }
              else
              {
                ChatFrm newChat = new ChatFrm(user, username, this);
                user.openChat(ref newChat);
                newChat.Show();
              }
            }
          }
        }
        
        e.Handled = true;

        // Memory handle
      }
    }

    private void lbUsers_MouseDown(object sender, MouseEventArgs e)
    {
      Point p = Cursor.Position;
      p = lbUsers.PointToClient(p);
      int selectedIndex = lbUsers.IndexFromPoint(p);

      if(e.Button == MouseButtons.Right && selectedIndex != -1)
      {
        cmUser.Show(this, new Point(e.X, e.Y));
      }
    }


    private void cmUser_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      int selectedIndex = lbUsers.SelectedIndex;
      String selectedUsername = lbUsers.Items[selectedIndex].ToString();
      GetPassword window = new GetPassword(selectedUsername);
      if(window.ShowDialog() != DialogResult.OK)
        return;
      
      
      foreach (User user in UserList)
      {
        if (user.username == selectedUsername)
        {
          if (user.chatExists())
          {
            if (user.chatHidden())
            {
              user.setPassword(window.password);
              user.getChat().Show();
            }
            user.getChat().Activate();
          }
          else
          {
            ChatFrm newChat = new ChatFrm(user, username, this);
            user.setPassword(window.password);
            user.openChat(ref newChat);
            newChat.Show();
          }
        }
      }
    }
  }
}
