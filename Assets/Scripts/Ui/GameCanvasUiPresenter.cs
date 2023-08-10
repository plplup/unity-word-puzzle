using UnityEngine;
using UnityEngine.UI;

public class GameCanvasUiPresenter : MonoBehaviour
{
    [Header("Reference Settings")]
    [SerializeField] private Button replayButton;
    [SerializeField] private Button returnButton;    

    private void Awake()
    {
        replayButton.onClick.AddListener(OnReplayButtonClick);
        returnButton.onClick.AddListener(OnReturnButtonClick);
    }

    private void OnReturnButtonClick()
    {
        MessageSender<MessagesUi>.Send(MessagesUi.ReturnButtonClick);
    }

    private void OnReplayButtonClick()
    {
        MessageSender<MessagesUi>.Send(MessagesUi.ReplayButtonClick);
    }

}
