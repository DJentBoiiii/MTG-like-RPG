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
    public int damageAmount = 10;   // Урон, що наносить кожна атака
    public Animator playerAnimator; // Аніматор гравця
    public Animator enemyAnimator;  // Аніматор ворога
    public Transform playerCharacter; // Трансформ гравця
    public Transform enemyCharacter;  // Трансформ ворога

    private void Start()
    {
        // Перевірка наявності всіх необхідних посилань
        if (player == null || enemy == null || playerHealthBar == null || enemyHealthBar == null ||
            attackButton == null || playerAnimator == null || enemyAnimator == null ||
            playerCharacter == null || enemyCharacter == null)
        {
            Debug.LogError("Не всі посилання встановлені у HealthUI!");
            return;
        }

        // Ініціалізація шкал здоров'я
        UpdateHealthBars();

        // Прив'язка методу атаки до кнопки
        attackButton.onClick.AddListener(OnAttackButtonClicked);
    }

    private void OnAttackButtonClicked()
    {
        // Почати послідовність атаки гравця і ворога
        StartCoroutine(BattleSequence());
    }

    private IEnumerator BattleSequence()
    {
        // Гравець атакує
        yield return StartCoroutine(Attack(playerCharacter, enemyCharacter, playerAnimator, enemy));
        if (CheckBattleEnd()) yield break;

        // Ворог атакує
        yield return StartCoroutine(Attack(enemyCharacter, playerCharacter, enemyAnimator, player));
        CheckBattleEnd();
    }

    private IEnumerator Attack(Transform attacker, Transform target, Animator attackerAnimator, Creature targetCreature)
    {
        // Переміщення атакуючого персонажа вперед
        float moveDistance = 1f; // Відстань, на яку переміщується персонаж
        float moveSpeed = 2f;    // Швидкість переміщення
        Vector3 targetPosition = attacker.position + attacker.forward * moveDistance;

        while (Vector3.Distance(attacker.position, targetPosition) > 0.05f)
        {
            attacker.position = Vector3.MoveTowards(attacker.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Запуск анімації атаки
        attackerAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f); // Час для завершення анімації

        // Нанесення шкоди
        targetCreature.TakeDamage(damageAmount);
        UpdateHealthBars();

        // Повернення атакуючого на початкову позицію
        Vector3 initialPosition = attacker.position - attacker.forward * moveDistance;
        while (Vector3.Distance(attacker.position, initialPosition) > 0.05f)
        {
            attacker.position = Vector3.MoveTowards(attacker.position, initialPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
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
