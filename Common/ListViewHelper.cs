// Decompiled with JetBrains decompiler
// Type: Common.ListViewHelper
// Assembly: DT-9961T, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 681D06A8-439A-4E73-B916-52A7EBFBD7ED
// Assembly location: C:\Program Files (x86)\Multimeter\Multimeter.exe

using System.Windows.Forms;

namespace Common
{
  public class ListViewHelper
  {
    public static void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      ListView listView = sender as ListView;
      if (e.Column == (listView.ListViewItemSorter as ListViewColumnSorter).SortColumn)
      {
        if ((listView.ListViewItemSorter as ListViewColumnSorter).Order == SortOrder.Ascending)
          (listView.ListViewItemSorter as ListViewColumnSorter).Order = SortOrder.Descending;
        else
          (listView.ListViewItemSorter as ListViewColumnSorter).Order = SortOrder.Ascending;
      }
      else
      {
        (listView.ListViewItemSorter as ListViewColumnSorter).SortColumn = e.Column;
        (listView.ListViewItemSorter as ListViewColumnSorter).Order = SortOrder.Ascending;
      }
      ((ListView) sender).Sort();
    }
  }
}
