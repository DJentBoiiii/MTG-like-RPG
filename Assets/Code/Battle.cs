using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HealthUI : MonoBehaviour
{
    public Creature player;         // Посилання на об'єкт гравця
    public Creature enemy;          // Посилання на об'єкт ворога
    public Image playerHealthBar;   // Шкала здоров'я гравця
    public Image enemyHealthBar;    // Шкала здоров'я ворога
    public Button attackButton;     // Кнопка атаки
    public Button magicButton;      // Кнопка магії
    public int damageAmount = 10;   // Урон, що наносить кожна атака
    public int magicDamageAmount = 15; // Урон, що наносить магія
    public Animator thirdObjectAnimator; // Аніматор третього об'єкта
    public Transform thirdObject;   // Трансформ третього об'єкта (наприклад, в повітрі)
    public Animator playerAnimator; // Аніматор гравця
    public Animator enemyAnimator;  // Аніматор ворога

    private void Start()
    {
        // Перевірка наявності всіх необхідних посилань
        if (player == null || enemy == null || playerHealthBar == null || enemyHealthBar == null ||
            attackButton == null || magicButton == null || playerAnimator == null || enemyAnimator == null ||
            thirdObjectAnimator == null || thirdObject == null)
        {
            Debug.LogError("Не всі посилання встановлені у HealthUI!");
            return;
        }

        // Ініціалізація шкал здоров'я
        UpdateHealthBars();

        // Прив'язка методів до кнопок
        attackButton.onClick.AddListener(OnAttackButtonClicked);
        magicButton.onClick.AddListener(OnMagicButtonClicked);
    }

    private void OnAttackButtonClicked()
    {
        // Почати послідовність атаки гравця і ворога
        StartCoroutine(BattleSequence());
    }

    private void OnMagicButtonClicked()
    {
        // Почати магічну атаку
        StartCoroutine(MagicEffectSequence());
    }

    private IEnumerator BattleSequence()
    {
        // Гравець атакує
        yield return StartCoroutine(Attack(player, enemy));
        if (CheckBattleEnd()) yield break;

        // Ворог атакує
        yield return StartCoroutine(Attack(enemy, player));
        CheckBattleEnd();
    }

    private IEnumerator MagicEffectSequence()
    {
     // Запуск анімації на третьому об'єкті
    thirdObjectAnimator.SetTrigger("MagicEffect");
    yield return new WaitForSeconds(1f); // Зачекати для завершення анімації

    // Нанесення шкоди ворогу
    enemy.TakeDamage(magicDamageAmount);
    UpdateHealthBars();

    // Перевірка, чи закінчилася битва
    if (CheckBattleEnd()) yield break;

    // Ворог атакує
    yield return StartCoroutine(Attack(enemy, player));
    CheckBattleEnd();
    }

    private IEnumerator Attack(Creature attacker, Creature target)
    {
        // Запуск анімації атаки
        Animator attackerAnimator = (attacker == player) ? playerAnimator : enemyAnimator;
        attackerAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f); // Час для завершення анімації

        // Нанесення шкоди
        target.TakeDamage(damageAmount);
        UpdateHealthBars();
    }

    private void UpdateHealthBars()
    {
        // Оновлення шкал здоров'я
        playerHealthBar.fillAmount = player.GetHealthPercentage();
        enemyHealthBar.fillAmount = enemy.GetHealthPercentage();
    }

    private bool CheckBattleEnd()
    {
        if (player.hp <= 0 || enemy.hp <= 0)
        {
            // Визначення переможця
            bool isPlayerWinner = enemy.hp <= 0;

            // Збереження нагороди
            int experience = isPlayerWinner ? 10 : 5;
            PlayerPrefs.SetInt("ExperienceReward", experience);
            PlayerPrefs.Save();

            // Перехід на сцену з нагородою
            SceneManager.LoadScene("RewardScene");
            return true;
        }

        return false;
    }
}
