using System.Collections.Generic;

namespace csharpcore
{
  public class GildedRose
  {
    readonly IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
      this.Items = Items;
    }

    public void UpdateQuality()
    {
      foreach (var item in Items)
      {
        ((IUpdateItem)item).DoUpdateSellIn();
        ((IUpdateItem)item).DoUpdateQuality();
      }
    }

  }
}
