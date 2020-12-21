using System;
using System.Collections.Generic;
using System.Text;

namespace csharpcore
{
  public static class ItemFactory
  {
    private const string AGED_BRIE = "Aged Brie";
    private const string BACKSTAGE_PASSES_TO_CONCERT = "Backstage passes to a TAFKAL80ETC concert";
    private const string SULFURAS_HAND_OF_RAGNAROS = "Sulfuras, Hand of Ragnaros";
    
    public static Item CreateItem(string name, int sellin, int quality)
    {
      switch (name)
      {
        case AGED_BRIE:
        {
          return new AgedBrie { Name = AGED_BRIE, SellIn = sellin, Quality = quality };
        }
        case BACKSTAGE_PASSES_TO_CONCERT:
        {
          return new BackstagePass { Name = BACKSTAGE_PASSES_TO_CONCERT, SellIn = sellin, Quality = quality };
        }
        case SULFURAS_HAND_OF_RAGNAROS:
          return new Sulfuras { Name = SULFURAS_HAND_OF_RAGNAROS, SellIn = sellin, Quality = quality };
        default:
        {
          return new StandardItem { Name = name, SellIn = sellin, Quality = quality };
        }
      }
    }
  }
}
