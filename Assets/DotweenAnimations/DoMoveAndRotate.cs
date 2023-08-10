using DG.Tweening;
using UnityEngine;

public class DoMoveAndRotate : MonoBehaviour
{
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private Ease easeType = Ease.OutQuad;

    private void Start()
    {
        MoveAndRotateRandomly();
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }

    private void MoveAndRotateRandomly()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        Vector3 randomRotation = new Vector3(Random.Range(90f, 180f), Random.Range(10, 360f), Random.Range(0f, 360f));

        DOTween.Kill(transform);

        transform.DOMove(randomPosition, animationDuration).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(randomRotation, animationDuration).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
    }
}
