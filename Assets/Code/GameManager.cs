using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Card> playerDeck = new List<Card>();
    public List<Card> enemyDeck = new List<Card>();

    public Card selectedPlayerCard;
    public Card enemyCard;


    [SerializeField] private Image playerCardImage;
    [SerializeField] private Image enemyCardImage;
    [SerializeField] private Text playerHealthText;
    [SerializeField] private Text enemyHealthText;

    void Start()
    {

        playerDeck.Add(new Card("Knight", 10, 3, 5, "VadymGay"));
        playerDeck.Add(new Card("Dragon", 12, 5, 5, "VadymGay"));
        enemyDeck.Add(new Card("Goblin", 8, 2, 5, "VadymGay"));
        enemyDeck.Add(new Card("Orc", 14, 4, 5, "VadymGay"));

        selectedPlayerCard = GetRandomCard(playerDeck);
        enemyCard = GetRandomCard(enemyDeck);


        UpdateUI();
    }


    Card GetRandomCard(List<Card> deck)
    {
        return deck[Random.Range(0, deck.Count)];
    }


    void UpdateUI()
    {
        playerHealthText.text = "Health: " + selectedPlayerCard.HP;
        enemyHealthText.text = "Health: " + enemyCard.HP;
    }


    public void EndPlayerTurn()
    {

        enemyCard = GetRandomCard(enemyDeck);


        selectedPlayerCard.TakeDamage(enemyCard.Power);
        enemyCard.TakeDamage(selectedPlayerCard.Power);


        UpdateUI();


        if (selectedPlayerCard.IsDead())
        {
            Debug.Log(selectedPlayerCard.CardName + " is dead!");
        }
        if (enemyCard.IsDead())
        {
            Debug.Log(enemyCard.CardName + " is dead!");
        }


        if (playerDeck.Count > 0)
        {
            selectedPlayerCard = GetRandomCard(playerDeck);
            UpdateUI();
        }
    }
}
