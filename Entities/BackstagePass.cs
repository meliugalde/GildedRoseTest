namespace csharpcore
{
  public class BackstagePass : Item, IUpdateItem
  {
    public void DoUpdateQuality()
    {
        if (Quality <= 50)
        {
          if (SellIn < 0)
          {
            Quality = 0;
          }
          else if (SellIn < 5)
          {
            Quality += 3;
          }
          else if (SellIn < 10)
          {
             Quality += 2;
          }
          else Quality += 1;
        }

        Quality = Helper.SetMaxQualityValues(Quality);
    }

    public void DoUpdateSellIn()
    {
      SellIn -= 1;
    }
  }
}
