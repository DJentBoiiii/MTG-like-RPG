using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CardManager : MonoBehaviour
{
    private string dataPath;
    private GameData gameData;

    void Awake()
    {
        dataPath = "My project_Data\\StreamingAssets\\GameDictionary.json";
        LoadGameData();
        Debug.Log("Data loaded successfully");
    }

    private void LoadGameData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            Debug.Log("JSON data loaded: " + json);
            gameData = JsonUtility.FromJson<GameData>(json);

            // Додаткове логування для перевірки даних
            if (gameData != null)
            {
                Debug.Log("GameData loaded successfully.");
                Debug.Log("PlayerInventory count: " + (gameData.PlayerInventory != null ? gameData.PlayerInventory.Count.ToString() : "null"));

                foreach (var item in gameData.PlayerInventory)
                {
                    if (item != null && item.Card != null)
                    {
                        Debug.Log($"Loaded card: {item.Card.CardName}, Quantity: {item.Quantity}");
                    }
                    else
                    {
                        Debug.LogError("Item or Card is null in PlayerInventory.");
                    }
                }
            }
            else
            {
                Debug.LogError("GameData is null after loading JSON.");
            }
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + dataPath);
            gameData = new GameData();
        }
    }

    private void SaveGameData()
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(dataPath, json);
    }

    public void AddCardToPlayerInventory(string cardName, int quantity)
    {
        var card = gameData.Cards.Find(c => c.CardName == cardName);
        if (card != null)
        {
            gameData.PlayerInventory.Add(new InventoryItem(card, quantity));
            SaveGameData();
        }
    }

    public void RemoveCardFromPlayerInventory(string cardName)
    {
        gameData.PlayerInventory.RemoveAll(item => item.Card.CardName == cardName);
        SaveGameData();
    }

    public void AddCardToDatabase(string cardName, int hp, int power, int manaPrice, string imageUrl)
    {
        gameData.Cards.Add(new Card(cardName, hp, power, manaPrice, imageUrl));
        SaveGameData();
    }

    public void RemoveCardFromDatabase(string cardName)
    {
        gameData.Cards.RemoveAll(card => card.CardName == cardName);
        SaveGameData();
    }

    public void AddCardToShopInventory(string cardName, int price, int amount)
    {
        var card = gameData.Cards.Find(c => c.CardName == cardName);
        if (card != null)
        {
            gameData.ShopInventory.Add(new ShopItem(card, price, amount));
            SaveGameData();
        }
    }

    public void TransferCardFromShopToPlayer(string cardName)
    {
        var shopItem = gameData.ShopInventory.Find(item => item.Card.CardName == cardName);
        if (shopItem != null)
        {
            AddCardToPlayerInventory(cardName, 1);
            gameData.ShopInventory.Remove(shopItem);
            SaveGameData();
        }
    }

    public void GetCardDetailsByName(string cardName)
    {
        var card = gameData.Cards.Find(c => c.CardName == cardName);
        if (card != null)
        {
            Debug.Log($"Name: {card.CardName}, HP: {card.HP}, Power: {card.Power}, Mana Price: {card.ManaPrice}, Image URL: {card.ImageUrl}");
        }
    }

    public void PrintAllPlayerCards()
    {
        if (gameData == null)
        {
            Debug.LogError("GameData is null. Make sure the JSON file is loaded correctly.");
            return;
        }

        if (gameData.PlayerInventory == null || gameData.PlayerInventory.Count == 0)
        {
            Debug.LogWarning("PlayerInventory is empty or null.");
            return;
        }

        Debug.Log("Printing all player cards...");
        foreach (var item in gameData.PlayerInventory)
        {
            if (item == null)
            {
                Debug.LogError("InventoryItem is null.");
                continue;
            }

            var card = item.Card;
            if (card == null)
            {
                Debug.LogError("Card is null in InventoryItem.");
                continue;
            }

            Debug.Log($"Name: {card.CardName}, HP: {card.HP}, Power: {card.Power}, Mana Price: {card.ManaPrice}, Quantity: {item.Quantity}, Image URL: {card.ImageUrl}");
        }
    }

    public void PrintAllShopCards()
    {
        foreach (var item in gameData.ShopInventory)
        {
            var card = item.Card;
            Debug.Log($"Name: {card.CardName}, HP: {card.HP}, Power: {card.Power}, Mana Price: {card.ManaPrice}, Price: {item.Price}, Amount: {item.Amount}, Image URL: {card.ImageUrl}");
        }
    }

    public List<Card> GetAllPlayerCards()
    {
        List<Card> playerCards = new List<Card>();

        foreach (var item in gameData.PlayerInventory)
        {
            playerCards.Add(item.Card);
        }

        return playerCards;
    }

    public List<Card> GetAllShopCards()
    {
        List<Card> shopCards = new List<Card>();

        foreach (var item in gameData.ShopInventory)
        {
            shopCards.Add(item.Card);
        }

        return shopCards;
    }

    public List<Card> GetAllCards()
    {
        return gameData.Cards;
    }
}
