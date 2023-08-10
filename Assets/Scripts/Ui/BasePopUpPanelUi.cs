using UnityEngine;
using UnityEngine.UI;

public class BasePopUpPanelUi : MonoBehaviour
{
    [Header("Base reference settings")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button closeButton;
    [SerializeField] private DoFadeAndScaleSeparated tweenAnimation;

    protected virtual void Awake()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    public virtual void Initialize()
    {
        ActivatePanel();
    }

    private void OnCloseButtonClick()
    {
        canvas.enabled = false;
    }

    private void ActivatePanel()
    {
        canvas.enabled = true;

        if (tweenAnimation != null)
            tweenAnimation.InitializeTween();
    }

}
