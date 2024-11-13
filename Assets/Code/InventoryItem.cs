using System;

[Serializable]
public class InventoryItem
{
    public Card Card;
    public int Quantity;

    public InventoryItem(Card card, int quantity)
    {
        Card = card;
        Quantity = quantity;
    }
}
