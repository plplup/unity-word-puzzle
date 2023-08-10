using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneController : MonoBehaviour
{
    [Header("Reference Settings")]
    [SerializeField] private LevelSettingsSO levelSettingsSO;
    [SerializeField] private ShelvesController shelvesController;
    [SerializeField] private InterfaceController interfaceController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private ReflectionProbe reflectionProbe;
    [Header("Test Variables")]
    [SerializeField] private bool isTestingScene = false;

    private List<string> selectedWords;

    private void Awake()
    {
        MessageSender<MessagesUi>.Subscribe(MessagesUi.ReplayButtonClick, OnReplayButtonClick);
        MessageSender<MessagesUi>.Subscribe(MessagesUi.ReturnButtonClick, OnReturnButtonClick);
        MessageSender<MessagesUi>.Subscribe(MessagesUi.MainMenuButtonClick, OnReturnButtonClick);
        MessageSender<MessagesUi>.Subscribe(MessagesUi.NextLevelButtonClick, OnNextLevelButtonClick);
        MessageSender<Messages>.Subscribe(Messages.ShelvesChanged, OnShelvesChanged);
    }

    private void OnDestroy()
    {
        MessageSender<MessagesUi>.Unsubscribe(MessagesUi.ReplayButtonClick, OnReplayButtonClick);
        MessageSender<MessagesUi>.Unsubscribe(MessagesUi.ReturnButtonClick, OnReturnButtonClick);
        MessageSender<MessagesUi>.Unsubscribe(MessagesUi.MainMenuButtonClick, OnReturnButtonClick);
        MessageSender<MessagesUi>.Unsubscribe(MessagesUi.NextLevelButtonClick, OnNextLevelButtonClick);
        MessageSender<Messages>.Unsubscribe(Messages.ShelvesChanged, OnShelvesChanged);
    }

    private void Start()
    {
        if (isTestingScene == true)
            InitializeLevel();
    }

    public void Initialize(LevelSettingsSO levelSettingsSO)
    {
        if (isTestingScene == true)
        {
            Debug.LogError("Testing scene is enable, disable it before playing from main menu");
            return;
        }

        LevelPersistent.SetLastLevelPlayed(levelSettingsSO.Id);

        this.levelSettingsSO = levelSettingsSO;

        UpdateLevelVisual(levelSettingsSO.skyBoxColor);

        InitializeLevel();
    }

    private void InitializeLevel()
    {
        var wordsData = GameManager.Instance.LocalizedWordDatabaseSO.GetWordsDataByType(levelSettingsSO.wordType);

        if (wordsData == null)
        {
            Debug.LogError("Word Data SO not found");
            return;
        }

        selectedWords = wordsData.GetRandomWordsFromData(levelSettingsSO.wordsAmount, levelSettingsSO.randomSeed);

        if (selectedWords == null)
        {
            Debug.LogError("Words not found");
            return;
        }

        mainCamera.orthographicSize = levelSettingsSO.cameraSize;

        shelvesController.Initialize(levelSettingsSO.shelvesAmount, selectedWords, levelSettingsSO.wordType,
            Utils.GetHorizontalExtensionFromViewBounds(mainCamera), levelSettingsSO.shelfXOffset);

        interfaceController.Initialize(selectedWords);
     
    }

    private void UpdateLevelVisual(Color skyBoxColor)
    {
        Material skyboxMaterial = RenderSettings.skybox;

        skyboxMaterial.SetColor("_Color2", skyBoxColor);

        reflectionProbe.RenderProbe();
    }

    private void OnReplayButtonClick(object obj)
    {
        shelvesController.ResetShelves();
        shelvesController.ArrangeLetterCubes(true);
    }

    private void OnReturnButtonClick(object obj)
    {
        SceneManager.LoadScene("MainScene");
    }

    private void OnShelvesChanged(object words)
    {
        if (words == null || (words is List<string>) == false)
            return;

        interfaceController.ResetBoardTexts();

        int corretWordsAmount = 0;
        foreach (var word in (List<string>) words)
        {
            if (selectedWords.Contains(word))
            {
                interfaceController.UpdateWord(word);
                corretWordsAmount++;
            }
        }

        if (corretWordsAmount == selectedWords.Count)
        {
            LevelPersistent.AddLevelCompleted(levelSettingsSO.Id);

            interfaceController.ShowLevelCompletePresenter();
        }
    }

    private void OnNextLevelButtonClick(object obj)
    {
        GameManager.Instance.LevelController.TryLoadNextLevel();
    }
}
