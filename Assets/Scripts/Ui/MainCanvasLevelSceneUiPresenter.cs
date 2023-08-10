using System;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasLevelSceneUiPresenter : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button selectLevelButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        selectLevelButton.onClick.AddListener(OnSelectLevelButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
        optionsButton.onClick.AddListener(OnOptionsButtonClick);
    }

    private void OnOptionsButtonClick()
    {
        MessageSender<MessagesUi>.Send(MessagesUi.OptionsButtonClick);
    }

    private void OnPlayButtonClick()
    {
        MessageSender<MessagesUi>.Send(MessagesUi.PlayButtonClick);
    }

    private void OnSelectLevelButtonClick()
    {
        MessageSender<MessagesUi>.Send(MessagesUi.SelectLevelButtonClick);
    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
