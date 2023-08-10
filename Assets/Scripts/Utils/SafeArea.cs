using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform panelSafeArea;

    private void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        if (panelSafeArea == null)
            return;

        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;

        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;

        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;
    }
}
