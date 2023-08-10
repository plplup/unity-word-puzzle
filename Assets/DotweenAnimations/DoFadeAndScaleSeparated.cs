using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DoFadeAndScaleSeparated : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private Transform objectToScale;
    [SerializeField] private float fadeInTweenDuration;
    [SerializeField] private float fadeAmount;
    [SerializeField] private float scaleDuration;
    [SerializeField] private Vector3 scaleInitialSize = Vector3.one * 0.8f;
    [SerializeField] private Vector3 scaleFinalSize = Vector3.one;
    [SerializeField] private Ease scaleTweenEase;

    private Sequence sequence;

    public void InitializeTween(GameObject objectToDisable = null)
    {
        if (fadeImage == null || objectToScale == null)
            return;

        sequence = DOTween.Sequence()
            .Append(fadeImage.DOFade(fadeAmount, fadeInTweenDuration).From(0))
            .Insert(0, objectToScale.DOScale(scaleFinalSize, scaleDuration).From(scaleInitialSize).SetEase(scaleTweenEase))
            .OnRewind(() => 
            {
                if (objectToDisable != null)
                    objectToDisable.SetActive(false);

                sequence.Kill();

            }).SetAutoKill(false);
    }

    public void PlayTweenBackwards()
    {
        if (sequence == null)
            return;

        sequence.PlayBackwards();
    }
}
