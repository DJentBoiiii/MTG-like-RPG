using UnityEngine;
using UnityEngine.UI;

public class RewardDisplay : MonoBehaviour
{
    public Text rewardText; // Поле для відображення нагороди

    private void Start()
    {
        if (rewardText == null)
        {
            Debug.LogError("Поле для відображення нагороди не встановлено!");
            return;
        }

        // Отримання нагороди з PlayerPrefs
        int experienceReward = PlayerPrefs.GetInt("ExperienceReward", 0);

        // Оновлення тексту нагороди
        rewardText.text = $"+ {experienceReward} experience points!";
    }
}
