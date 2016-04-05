// Decompiled with JetBrains decompiler
// Type: Common.ListViewColumnSorter
// Assembly: DT-9961T, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 681D06A8-439A-4E73-B916-52A7EBFBD7ED
// Assembly location: C:\Program Files (x86)\Multimeter\Multimeter.exe

using System.Collections;
using System.Windows.Forms;

namespace Common
{
  public class ListViewColumnSorter : IComparer
  {
    private int ColumnToSort;
    private SortOrder OrderOfSort;
    private CaseInsensitiveComparer ObjectCompare;

    public int SortColumn
    {
      get
      {
        return this.ColumnToSort;
      }
      set
      {
        this.ColumnToSort = value;
      }
    }

    public SortOrder Order
    {
      get
      {
        return this.OrderOfSort;
      }
      set
      {
        this.OrderOfSort = value;
      }
    }

    public ListViewColumnSorter()
    {
      this.ColumnToSort = 0;
      this.OrderOfSort = SortOrder.None;
      this.ObjectCompare = new CaseInsensitiveComparer();
    }

    public int Compare(object x, object y)
    {
      ListViewItem listViewItem1 = (ListViewItem) x;
      ListViewItem listViewItem2 = (ListViewItem) y;
      int num;
      try
      {
        num = this.ObjectCompare.Compare((object) listViewItem1.SubItems[this.ColumnToSort].Text, (object) listViewItem2.SubItems[this.ColumnToSort].Text);
      }
      catch
      {
        return 0;
      }
      if (this.OrderOfSort == SortOrder.Ascending)
        return num;
      if (this.OrderOfSort == SortOrder.Descending)
        return -num;
      return 0;
    }
  }
}
