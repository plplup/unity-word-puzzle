using System.Collections.Generic;
using UnityEngine;

public enum WordDataType
{
    None,
    FourLetter,
    FiveLetter,
    SixLetter
}


[CreateAssetMenu(fileName = "New Level Words Data", menuName = "Level/LevelWordsData")]
public class LevelWordsDataSO : ScriptableObject
{
    public WordDataType wordDataType;
    public List<string> words;

    public List<string> GetRandomWordsFromData(int amount, int randomSeed = 0)
    {
        if (amount > words.Count)
        {
            Debug.LogError("Amount is bigger than the dataset");
            return null;
        }

        if (randomSeed != 0)
        {
            Random.InitState(randomSeed);
        }

        int securityAttempts = 30;
        List<string> selectedElements = new List<string>();

        while (selectedElements.Count < amount && securityAttempts > 0)
        {
            int randomIndex = Random.Range(0, words.Count);

            if (selectedElements.Contains(words[randomIndex]) == false)
            {
                selectedElements.Add(words[randomIndex]);
            }

            securityAttempts--;
        }

        return selectedElements;
    }
}
