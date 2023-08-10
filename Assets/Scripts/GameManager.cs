using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;
using UnityEngine.Localization.Tables;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Reference Settings")]
    [SerializeField] private LevelController levelController;

    public LevelController LevelController => levelController;
    public WordsDatabaseSO LocalizedWordDatabaseSO { get; private set; }
    public AssetTable LocalizedAssetTable { get; private set; }
    public RuntimePlatform Platform { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Platform = Application.platform;

        LevelPersistent.Init();

        UserPreferences.Initialize();

        StartCoroutine(LoadAssetTable());

        LocalizationSettings.SelectedLocaleChanged += SelectedLocaleChanged;

        if (Platform == RuntimePlatform.Android || Platform == RuntimePlatform.IPhonePlayer)
            Application.targetFrameRate = 30;
    }

    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= SelectedLocaleChanged;
    }

    private IEnumerator LoadAssetTable()
    {
        var loadingOperation = LocalizationSettings.AssetDatabase.GetTableAsync("DefaultAssetTable");
        yield return loadingOperation;

        if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
        {
            LocalizedAssetTable = loadingOperation.Result;

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[UserPreferences.LanguageOption.CurrentValue];

            StartCoroutine(LoadAssetCoroutine());
        }
    }

    private void SelectedLocaleChanged(Locale obj)
    {
        StartCoroutine(LoadAssetCoroutine());
    }
    
    private IEnumerator LoadAssetCoroutine()
    {  
        var operation = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<WordsDatabaseSO>(LocalizedAssetTable.TableCollectionName, Constants.WordDatabaseKey);

        yield return operation;

        LocalizedWordDatabaseSO = operation.Result;
    }

}
