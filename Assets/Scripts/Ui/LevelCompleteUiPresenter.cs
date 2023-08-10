using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUiPresenter : BasePopUpPanelUi
{
    [Header("Reference Settings")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button nextLevelButton;

    protected override void Awake()
    {
        base.Awake();

        mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
    }

    private void OnMainMenuButtonClick()
    {
        MessageSender<MessagesUi>.Send(MessagesUi.MainMenuButtonClick);
    }

    private void OnNextLevelButtonClick()
    {
        MessageSender<MessagesUi>.Send(MessagesUi.NextLevelButtonClick);
    }
}
