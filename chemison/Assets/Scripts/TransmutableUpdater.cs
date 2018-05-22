using System.Collections.Generic;
using UnityEngine;

public class TransmutableUpdater : Singleton<TransmutableUpdater>
{
  struct Entry
  {
    public Transmutable t;
    public Element e;
  }

  HashSet<Entry> updatesThisFrame = new HashSet<Entry>();
  HashSet<Entry> updatesNextFrame = new HashSet<Entry>();

  public void Enqueue(Transmutable t, Element e)
  {
    updatesThisFrame.Add(new Entry { t = t, e = e });
  }

  void RunAll()
  {
    var temp = updatesThisFrame;
    updatesThisFrame = updatesNextFrame;
    updatesThisFrame.Clear();
    foreach (var entry in temp)
    {
      if (entry.t != null)
      {
        entry.t.Apply(entry.e);
      }
    }
    temp.Clear();
    updatesNextFrame = temp;
  }

  void LateUpdate()
  {
    RunAll();
  }
}
