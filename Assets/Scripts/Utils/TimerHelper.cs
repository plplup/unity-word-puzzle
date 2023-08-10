using UnityEngine;

public class TimeHelper : MonoBehaviour
{
    public static float Time;
    public static float DeltaTime;
    public static float FixedDeltaTime;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        Time = 0f;
        DeltaTime = 0f;
        FixedDeltaTime = 0f;
    }

    private void Update()
    {
        Time = UnityEngine.Time.time;
        DeltaTime = UnityEngine.Time.deltaTime;
        FixedDeltaTime = UnityEngine.Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        Time = UnityEngine.Time.time;
        DeltaTime = UnityEngine.Time.deltaTime;
        FixedDeltaTime = UnityEngine.Time.fixedDeltaTime;
    }
}
