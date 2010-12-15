using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Timers;
using System.Net;

namespace LocalChat
{
  public class SendMessage 
  {
    private int timeoutCounter { get; set; }
    public Message toSend { get; set; }
    private ChatFrm owner;
    private Timer MACKTimer;
    private UdpClient sendClient;
    private IPEndPoint sendEP;
    private User chatUser;
    private String thisUser;
    public int iSequenceID;

    private void SocketSend() {
      sendClient = new UdpClient();
      sendEP = new IPEndPoint(toSend.sndUser.userAddress, StaticVars.msgPort);
      String sequenceID = chatUser.sequence.currentSequence();
      iSequenceID = Int32.Parse(sequenceID);
      char[] sendMsg;
      if (chatUser.isPasswordProtected())
      {
        String encMsg = Message.xorMsg(toSend.messageStr,
          chatUser.getPassword());
        sendMsg = Message.setupPacket("MESG", thisUser,
                          sequenceID, '1', encMsg);
      }
      else
      {
        sendMsg = Message.setupPacket("MESG", thisUser,
                            sequenceID, '0', toSend.messageStr);
      }

      sendClient.Send(Encoding.ASCII.GetBytes(sendMsg), sendMsg.Length, 
        sendEP);
      

    }

    public SendMessage(Message toSend, User chatUser, String thisUser,
                       ChatFrm owner)
    {
      this.timeoutCounter = 0;
      this.toSend = toSend;
      this.owner = owner;
      this.chatUser = chatUser;
      this.thisUser = thisUser;

      SocketSend();

      MACKTimer = new Timer();
      MACKTimer.Interval = 10;
      MACKTimer.Elapsed += new ElapsedEventHandler(MACKTimer_Tick);
      MACKTimer.Enabled = true;
      MACKTimer.Start();
 
    }


    public Boolean msgTimeOut()
    {
      if (timeoutCounter < 3)
      {
        timeoutCounter++;
        return true;
      }
      else
        return false;
    }

    public void MsgRcvd() 
    {

      MACKTimer.Stop();
      MACKTimer.Enabled = false;
    }

    private void MACKTimer_Tick(object sender, System.EventArgs e) 
    {
      
      if (++timeoutCounter >= 3) {
        owner.MessageError(this);
        MACKTimer.Stop();
        MACKTimer.Enabled = false;

      }
      else {
        MACKTimer.Interval = 1000;
        MACKTimer.Start();
      }
    }
  }
}
