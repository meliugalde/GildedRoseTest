namespace csharpcore
{
  public class StandardItem : Item, IUpdateItem
  {
    public void DoUpdateQuality()
    {
      if (SellIn < 0)
      {
        Quality = Quality - 2;
      }
      else Quality = Quality - 1;

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
