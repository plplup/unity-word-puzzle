using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLevelController : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private SelectLevelUiPresenter selectLevelUiPresenter;    

    private void Awake()
    {
        MessageSender<MessagesUi>.Subscribe(MessagesUi.SelectLevelButtonClick, OnSelectLevelButtonClick);
        MessageSender<MessagesUi>.Subscribe(MessagesUi.LevelSelectButtonClick, OnLevelSelectButtonClick);

        selectLevelUiPresenter.InitializeLevels(levelController.LevelDatabases);
    }

    private void OnDestroy()
    {
        MessageSender<MessagesUi>.Unsubscribe(MessagesUi.SelectLevelButtonClick, OnSelectLevelButtonClick);
        MessageSender<MessagesUi>.Unsubscribe(MessagesUi.LevelSelectButtonClick, OnLevelSelectButtonClick);
    }

    private void OnSelectLevelButtonClick(object obj)
    {
        selectLevelUiPresenter.Initialize();
    }

    private void OnLevelSelectButtonClick(object levelId)
    {
        if (levelId is string == false)
            return;

        if (string.IsNullOrEmpty((levelId as string)) == true)
            return;

        levelController.TryLoadLevel((string)levelId);
    }


}
