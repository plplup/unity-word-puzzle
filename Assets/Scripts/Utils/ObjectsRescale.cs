using UnityEngine;

public class ObjectsRescale : MonoBehaviour
{
    [SerializeField] private float scaleFactor = 1f; 

    void Start()
    {
        float screenHeight = Screen.height;
        float defaultScreenHeight = 1080f; 

        float scale = screenHeight / defaultScreenHeight * scaleFactor;

        transform.localScale *= scale;
    }
}
