namespace csharpcore
{
  public class AgedBrie : Item, IUpdateItem
  {

    public void DoUpdateQuality()
    {
      
      if (SellIn < 0)
      {
        Quality += 2;
      }
      else Quality += 1;

      Quality = Helper.SetMaxQualityValues(Quality);
    }

    public void DoUpdateSellIn()
    {
      SellIn -= 1;
    }

    

  }
}
