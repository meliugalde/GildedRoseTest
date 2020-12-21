namespace csharpcore
{
  public class Conjured : Item, IUpdateItem
  {
    public void DoUpdateQuality()
    {
    
      if (SellIn < 0)
      {
          Quality -= 4;
      }
      else Quality -= 2;

      Quality = Helper.SetMaxQualityValues(Quality);
    }

    public void DoUpdateSellIn()
    {
      SellIn -= 1;
    }

   
  }
}
