using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProximitySoundController : MonoBehaviour
{
    public Transform target; // Посилання на цільовий об'єкт, що наближається
    public AudioSource audioSource; // Посилання на AudioSource
    public Text messageText; // Посилання на текстовий компонент для відображення повідомлення
    public float maxDistance = 20f; // Максимальна відстань до об'єкта, на якій звук буде максимально гучний
    public float minDistance = 2f; // Мінімальна відстань до об'єкта, на якій звук починає відтворюватися

    private bool isPlaying = false;

    void Update()
    {
        // Визначаємо відстань між цільовим об'єктом та нашим об'єктом
        float distance = Vector3.Distance(target.position, transform.position);

        // Якщо цільовий об'єкт у межах чутності, регулюємо гучність звуку
        if (distance <= maxDistance)
        {
            float volume = Mathf.Clamp01((maxDistance - distance) / (maxDistance - minDistance));
            audioSource.volume = volume;

            // Якщо звук ще не відтворюється, починаємо відтворення
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }
        }
        else
        {
            // Якщо цільовий об'єкт вийшов за межі, поступово зменшуємо гучність до 0
            if (audioSource.volume > 0)
            {
                audioSource.volume = Mathf.Max(audioSource.volume - Time.deltaTime, 0);
            }
            else if (audioSource.isPlaying)
            {
                audioSource.Stop();
                isPlaying = false;
            }
        }

        // Якщо звук завершив відтворення, відображаємо повідомлення
        if (!audioSource.isPlaying && isPlaying)
        {
            StartCoroutine(DisplayMessage());
            isPlaying = false;
        }
    }

    IEnumerator DisplayMessage()
    {
        messageText.text = "Щось ми збились, хлопи. Давайте відпочинемо";
        yield return new WaitForSeconds(3);
        messageText.text = "";
    }
}