using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [Header("Reference Settings")]
    [SerializeField] private LevelDatabaseSO levelDatabaseSO;

    private LevelSettingsSO currentLoadedLevel;

    public LevelDatabaseSO LevelDatabases => levelDatabaseSO;

    private void Awake()
    {
        MessageSender<MessagesUi>.Subscribe(MessagesUi.PlayButtonClick, OnPlayButtonClick);
    }

    private void OnDestroy()
    {
        MessageSender<MessagesUi>.Unsubscribe(MessagesUi.PlayButtonClick, OnPlayButtonClick);
    }

    private void Start()
    {
        if (LevelPersistent.LastLevelPlayed != string.Empty)
            currentLoadedLevel = levelDatabaseSO.GetLevelSettingsByID(LevelPersistent.LastLevelPlayed);
    }

    public void LoadSceneAndChangeInformation()
    {
        //TO DO - add loading scene
        SceneManager.LoadSceneAsync("Level", LoadSceneMode.Single).completed += OnSceneLoaded;
    }

    public void TryLoadLevel(string levelId)
    {
        currentLoadedLevel = levelDatabaseSO.GetLevelSettingsByID(levelId);

        if (currentLoadedLevel == null)
            return;

        LoadSceneAndChangeInformation();
    }

    public void TryLoadNextLevel()
    {
        if (currentLoadedLevel == null)
            return;

        int index = levelDatabaseSO.levels.IndexOf(currentLoadedLevel);

        if (index == -1 || index > levelDatabaseSO.levels.Count - 1)
        {
            return;
        }

        currentLoadedLevel = levelDatabaseSO.levels[index + 1];

        LoadSceneAndChangeInformation();
    }


    private void OnPlayButtonClick(object obj)
    {
        if (currentLoadedLevel == null)
            currentLoadedLevel = levelDatabaseSO.levels.FirstOrDefault();

        if (currentLoadedLevel == null)
        {
            Debug.Log("No level found");
            return;
        }

        LoadSceneAndChangeInformation();
    }

    private void OnSceneLoaded(AsyncOperation asyncOp)
    {
        LevelSceneController levelSceneController = FindObjectOfType<LevelSceneController>();

        if (levelSceneController == null)
            return;

        levelSceneController.Initialize(currentLoadedLevel);
    }
}
