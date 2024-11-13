using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<Card> Cards;
    public List<InventoryItem> PlayerInventory;
    public List<ShopItem> ShopInventory;

    public GameData()
    {
        Cards = new List<Card>();
        PlayerInventory = new List<InventoryItem>();
        ShopInventory = new List<ShopItem>();
    }
}
