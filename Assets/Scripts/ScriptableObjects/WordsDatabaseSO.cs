using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Word Database", menuName = "Level/WordDatabase")]
public class WordsDatabaseSO : ScriptableObject
{
    public List<LevelWordsDataSO> wordsData;

    public LevelWordsDataSO GetWordsDataByType(WordDataType wordType)
    {
        if (wordsData == null || wordsData.Count == 0)
            return null;

        foreach(var wordData in wordsData)
        {
            if (wordData.wordDataType == wordType)
                return wordData;
        }

        return null;
    }
}
