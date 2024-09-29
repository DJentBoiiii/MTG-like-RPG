using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;  // Публічна змінна для прив'язки AudioSource

    void Start()
    {
        // Отримуємо компонент кнопки та додаємо обробник натискання
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlaySound);  // Додаємо обробник події
        }
        else
        {
            Debug.LogError("Button component not found!");
        }
    }

    void PlaySound()
    {
        if (audioSource != null)
        {
            Debug.Log("Playing sound");  // Перевірка виведення в консоль
            audioSource.Play();  // Відтворення звуку
        }
        else
        {
            Debug.LogError("AudioSource not assigned!");
        }
    }
}
