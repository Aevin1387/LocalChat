using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalChat {
  public class Message {
    
    public String messageStr { get; set; }
    public User sndUser { get; set; }
    public DateTime time { get; set; }

    public static char[] setupPacket(String command, String pUsername,
                 String sequence, char encFlag,
                 String message) {
      String strMessage = command + pUsername.PadRight(32, '\0');

      if (command == "BCST" || command == "BACK") {
        // do nothing, default strMessage
      }
      else if (command == "MESG") {
        strMessage += sequence.PadLeft(5, '0') + encFlag +
                      message.PadRight(140, '\0');
      }
      else if (command == "MACK") {
        strMessage += sequence.PadLeft(5, '0');
      }

      char[] partialMessage = strMessage.ToCharArray(0, strMessage.Length);
      char[] fullMessage = new char[strMessage.Length + 1];
      partialMessage.CopyTo(fullMessage, 0);
      fullMessage[strMessage.Length] = '\0';

      /* Garbage Collection */
      partialMessage = null;
      strMessage = null;

      return fullMessage;
    }


    public Message(String messageStr, User sndUser) {
      this.messageStr = messageStr;
      this.sndUser = sndUser;
      this.time = DateTime.Now;
    }

    public static String xorMsg(String toXor, String password)
    {
      int i = 0;
      String result = "";
      foreach (Char j in toXor)
      {
        result += (char)(j ^ password[i]);
        if (i < 139)
        {
          i++;
        }
        else
          i = 0;
      }
      return result;
    }
  }
}
