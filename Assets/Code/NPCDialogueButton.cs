using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueButton : MonoBehaviour
{
    public string[] dialoguePhrases; // Масив фраз для діалогу
    public Text dialogueText; // Поле для тексту діалогу
    private int currentPhraseIndex = 0; // Індекс поточної фрази

    public void OnButtonClick()
    {
        if (dialoguePhrases.Length == 0) return;

        // Змінюємо текст на наступну фразу
        dialogueText.text = dialoguePhrases[currentPhraseIndex];

        // Змінюємо індекс на наступну фразу
        currentPhraseIndex = (currentPhraseIndex + 1) % dialoguePhrases.Length;
    }
}
