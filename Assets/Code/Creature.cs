using UnityEngine;

public class Creature : MonoBehaviour
{
    public int maxHP = 100;
    public int hp;

    private void Start()
    {
        hp = maxHP; // Ініціалізація HP на початку
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
        }
    }

    public float GetHealthPercentage()
    {
        return (float)hp / maxHP; // Повертаємо відсоток здоров'я
    }
}
