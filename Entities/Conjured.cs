namespace csharpcore
{
  public class Conjured : Item, IUpdateItem
  {
    public void DoUpdateQuality()
    {
    
      if (SellIn < 0)
      {
          Quality = Quality - 4;
      }
      else Quality = Quality - 2;
      
      SetMaxQualityValues();
    }

    public void DoUpdateSellIn()
    {
      SellIn -= 1;
    }

    private void SetMaxQualityValues()
    {
      if (Quality < 0)
      {
        Quality = 0;
      }
      else if (Quality > 50)
      {
        Quality = 50;
      }
    }

  }
}
