using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Database", menuName = "Level/LevelDatabase")]
public class LevelDatabaseSO : ScriptableObject
{
    public List<LevelSettingsSO> levels;

    public LevelSettingsSO GetLevelSettingsByID(string id)
    {
        if (levels == null || levels.Count == 0)
        {
            Debug.Log("Levels settings missing");
            return null;
        }

        foreach(var level in levels)
        {
            if (string.Equals(level.Id, id))
                return level;
        }

        return null;
    }
}
