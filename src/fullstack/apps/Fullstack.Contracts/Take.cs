namespace Fullstack.Contracts
{
  public record struct Take
  {
    public int Amount { get; init; }

    public Take(int amount)
    {
      Amount = amount;
    }
    public Take()
    {
      Amount = 10;
    }

    public bool IsValid()
    {
      return Amount > 0;
    }
  }
}
