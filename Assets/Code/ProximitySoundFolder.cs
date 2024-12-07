using UnityEngine;

public class AudioProximityController : MonoBehaviour
{
    public AudioSource audioSource; // Посилання на джерело звуку
    public Transform player; // Посилання на гравця
    public float maxDistance = 10f; // Максимальна відстань, на якій звук чутно
    public float minVolume = 0.2f; // Мінімальна гучність
    public float maxVolume = 1.0f; // Максимальна гучність

    private void Start()
    {
        if (audioSource == null || player == null)
        {
            Debug.LogError("Будь ласка, призначте аудіоджерело та гравця в інспекторі.");
            return;
        }

        // Увімкнути відтворення аудіо, якщо воно ще не грає
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (audioSource == null || player == null)
            return;

        // Обчислити відстань між гравцем і джерелом звуку
        float distance = Vector3.Distance(player.position, transform.position);

        // Обчислити нову гучність
        float volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);
        volume = Mathf.Clamp(volume, minVolume, maxVolume);

        // Застосувати гучність до аудіоджерела
        audioSource.volume = volume;
    }
}
