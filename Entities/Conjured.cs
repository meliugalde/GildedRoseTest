namespace csharpcore
{
  public class Conjured : Item, IUpdateItem
  {
    public void DoUpdateQuality()
    {
      if (Quality > 0)
      {
        Quality = Quality - 2;

      }
      SellIn = SellIn - 1;

      if (SellIn < 0)
      {
        if (Quality > 0)
        {

          Quality = Quality - 2;
        }
      }

    }
  }
}
