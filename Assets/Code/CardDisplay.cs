using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Canvas canvas;  // Призначте полотно в інспекторі
    public Sprite cardSprite;  // Зображення картки, яке можна також призначити в інспекторі або завантажувати з ресурсів

    void Start()
    {
        // Приклад виклику функції для створення нового зображення на полотні
        CreateCardImage();
    }

    private void CreateCardImage()
    {
        // Створюємо новий об'єкт Image
        GameObject cardImageObject = new GameObject("CardImage", typeof(RectTransform), typeof(Image));
        
        // Робимо об'єкт дочірнім до полотна
        cardImageObject.transform.SetParent(canvas.transform);

        // Налаштовуємо RectTransform для правильного масштабування та позиціонування
        RectTransform rectTransform = cardImageObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(100, 150);  // Розмір зображення картки
        rectTransform.anchoredPosition = Vector2.zero;  // Позиція на полотні

        // Додаємо компонент Image і присвоюємо спрайт
        Image cardImage = cardImageObject.GetComponent<Image>();
         cardImage.sprite = Resources.Load<Sprite>("Assets/CrusaderCard.png");
    }
}
