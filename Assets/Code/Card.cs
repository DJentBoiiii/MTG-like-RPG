public class Card
{
    public string cardName;
    public int health;
    public int damage;

    public Card(string name, int hp, int dmg)
    {
        cardName = name;
        health = hp;
        damage = dmg;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
