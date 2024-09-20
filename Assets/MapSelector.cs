using UnityEngine;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{
    public Dropdown mapDropdown;  // Dropdown для вибору карти
    public Image displayImage;    // UI Image для відображення карти
    public Button showImageButton; // Кнопка для показу картинки

    public Sprite dragonSprite;   // Картинка для опції Dragon
    public Sprite anotherSprite;  // Картинка для іншої опції

    void Start()
    {
        if (showImageButton != null)
        {
            showImageButton.onClick.AddListener(ShowSelectedImage);
        }
        else
        {
            Debug.LogError("showImageButton не присвоєно в інспекторі!");
        }

        if (displayImage != null)
        {
            displayImage.gameObject.SetActive(false); // Сховати картинку при старті
        }
        else
        {
            Debug.LogError("displayImage не присвоєно в інспекторі!");
        }
    }

    public void ShowSelectedImage()
    {
        if (mapDropdown == null || displayImage == null)
        {
            return; // Якщо компоненти не ініціалізовані, виходимо з методу
        }

        // Отримуємо вибрану опцію
        string selectedMap = mapDropdown.options[mapDropdown.value].text;

        // Вибираємо відповідну картинку
        switch (selectedMap)
        {
            case "Dragon":
                displayImage.sprite = dragonSprite;
                break;
            case "Forest":
                displayImage.sprite = anotherSprite;
                break;
            case "Ocean":
                displayImage.sprite = anotherSprite;
                break;
            default:
                displayImage.sprite = null; 
                break;
        }

      
        displayImage.gameObject.SetActive(true);
    }
}
