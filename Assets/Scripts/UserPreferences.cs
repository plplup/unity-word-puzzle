using System;
using UnityEngine;

public static class UserPreferences
{
    public class IntPreference
    {
        private readonly string PrefKey;
        private int currentValue;
        public Action<int> ValueChanged;

        public int CurrentValue
        {
            get => currentValue;
            set
            {
                if (currentValue != value)
                {
                    currentValue = value;
                    PlayerPrefs.SetInt(PrefKey, currentValue);
                    PlayerPrefs.Save();
                    ValueChanged?.Invoke(currentValue);
                }
            }
        }

        public IntPreference(string prefKey, int defaultValue)
        {
            PrefKey = prefKey;
            currentValue = PlayerPrefs.GetInt(prefKey, defaultValue);
        }
    }

    public static IntPreference LanguageOption;

    public static void Initialize()
    {
        LanguageOption = new IntPreference(nameof(LanguageOption), 1);
    }
}