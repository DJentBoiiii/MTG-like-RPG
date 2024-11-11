using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.IO;
using System;

public class CardDisplayManager : MonoBehaviour
{
    public List<Card> cards; // Список карток
    public GameObject buttonPrefab; // Префаб для кнопок із зображеннями
    public Transform contentParent; // Батьківський об'єкт для кнопок (Horizontal Layout Group)
    public InputField searchInput; // Поле вводу для запиту
    public Dropdown sortDropdown; // Dropdown для вибору сортування
    int balance = 200;

    void Start()
    {
        contentParent = GameObject.Find("ButtonContent").transform; // Впевніться, що об'єкт "Content" існує в ієрархії
        searchInput.onValueChanged.AddListener(delegate { FilterCards(); });
        sortDropdown.onValueChanged.AddListener(delegate { SortCards(); });
        DisplayCards();
    }

    void DisplayCards()
    {
        ClearExistingButtons();
        string dbPath = Path.Combine(Application.streamingAssetsPath, "Cards.db");
        Debug.Log("Trying to connect to database at " + dbPath);
        using var connection = new SqliteConnection("URI=file:" + dbPath);
        connection.Open();
        CardManager m = gameObject.AddComponent(typeof(CardManager)) as CardManager;
        cards = m.GetAllPlayerCards();
        Debug.Log("Loaded cards from database");
        connection.Close();
        foreach (Card card in cards)
        {
            GameObject newButtonObject = Instantiate(buttonPrefab, contentParent);
            Text textComponent = newButtonObject.GetComponentInChildren<Text>();
            textComponent.text = card.CardName;
            Button button = newButtonObject.GetComponent<Button>();
            if (balance >= 100)
            {
                button.onClick.AddListener(() =>
                {
                    RemoveCardButton(newButtonObject);
                    balance -= 100;
                    Debug.Log("New balance: " + balance);

                    // Зробити всі кнопки недоступними, якщо баланс менше 100
                    if (balance < 100)
                    {
                        SetAllButtonsInteractable(false);
                    }
                });
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    void ClearExistingButtons()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
    }

    void FilterCards()
    {
        string query = searchInput.text.ToLower();
        List<Card> filteredCards = cards.FindAll(card => card.CardName.ToLower().Contains(query));
        ClearExistingButtons();
        foreach (Card card in filteredCards)
        {
            GameObject newButtonObject = Instantiate(buttonPrefab, contentParent);
            Text textComponent = newButtonObject.GetComponentInChildren<Text>();
            textComponent.text = card.CardName;
            Button button = newButtonObject.GetComponent<Button>();
            if (balance >= 100)
            {
                button.onClick.AddListener(() =>
                {
                    RemoveCardButton(newButtonObject);
                    balance -= 100;
                    Debug.Log("New balance: " + balance);

                    // Зробити всі кнопки недоступними, якщо баланс менше 100
                    if (balance < 100)
                    {
                        SetAllButtonsInteractable(false);
                    }
                });
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    void SortCards()
    {
        string criteria = sortDropdown.options[sortDropdown.value].text;
        if (criteria == "Power")
        {
            cards.Sort((card1, card2) => card2.Power.CompareTo(card1.Power));
        }
        else if (criteria == "Mana Price")
        {
            cards.Sort((card1, card2) => card2.ManaPrice.CompareTo(card1.ManaPrice));
        }
        else if (criteria == "HP")
        {
            cards.Sort((card1, card2) => card2.HP.CompareTo(card1.HP));
        }
        FilterCards();
    }

    void RemoveCardButton(GameObject buttonObject)
    {
        Destroy(buttonObject);
    }

    void SetAllButtonsInteractable(bool interactable)
    {
        foreach (Transform child in contentParent)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = interactable;
            }
        }
    }
}
