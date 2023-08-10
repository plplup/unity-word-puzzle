using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveDuration = .5f;

    private Vector3 initialPosition;
    private const float constantA = 1.70158f;
    private const float constantB = constantA * 1.525f;
    private const float constantC = constantA + 1f;

    private IEnumerator Move(Vector3 targetPosition, Action OnComplete = null)
    {
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            float easeFactor = EaseOutBack(t);
            transform.position = Vector3.LerpUnclamped(initialPosition, targetPosition, easeFactor);

            elapsedTime += TimeHelper.DeltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        if (OnComplete != null)
            OnComplete();
    }

    private float EaseOutBack(float time)
    {
        float overshoot = 1f;
        time -= 1f;
        return time * time * ((overshoot + 1f) * time + overshoot) + 1f;
        //return 1f + constantC * Mathf.Pow(time - 1, 3) + constantA * Mathf.Pow(time - 1, 2);
    }

    public void MoveToTarget(Vector3 targetPosition, Action OnComplete = null)
    {
        initialPosition = transform.position;

        if (initialPosition == targetPosition)
            return;

        StartCoroutine(Move(targetPosition, OnComplete));
    }
}
