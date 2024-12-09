using UnityEngine;
using UnityEngine.UI;

public class TrainButton : MonoBehaviour
{
    public Text levelText;
    public Text hpText;
    public Text attackText;
    public Text defenseText;

    private int level;
    private int hp;
    private int attack;
    private int defense;

    void Start()
    {
      
        level = PlayerPrefs.GetInt("Level", 1);
        hp = PlayerPrefs.GetInt("HP", 100);
        attack = PlayerPrefs.GetInt("Attack", 10);
        defense = PlayerPrefs.GetInt("Defense", 5);

        UpdateUI();
    }

    public void OnTrainButtonPressed()
    {
       
        level++;
        hp += 10;
        attack += 5;
        defense += 3;

        
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("HP", hp);
        PlayerPrefs.SetInt("Attack", attack);
        PlayerPrefs.SetInt("Defense", defense);

        PlayerPrefs.Save();

        UpdateUI();
    }

    private void UpdateUI()
    {
        
        levelText.text = "Level: " + level;
        hpText.text = "HP: " + hp;
        attackText.text = "Attack: " + attack;
        defenseText.text = "Defense: " + defense;
    }
}
