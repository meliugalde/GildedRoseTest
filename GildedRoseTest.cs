using Xunit;
using System.Collections.Generic;
using ApprovalTests.Combinations;
using ApprovalTests.Reporters;

namespace csharpcore
{
    public class GildedRoseTest
    {

      [UseReporter(typeof(DiffReporter))]
      [Fact]
      public void updateQuality()
      {
        CombinationApprovals.VerifyAllCombinations(
          doUpdateQuality,
          new string[] { "foo", "Aged Brie", "Backstage passes to a TAFKAL80ETC concert", "Sulfuras, Hand of Ragnaros" },
          new int[] { -1, 0, 2, 6, 11 },
          new int[] { 0, 1, 49, 50 }
        );
      }

      private static string doUpdateQuality(string name, int sellIn, int quality)
      {
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        string itemString = Items[0].Name + "," + Items[0].SellIn + "," + Items[0].Quality;
        return itemString;
      }
  }
}
