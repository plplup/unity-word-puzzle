using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LevelPersistent : MonoBehaviour
{
    private static string lastLevelPlayed;
    private static HashSet<string> levelsCompleted;

    public static string LastLevelPlayed => lastLevelPlayed;
    public static HashSet<string> LevelsCompleted => levelsCompleted;

    public static void AddLevelCompleted(string levelId)
    {
        if (levelsCompleted.Add(levelId))
        {
            SerializedLevelsCompleted();
        }
    }

    public static void SetLastLevelPlayed(string levelId)
    {
        if (string.IsNullOrEmpty(levelId) == true)
            return;

        lastLevelPlayed = levelId;
        PlayerPrefs.SetString(Constants.LastLevelPlayed, levelId);
    }

    private static void SerializedLevelsCompleted()
    {
        var stringBuilder = new StringBuilder();
        var i = 0;
        foreach (var levels in levelsCompleted)
        {
            stringBuilder.Append(levels.ToString());
            if (i != levelsCompleted.Count - 1)
            {
                stringBuilder.Append(";");
            }
            i++;
        }

        PlayerPrefs.SetString(Constants.LevelKey, stringBuilder.ToString());
    }

    public static void Init()
    {
        levelsCompleted = PlayerPrefs.GetString(Constants.LevelKey, string.Empty).Split(';').ToHashSet();
        lastLevelPlayed = PlayerPrefs.GetString(Constants.LastLevelPlayed, string.Empty);
    }
}