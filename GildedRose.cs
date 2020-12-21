using System.Collections.Generic;

namespace csharpcore
{
  public class GildedRose
  {
    readonly IList<Item> Items;

    private const string AGED_BRIE = "Aged Brie";
    private const string BACKSTAGE_PASSES_TO_CONCERT = "Backstage passes to a TAFKAL80ETC concert";
    private const string SULFURAS_HAND_OF_RAGNAROS = "Sulfuras, Hand of Ragnaros";

    public GildedRose(IList<Item> Items)
    {
      this.Items = Items;
    }

    public void UpdateQuality()
    {
      foreach (var item in Items)
      {
        ((IUpdateItem)item).DoUpdateQuality();
      }
    }

  }
}
