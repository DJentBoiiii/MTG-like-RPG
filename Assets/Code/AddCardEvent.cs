using UnityEngine;

public class AddCardEvent : MonoBehaviour
{
    void Start()
    {
        LoadAllPlayerCards();
    }

    public void LoadAllPlayerCards()
    { 
        CardManager cardManager = gameObject.AddComponent<CardManager>();
        cardManager.PrintAllPlayerCards();

        Debug.Log("All player cards have been loaded.");
    }
}
