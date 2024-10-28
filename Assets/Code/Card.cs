using UnityEngine.Timeline;

public class Card
{
    public string CardName { get; set; }
    public int HP { get; set; }
    public int Power { get; set; }
    public int ManaPrice { get; set; }
    public string ImageUrl { get; set; }
    public int Quantity { get; set; }

    public Card(string name, int hp, int dmg, int prc, string url)
    {
        CardName = name;
        HP = hp;
        Power = dmg;
        ManaPrice = prc;
        ImageUrl = url;
        

    }

    public void TakeDamage(int dmg)
    {
        HP -= dmg;
    }

    public bool IsDead()
    {
        return HP <= 0;
    }
}
