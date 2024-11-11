using UnityEngine;
using Mono.Data.Sqlite;

public class AddCardEvent : MonoBehaviour
{
    void Start()
    {
        LoadAllCards();
    }

    private void LoadAllCards()
    {
        var connectionString = "URI=file:" + Application.dataPath + "/Cards.db";
        Debug.Log("Connecting to database...");
        
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        
        CardManager m = gameObject.AddComponent(typeof(CardManager)) as CardManager;
        m.PrintAllPlayerCards(1);  // Виклик методу для виведення всіх карток

        Debug.Log("All cards have been loaded.");
        connection.Close();
    }
}
