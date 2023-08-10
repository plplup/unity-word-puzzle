using System;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    [Header("Reference Settings")]
    [SerializeField] private WordCanvasUiPresenter wordCanvasUiPresenter;
    [SerializeField] private LevelCompleteUiPresenter levelCompleteUiPresenter;

    private void Awake()
    {
        MessageSender<MessagesUi>.Subscribe(MessagesUi.ReplayButtonClick, OnReplayButtonClick);

    }

    private void OnDestroy()
    {
        MessageSender<MessagesUi>.Unsubscribe(MessagesUi.ReplayButtonClick, OnReplayButtonClick);
    }

    private void OnReplayButtonClick(object obj)
    {
        ResetBoardTexts();
    }

    public void Initialize(List<string> words)
    {
        foreach (var word in words)
        {
            wordCanvasUiPresenter.SpawnText(word);
        }
    }

    public void ResetBoardTexts()
    {
        wordCanvasUiPresenter.ClearBoard();
    }

    public void UpdateWord(string word)
    {
        wordCanvasUiPresenter.FindTextAndUpdate(word);
    }

    public void ShowLevelCompletePresenter()
    {
        levelCompleteUiPresenter.Initialize();
    }
}
