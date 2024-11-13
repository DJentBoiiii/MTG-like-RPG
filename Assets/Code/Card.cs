using System;
using UnityEngine;

[Serializable]
public class Card
{
    public string CardName;
    public int HP;
    public int Power;
    public int ManaPrice;
    public string ImageUrl;

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
