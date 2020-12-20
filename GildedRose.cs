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
        DoUpdateQuality(item);
      }
    }

    private static void DoUpdateQuality(Item item)
    {
      switch (item.Name)
      {
        case AGED_BRIE:
          {
            if (item.Quality < 50)
            {
              item.Quality = item.Quality + 1;
            }

            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0)
            {
              {
                if (item.Quality < 50)
                {
                  item.Quality = item.Quality + 1;
                }
              }
            }

            break;
          }
        case BACKSTAGE_PASSES_TO_CONCERT:
          {
            if (item.Quality < 50)
            {
              item.Quality = item.Quality + 1;

              if (item.SellIn < 11)
              {
                if (item.Quality < 50)
                {
                  item.Quality = item.Quality + 1;
                }
              }

              if (item.SellIn < 6)
              {
                if (item.Quality < 50)
                {
                  item.Quality = item.Quality + 1;
                }
              }
            }

            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0)
            {
              item.Quality = 0;
            }

            break;
          }
        case SULFURAS_HAND_OF_RAGNAROS:
          break;
        default:
          {
            if (item.Quality > 0)
            {
              item.Quality = item.Quality - 1;
            }
            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0)
            {
              if (item.Quality > 0)
              {
                item.Quality = item.Quality - 1;
              }
            }

            break;
          }
      }
    }
  }
}
