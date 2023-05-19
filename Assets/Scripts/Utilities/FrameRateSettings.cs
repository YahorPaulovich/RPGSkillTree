using UnityEngine;

public class FrameRateSettings : MonoBehaviour
{
    private void Start()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = -1;
#elif UNITY_ANDROID
        Application.targetFrameRate = 60;
#elif UNITY_IOS
    Application.targetFrameRate = 60;
#endif
    }
}
