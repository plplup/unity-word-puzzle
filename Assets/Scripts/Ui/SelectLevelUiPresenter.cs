using UnityEngine;

public class SelectLevelUiPresenter : BasePopUpPanelUi
{
    [Header("Reference settings")]
    [SerializeField] private Transform levelButtonParent;
    [SerializeField] private LevelButtonUi levelButtonPrefab;

    public void InitializeLevels(LevelDatabaseSO levelDataBaseSO)
    {
        foreach(var level in levelDataBaseSO.levels)
        {
            var newLevelButton = Instantiate(levelButtonPrefab, levelButtonParent);

            newLevelButton.titleText.text = level.levelName;
            newLevelButton.backGroundImage.color = level.skyBoxColor;
            newLevelButton.button.onClick.AddListener(() => OnLevelButtonClick(level.Id));
        }
    }

    private void OnLevelButtonClick(string levelId)
    {
        MessageSender<MessagesUi>.Send(MessagesUi.LevelSelectButtonClick, levelId);
    }
}
