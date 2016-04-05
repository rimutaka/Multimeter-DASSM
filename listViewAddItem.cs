// Decompiled with JetBrains decompiler
// Type: DT_3391.listViewAddItem
// Assembly: DT-9961T, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 681D06A8-439A-4E73-B916-52A7EBFBD7ED
// Assembly location: C:\Program Files (x86)\Multimeter\Multimeter.exe

using System.Windows.Forms;

namespace DT_3391
{
  internal class listViewAddItem
  {
    public ListViewItem listView_AddItem(string No, string Fun, string Data, string Uint, string day, string Time)
    {
      return new ListViewItem(No)
      {
        SubItems = {
          Fun,
          Data,
          Uint,
          day,
          Time
        }
      };
    }
  }
}
