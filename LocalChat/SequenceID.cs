using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalChat
{
  public class SequenceID
  {
    public int id { get; set; }

    public SequenceID()
    {
      id = 0;
    }

    public SequenceID(int id)
    {
      this.id = id;
    }

    public String currentSequence()
    {
      String idNum = id.ToString();
      idNum = idNum.PadLeft(5, '0');
      id++;
      return idNum;
    }
  }
}
