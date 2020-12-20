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

      [Fact]
      public void UpdateQuality_AgedBrie_ReturnsQualityIncreased()
      {
        //Arrange
        Item actual = new Item {Name = "Aged Brie", SellIn = 2, Quality = 0};
        Item expected = new Item { Name = "Aged Brie", SellIn = 1, Quality = 1 };
        List<Item> Items = new List<Item> { actual };
        GildedRose app = new GildedRose(Items);

        //Act
        app.UpdateQuality();

        //Assert
        Assert.Equal(expected.Quality, Items[0].Quality);
        Assert.Equal(expected.Name, Items[0].Name);
    }

    [Fact]
    public void UpdateQuality_AgedBrie_ReturnsSellInDecreased()
    {
      //Arrange
      Item actual = new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 };
      Item expected = new Item { Name = "Aged Brie", SellIn = 1, Quality = 1 };
      List<Item> Items = new List<Item> { actual };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.Equal(expected.SellIn, Items[0].SellIn);
    }

    [Fact]
    public void UpdateQuality_AgedBrieSellIn0_ReturnsQualityDegradesTwice()
    {
      //Arrange
      int quality = 0;
      Item actual = new Item { Name = "Aged Brie", SellIn = 0, Quality = quality };
      List<Item> Items = new List<Item> { actual };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.Equal(2, Items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_AgedBrieSellIn1_ReturnsQualityNotDegradesTwice()
    {
      //Arrange
      int quality = 0;
      Item actual = new Item { Name = "Aged Brie", SellIn = 1, Quality = quality};
      List<Item> Items = new List<Item> { actual };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.False( quality + 2 == Items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_ItemsQualityLessThan50_ReturnsTrue()
    {
      //Arrange
      IList<Item> Items = new List<Item>
      {
        new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
        new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
        new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
        new Item
        {
          Name = "Backstage passes to a TAFKAL80ETC concert",
          SellIn = 15,
          Quality = 20
        },
        new Item
        {
          Name = "Backstage passes to a TAFKAL80ETC concert",
          SellIn = 10,
          Quality = 49
        },
        new Item
        {
          Name = "Backstage passes to a TAFKAL80ETC concert",
          SellIn = 5,
          Quality = 49
        }
      };
     
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.All(Items, item => Assert.InRange(item.Quality, 0, 50));
    }

    [Fact]
    public void UpdateQuality_ItemsQualityGreaterThan0_ReturnsTrue()
    {
      //Arrange
      IList<Item> Items = new List<Item>
      {
        new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
        new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
        new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
        new Item
        {
          Name = "Backstage passes to a TAFKAL80ETC concert",
          SellIn = 15,
          Quality = 20
        },
        new Item
        {
          Name = "Backstage passes to a TAFKAL80ETC concert",
          SellIn = 10,
          Quality = 49
        },
        new Item
        {
          Name = "Backstage passes to a TAFKAL80ETC concert",
          SellIn = 5,
          Quality = 49
        }
      };

      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.All(Items, item => Assert.True(item.Quality >= 0));
    }

    
    [Fact]
    public void UpdateQuality_StandardsItemsQualityNotBeLowerThanZero_ReturnsTrue()
    {
      //Arrange
      Item item = new Item { Name = "foo", SellIn = 2, Quality = 0 };

      IList<Item> Items = new List<Item> { item };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].Quality >= 0);
    }

    [Fact]
    public void UpdateQuality_SulfurasQuality80_ReturnsTrue()
    {
      Item sulfuras1 = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 };
      Item sulfuras2 = new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80};
      Item sulfuras3 = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 80 };
      //Arrange
      IList<Item>  Items = new List<Item> { sulfuras1, sulfuras2, sulfuras3 };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].Quality == 80);
      Assert.True(Items[1].Quality == 80);
      Assert.True(Items[2].Quality == 80);
    }

    [Fact]
    public void UpdateQuality_SulfurasSellInDoesNotModified_ReturnsTrue()
    {
      Item sulfuras1 = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 };
      Item sulfuras2 = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
      Item sulfuras3 = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 80 };
      //Arrange
      List<Item> Items = new List<Item> { sulfuras1, sulfuras2, sulfuras3 };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].SellIn == -1);
      Assert.True(Items[1].SellIn == 0);
      Assert.True(Items[2].SellIn == 1);
    }

    [Fact]
    public void UpdateQuality_Backstage_ReturnsQualityIncreasedBy2()
    {
      Item backstage1 = new Item
      {
        Name = "Backstage passes to a TAFKAL80ETC concert",
        SellIn = 10,
        Quality = 20
      };
    
      //Arrange
      List<Item> Items = new List<Item> { backstage1 };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].Quality == 22);
      
    }

    [Fact]
    public void UpdateQuality_Backstage_ReturnsQualityIncreasedBy3()
    {
      Item backstage1 = new Item
      {
        Name = "Backstage passes to a TAFKAL80ETC concert",
        SellIn = 5,
        Quality = 20
      };

      //Arrange
      List<Item> Items = new List<Item> { backstage1 };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].Quality == 23);

    }

    [Fact]
    public void UpdateQuality_Backstage_ReturnsQuality0()
    {
      Item backstage1 = new Item
      {
        Name = "Backstage passes to a TAFKAL80ETC concert",
        SellIn = 0,
        Quality = 20
      };

      //Arrange
      List<Item> Items = new List<Item> { backstage1 };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].Quality == 0);

    }
  }
}
