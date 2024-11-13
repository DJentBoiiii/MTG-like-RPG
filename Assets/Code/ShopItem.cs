[System.Serializable]
public class ShopItem
{
    public Card Card { get; set; }
    public string CardName { get; set; }
    public int Price { get; set; }
    public int Amount { get; set; }

    public ShopItem(Card card, int price, int amount)
    {
        Card = card;
        Price = price;
        Amount = amount;
    }
}
