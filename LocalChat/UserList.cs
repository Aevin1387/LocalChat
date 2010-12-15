using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Net;

namespace LocalChat
{
    public partial class UserList : Form
    {
        Socket bCastSocket;
        IPEndPoint bCastEP;
        IPEndPoint rcvEP;
        UdpClient clientReceive;
        int bCastPort = 8888;
        //char[] username;
        String username;
        SequenceID bCastSeq;
        //IPEndPoint bCastEP; 

        public void initializeSockets(){
            // Initialize socket for Broadcast sending
            bCastSocket = new Socket(AddressFamily.InterNetwork, 
                                     SocketType.Dgram, ProtocolType.Udp);
            bCastSocket.EnableBroadcast = true;
            IPAddress broadcast = IPAddress.Broadcast;
            bCastEP = new IPEndPoint(broadcast, bCastPort);

            // Initialize socket for receiving packets
            clientReceive = new UdpClient(bCastPort);
            rcvEP = new IPEndPoint(IPAddress.Any, bCastPort);
            bCastSeq = new SequenceID();
        }

        private void broadcastSend()
        {
            char[] message = setupPacket("BCST", username,
                "", '~', "");

            bCastSocket.SendTo(Encoding.ASCII.GetBytes(message), bCastEP);
        }

        private void broadcastListen()
        {
            byte[] rcv = clientReceive.Receive(ref rcvEP);
            char[] rcvCStr = Encoding.ASCII.GetChars(rcv, 0, 183 * sizeof(char));
            String rcvStr = rcvCStr.ToString();

        }

        public char[] setupPacket(String command, String pUsername, String sequence, 
                           char encFlag, String message){
            String strMessage = command + pUsername.PadRight(32, '~') +
               sequence.PadRight(5, '~') + encFlag +
               message.PadRight(140, '~');
            char[] partialMessage = strMessage.ToCharArray(0, 182);
            char[] fullMessage = new char[183];
            partialMessage.CopyTo(fullMessage, 0);
            fullMessage[182] = '\0';
            partialMessage = null;
            return fullMessage;
        }

        public void setupUsername(String uName)
        {
            username = uName.PadRight(32, '~');
            //username = userName.ToCharArray(0, 32);
        }

        public char[] getSequenceNum()
        {
            char[] num = {'0', '0', '0', '0', '1'};
            return num;
        }

        public UserList(String uName)
        {
            InitializeComponent();
            initializeSockets();
            //setupUsername(uName);
            username = uName;
            broadcastSend();
           
        }

        private void userBACKTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}
