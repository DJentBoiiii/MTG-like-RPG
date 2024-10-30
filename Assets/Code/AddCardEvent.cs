using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;


public class AddCardEvent : MonoBehaviour
{
    public void NewCard()
    {
        var connectionString = "URI=file:" + Application.dataPath + "/Cards.db";
        Debug.Log("Trying to connect");
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        CardManager m = gameObject.AddComponent(typeof(CardManager)) as CardManager;
        m.AddCardToDatabase("Orc", 13, 24, 3, "Assets\\Orc.png");
        m.AddCardToDatabase("SomeDude", 6, 456, 3, "Assets\\SomeDude.png");
        m.AddCardToDatabase("Wizard", 15, 69, 3, "Assets\\Wizard.png");
        m.AddCardToDatabase("Skeleton", 5, 2, 12, "Assets\\Skeleton.png");
        m.AddCardToPlayerInventory(2, 1);
        m.AddCardToPlayerInventory(3, 1);
        m.AddCardToPlayerInventory(4, 1);
        m.AddCardToPlayerInventory(5, 1);
        Debug.Log("Here you go, Nigger");
        connection.Close();
    }
}
