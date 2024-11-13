using UnityEngine;

public class Creature : MonoBehaviour
{
    public int maxHP = 100;
    public int hp;
    public int mana;
    public int attack;

    private void Start()
    {
        hp = maxHP; 
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
        }
        Debug.Log("Player HP: " + hp);
    }

    public float GetHealthPercentage()
    {
        return (float)hp / maxHP;
    }
}
