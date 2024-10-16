using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class CardManager : MonoBehaviour
{
      private string connectionString;

      void Awake()
      {
            connectionString = "URI=file:" + Application.dataPath + "/Cards.db";
            Debug.Log("Trying to connect");
            using var connection = new SqliteConnection(connectionString);
            try
            {
                  connection.Open();
                  Debug.Log("Connection successful");

                  using (var command = connection.CreateCommand())
                  {
                        command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Player_Inventory (
                        card_id INTEGER,
                        quantity INTEGER
                    );
                    CREATE TABLE IF NOT EXISTS Cards (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        card_name TEXT,
                        hp INTEGER,
                        power INTEGER,
                        mana_price INTEGER,
                        image_url TEXT
                    );
                    CREATE TABLE IF NOT EXISTS Shop_Inventory (
                        card_id INTEGER,
                        card_name TEXT,
                        price INTEGER,
                        amount INTEGER
                    )";
                        command.ExecuteNonQuery();
                  }

                  connection.Close();
            }
            catch (Exception ex)
            {
                  Debug.LogError("Error: " + ex.Message);
            }
      }

      public void AddCardToPlayerInventory(int playerId, int cardId, string itemName, int quantity)
      {
            using (var connection = new SqliteConnection(connectionString))
            {
                  connection.Open();
                  using (var command = connection.CreateCommand())
                  {
                        command.CommandText = "INSERT INTO Player_Inventory (player_id, cards_id, item_name, quantity) VALUES (@player_id, @cards_id, @item_name, @quantity)";
                        command.Parameters.AddWithValue("@player_id", playerId);
                        command.Parameters.AddWithValue("@cards_id", cardId);
                        command.Parameters.AddWithValue("@item_name", itemName);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.ExecuteNonQuery();
                  }
                  connection.Close();
            }
      }

      public void RemoveCardFromPlayerInventory(int playerId, int cardId)
      {
            using (var connection = new SqliteConnection(connectionString))
            {
                  connection.Open();
                  using (var command = connection.CreateCommand())
                  {
                        command.CommandText = "DELETE FROM Player_Inventory WHERE player_id = @player_id AND cards_id = @cards_id";
                        command.Parameters.AddWithValue("@player_id", playerId);
                        command.Parameters.AddWithValue("@cards_id", cardId);
                        command.ExecuteNonQuery();
                  }
                  connection.Close();
            }
      }


      public void AddCardToDatabase(string cardName, int hp, int power, int manaPrice, string imageUrl)
      {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                  command.CommandText = "INSERT INTO Cards (card_name, hp, power, mana_price, image_url) VALUES (@card_name, @hp, @power, @mana_price, @image_url)";

                  command.Parameters.AddWithValue("@card_name", cardName);
                  command.Parameters.AddWithValue("@hp", hp);
                  command.Parameters.AddWithValue("@power", power);
                  command.Parameters.AddWithValue("@mana_price", manaPrice);
                  command.Parameters.AddWithValue("@image_url", imageUrl);
                  command.ExecuteNonQuery();
            }
            connection.Close();
      }

      public void RemoveCardFromDatabase(string name)
      {
            using (var connection = new SqliteConnection(connectionString))
            {
                  connection.Open();
                  using (var command = connection.CreateCommand())
                  {
                        command.CommandText = "DELETE FROM Cards WHERE card_name = @name";
                        command.Parameters.AddWithValue("@name", name);
                        command.ExecuteNonQuery();
                  }
                  connection.Close();
            }
      }


      public void AddCardToShopInventory(int cardId, string cardName, int price, int amount)
      {
            using (var connection = new SqliteConnection(connectionString))
            {
                  connection.Open();
                  using (var command = connection.CreateCommand())
                  {
                        command.CommandText = "INSERT INTO Shop_Inventory (card_id, card_name, price, amount) VALUES (@card_id, @card_name, @price, @amount)";
                        command.Parameters.AddWithValue("@card_id", cardId);
                        command.Parameters.AddWithValue("@card_name", cardName);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@amount", amount);
                        command.ExecuteNonQuery();
                  }
                  connection.Close();
            }
      }

      public void TransferCardFromShopToPlayer(int cardId)
      {
            using (var connection = new SqliteConnection(connectionString))
            {
                  connection.Open();
                  using (var command = connection.CreateCommand())
                  {
                        command.CommandText = @"
            INSERT INTO Player_Inventory (cards_id)
            SELECT card_id FROM Shop_Inventory WHERE card_id = @card_id;
            DELETE FROM Shop_Inventory WHERE card_id = @card_id";
                        command.Parameters.AddWithValue("@card_id", cardId);
                        command.ExecuteNonQuery();
                  }
                  connection.Close();
            }
      }

      public void GetCardDetailsById(int cardId)
      {
            using (var connection = new SqliteConnection(connectionString))
            {
                  connection.Open();
                  using (var command = connection.CreateCommand())
                  {
                        command.CommandText = "SELECT * FROM Cards WHERE id = @card_id";
                        command.Parameters.AddWithValue("@card_id", cardId);
                        using (var reader = command.ExecuteReader())
                        {
                              while (reader.Read())
                              {
                                    Debug.Log($"ID: {reader["id"]}, Name: {reader["card_name"]}, HP: {reader["hp"]}, Power: {reader["power"]}, Mana Price: {reader["mana_price"]}, Description: {reader["description"]}, Image URL: {reader["image_url"]}");
                              }
                        }
                  }
                  connection.Close();
            }
      }

}
