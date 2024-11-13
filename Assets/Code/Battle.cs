using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    public Creature player;         // Посилання на об'єкт гравця
    public Image healthBar;       // Зображення типу "Filled"
    public Button damageButton;   // Кнопка для зниження HP
    public int damageAmount = 10; // Кількість урону при натисканні

    private void Start()
    {
        // Перевірка наявності всіх необхідних посилань
        if (player == null || healthBar == null || damageButton == null)
        {
            Debug.LogError("Не всі посилання встановлені у HealthUI!");
            return;
        }

        // Прив'язка методу для зниження HP до кнопки
        damageButton.onClick.AddListener(OnDamageButtonClicked);
        UpdateHealthBar();
    }

    private void OnDamageButtonClicked()
    {
        player.TakeDamage(damageAmount);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Оновлення `fillAmount` відповідно до відсотка HP гравця
        healthBar.fillAmount = player.GetHealthPercentage();
    }
}
