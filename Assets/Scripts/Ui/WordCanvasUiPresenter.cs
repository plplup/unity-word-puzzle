using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WordCanvasUiPresenter : MonoBehaviour
{
    [Header("Reference Settings")]
    [SerializeField] private Transform wordBoardTransform;
    [SerializeField] private TMP_Text wordTextPrefab;
    [Header("Detail Settings")]
    [SerializeField] private Color wordCompleteColor;
    [SerializeField] private Color wordDefaultColor;

    private List<TMP_Text> wordTexts;

    private void Awake()
    {
        wordTexts = new List<TMP_Text>();
    }

    public void SpawnText(string word)
    {
        var newText = Instantiate(wordTextPrefab, wordBoardTransform);

        newText.SetText(word);

        wordTexts.Add(newText);  // Add the spawned text to the list for tracking
    }

    public void ClearBoard()
    {
        foreach (var wordText in wordTexts)
        {
            wordText.color = wordDefaultColor;
            wordText.fontStyle = FontStyles.Normal;
        }
    }

    public void FindTextAndUpdate(string word)
    {
        foreach (var wordText in wordTexts)
        {
            if (string.Equals(wordText.text, word))
            {
                wordText.color = wordCompleteColor;
                wordText.fontStyle = FontStyles.Underline;
                break;
            }

        }
    }
}
