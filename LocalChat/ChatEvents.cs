using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Based on http://msdn.microsoft.com/en-us/library/aa645739(VS.71).aspx
namespace LocalChat {
  public class ChatEventArgs : EventArgs
  {
    public ChatFrm newChat { get; set; }
    public bool chatSet { get; set; }
    public bool test { get; set; }

    public ChatEventArgs(ChatFrm newChat)
    {
      this.newChat = newChat;
      chatSet = true;
    }

    public ChatEventArgs()
    {
      this.chatSet = false;
    }
  }

  public delegate void ChangedChatEventHandler(object sender, ChatEventArgs e);

  public class ChatListWithChangedEvent : List<ChatFrm>
  {
    public event ChangedChatEventHandler Changed;

    public virtual void OnChanged(ChatEventArgs e)
    {
      if (Changed != null)
        Changed(this, e);
    }

    public new void Add(ChatFrm value)
    {
      base.Add(value);
      ChatEventArgs e = new ChatEventArgs(value);
      OnChanged(e);
    }

    public new void Clear()
    {
      base.Clear();
      ChatEventArgs e = new ChatEventArgs();
      OnChanged(e);
    }


  }

  public class MessageEventArgs : EventArgs {
    public Message newMessage { get; set; }
    public bool messageSet { get; set; }
    public bool test { get; set; }

    public MessageEventArgs(Message newMessage) 
    {
      this.newMessage = newMessage;
      messageSet = true;
    }

    public MessageEventArgs()
    {
      this.messageSet = false;
    }
  }

  public delegate void ChangedEventHandler(object sender, MessageEventArgs e);

  public class MessageListWithChangedEvent : List<Message> 
  {
    public event ChangedEventHandler Changed;

    public virtual void OnChanged(MessageEventArgs e) 
    {
      if (Changed != null)
        Changed(this, e);
    }

    public new void Add(Message value) 
    {
      base.Add(value);
      MessageEventArgs e = new MessageEventArgs(value);
      OnChanged(e);
    }

    public new void Clear()
    {
      base.Clear();
      MessageEventArgs e = new MessageEventArgs();
      OnChanged(e);
    }

    
  }

 
}
