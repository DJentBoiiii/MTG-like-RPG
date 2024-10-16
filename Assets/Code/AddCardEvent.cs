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
        m.TransferCardFromShopToPlayer(1);
        Debug.Log("Added card");
        connection.Close();
    }
}
