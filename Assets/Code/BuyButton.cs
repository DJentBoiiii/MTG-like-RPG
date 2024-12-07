using UnityEngine;
using UnityEngine.UI;

public class ChangeTextOnButtonPress : MonoBehaviour
{
    public Text targetText; 
    public Button button;   

    private void Start()
    {
        if (targetText == null || button == null)
        {
            Debug.LogError("Не всі посилання встановлені!");
            return;
        }

       
        button.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        
        targetText.text = "Sold";
    }
}
