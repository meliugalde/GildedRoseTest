namespace csharpcore
{
  public class StandardItem : Item, IUpdateItem
  {
    public void DoUpdateQuality()
    {
      {
        if (Quality > 0)
        {
          Quality = Quality - 1;

        }
        SellIn = SellIn - 1;

        if (SellIn < 0)
        {
          if (Quality > 0)
          {

            Quality = Quality - 1;
          }
        }
      }

    }
  }
}
