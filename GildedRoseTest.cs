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
        IList<Item> Items = new List<Item> {ItemFactory.CreateItem(name, sellIn, quality)};
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        string itemString = Items[0].Name + "," + Items[0].SellIn + "," + Items[0].Quality;
        return itemString;
      }  

      [Fact]
      public void UpdateQuality_AgedBrie_ReturnsQualityIncreased()
      {
        //Arrange
        Item actual = ItemFactory.CreateItem("Aged Brie",2,  0);
        Item expected = ItemFactory.CreateItem("Aged Brie",  1,  1 );
        List<Item> Items = new List<Item> { actual };
        GildedRose app = new GildedRose(Items);

        //Act
        app.UpdateQuality();

        //Assert
        Assert.Equal(expected.Quality, Items[0].Quality);
        Assert.Equal(expected.Name, Items[0].Name);
    }

    [Fact]
    public void UpdateQuality_AgedBrie_ReturnsQualityIncreased_n150()
    {
      //Arrange
      Item actual = ItemFactory.CreateItem("Aged Brie", -1, 50);
      Item expected = ItemFactory.CreateItem("Aged Brie", -2, 50);
      List<Item> Items = new List<Item> { actual };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.Equal(expected.Quality, Items[0].Quality);
      Assert.Equal(expected.SellIn, Items[0].SellIn);
      Assert.Equal(expected.Name, Items[0].Name);
    }

    [Fact]
    public void UpdateQuality_AgedBrie_ReturnsSellInDecreased()
    {
      //Arrange
      Item actual = ItemFactory.CreateItem("Aged Brie", 2,  0 );
      Item expected = ItemFactory.CreateItem("Aged Brie", 1, 1 );
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
      Item actual = ItemFactory.CreateItem("Aged Brie",  0, quality );
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
      Item actual = ItemFactory.CreateItem("Aged Brie",  1, quality);
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
        ItemFactory.CreateItem( "+5 Dexterity Vest",  10,  20),
        ItemFactory.CreateItem( "Aged Brie", 2,  0),
        ItemFactory.CreateItem("Elixir of the Mongoose", 5, 7),
        ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert",15, 20),
        ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 10, 49),
        ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 5, 49)
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
        ItemFactory.CreateItem( "+5 Dexterity Vest",  10,  20),
        ItemFactory.CreateItem( "Aged Brie", 2,  0),
        ItemFactory.CreateItem("Elixir of the Mongoose", 5, 7),
        ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert",15, 20),
        ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 10, 49),
        ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 5, 49)
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
      Item item = ItemFactory.CreateItem("foo",  2, 0 );

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
      Item sulfuras1 = ItemFactory.CreateItem("Sulfuras, Hand of Ragnaros",  -1, 80 );
      Item sulfuras2 = ItemFactory.CreateItem("Sulfuras, Hand of Ragnaros",  0, 80);
      Item sulfuras3 = ItemFactory.CreateItem("Sulfuras, Hand of Ragnaros",  1, 80 );
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
      Item sulfuras1 = ItemFactory.CreateItem("Sulfuras, Hand of Ragnaros", -1, 80);
      Item sulfuras2 = ItemFactory.CreateItem("Sulfuras, Hand of Ragnaros", 0, 80);
      Item sulfuras3 = ItemFactory.CreateItem("Sulfuras, Hand of Ragnaros", 1, 80);

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
      Item backstage1 = ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 10,20);
    
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
      Item backstage1 = ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert",5, 20);

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
      Item backstage1 = ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 0, 20);

      //Arrange
      List<Item> Items = new List<Item> { backstage1 };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].Quality == 0);

    }

    [Fact]
    public void UpdateQuality_Backstage6_0_ReturnsQuality0()
    {
      Item backstage1 = ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 6, 0);

      //Arrange
      List<Item> Items = new List<Item> { backstage1 };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].Quality == 2);
      Assert.True(Items[0].SellIn == 5);

    }

    [Fact]
    public void UpdateQuality_Backstage6_1_ReturnsQuality0()
    {
      Item backstage1 = ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 6, 1);

      //Arrange
      List<Item> Items = new List<Item> { backstage1 };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].Quality == 3);
      Assert.True(Items[0].SellIn == 5);

    }

    [Fact]
    public void UpdateQuality_Backstage6_49_ReturnsQuality0()
    {
      Item backstage1 = ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 6, 49);

      //Arrange
      List<Item> Items = new List<Item> { backstage1 };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].Quality == 50);
      Assert.True(Items[0].SellIn == 5);

    }

    [Fact]
    public void UpdateQuality_Backstagen1_50_ReturnsQuality0()
    {
      Item backstage1 = ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", -1, 50);

      //Arrange
      List<Item> Items = new List<Item> { backstage1 };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].Quality == 0);
      Assert.True(Items[0].SellIn == -2);

    }

    [Fact]
    public void UpdateQuality_Backstage6_50_ReturnsQuality0()
    {
      Item backstage1 = ItemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 6, 50);

      //Arrange
      List<Item> Items = new List<Item> {backstage1};
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(Items[0].Quality == 50);
      Assert.True(Items[0].SellIn == 5);
    }

    [Fact]
    public void UpdateQuality_ConjuredQualityDecreaseTwiceAsFast_SellInGreaterThan0_ReturnsTrue()
    {
      //Arrange
      int quality = 20;
      Item item = ItemFactory.CreateItem("Conjured Mana Cake", 10, quality);
      Item standardItem = ItemFactory.CreateItem("foo", 10, quality);

      IList<Item> Items = new List<Item> { item, standardItem };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      int qualityDifference = quality - standardItem.Quality;
      Assert.True(item.Quality == quality -(qualityDifference*2));
    }

    [Fact]
    public void UpdateQuality_ConjuredQualityDecreaseTwiceAsFast_SellIn0_ReturnsTrue()
    {
      //Arrange
      int quality = 20;
      Item item = ItemFactory.CreateItem("Conjured Mana Cake", 0, quality);
      Item standardItem = ItemFactory.CreateItem("foo", 0, quality);

      IList<Item> Items = new List<Item> { item, standardItem };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      int qualityDifference = quality - standardItem.Quality;
      Assert.True(item.Quality == quality -(qualityDifference*2));
      
    }

    [Fact]
    public void UpdateQuality_ConjuredQualityDecreaseTwiceAsFast_SellInLesserThan0_ReturnsTrue()
    {
      //Arrange
      int quality = 20;
      Item item = ItemFactory.CreateItem("Conjured Mana Cake", -1, quality);
      Item standardItem = ItemFactory.CreateItem("foo", -1, quality);

      IList<Item> Items = new List<Item> { item, standardItem };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      int qualityDifference = quality - standardItem.Quality;
      Assert.True(item.Quality == quality - (qualityDifference * 2));
    }

    [Fact]
    public void UpdateQuality_ConjuredQualityDecreaseTwiceAsFast_Quality0_ReturnsTrue()
    {
      //Arrange
      int quality = 0;

      Item item = ItemFactory.CreateItem("Conjured Mana Cake", 1, quality);
      Item standardItem = ItemFactory.CreateItem("foo", 1, quality);

      IList<Item> Items = new List<Item> { item, standardItem };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      int qualityDifference = quality - standardItem.Quality;
      Assert.True(item.Quality == quality - (qualityDifference * 2));
      Assert.True(item.Quality >= 0);
    }

    [Fact]
    public void UpdateQuality_ConjuredQualityDecreaseTwiceAsFast_ReturnsTrue()
    {
      //Arrange
      int quality = 6;

      Item item = ItemFactory.CreateItem("Conjured Mana Cake", 3, quality);
      Item standardItem = ItemFactory.CreateItem("foo", 3, quality);

      IList<Item> Items = new List<Item> { item, standardItem };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      int qualityDifference = quality - standardItem.Quality;
      Assert.True(item.Quality == quality - (qualityDifference * 2));
      Assert.True(item.Quality >= 0);
      Assert.True(item.SellIn == standardItem.SellIn);
    }

    [Fact]
    public void UpdateQuality_ConjuredQualityDecreaseTwiceAsFast_ReturnsQualityDifferent()
    {
      //Arrange
      int quality = 6;

      Item item = ItemFactory.CreateItem("Conjured Mana Cake", 3, quality);
      Item standardItem = ItemFactory.CreateItem("foo", 3, quality);

      IList<Item> Items = new List<Item> { item, standardItem };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      int qualityDifference = quality - standardItem.Quality;
      Assert.False(item.Quality != quality - (qualityDifference * 2));
      Assert.False(item.SellIn != standardItem.SellIn);
    }

    
    [Fact]
    public void UpdateQuality_ConjuredQualityDecreaseTwiceAsFast_QualityLesserThan0_ReturnsQuality()
    {
      //Arrange
      int quality = -1;

      Item item = ItemFactory.CreateItem("Conjured Mana Cake", -1, quality);
      Item standardItem = ItemFactory.CreateItem("foo", -1, quality);

      IList<Item> Items = new List<Item> { item, standardItem };
      GildedRose app = new GildedRose(Items);

      //Act
      app.UpdateQuality();

      //Assert
      Assert.True(item.Quality == quality);
    
    }

  }
}
