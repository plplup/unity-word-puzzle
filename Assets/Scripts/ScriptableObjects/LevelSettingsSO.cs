using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Settings", menuName = "Level/LevelSettings")]
public class LevelSettingsSO : BaseScriptableObject
{
    [Header("General settings")]
    public string levelName;
    public int shelvesAmount;
    public int wordsAmount;
    public float cameraSize;
    public float shelfXOffset;
    public Color skyBoxColor;
    public WordDataType wordType;
    [Tooltip("Seed to control word randomness")]
    public int randomSeed;
}
