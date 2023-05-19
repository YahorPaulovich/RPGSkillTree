using UnityEngine;

public class FramerateCounter : MonoBehaviour
{
    private float _deltaTime = 0.0f;

    private void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        int width = Screen.width;
        int height = Screen.height;

        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(0f, 0f, width, height * 2f / 100f);

        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = height * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        style.normal.background = Texture2D.whiteTexture;

        float msec = _deltaTime * 1000.0f;
        float fps = 1.0f / _deltaTime;
        string text = $"{msec:0.0} ms ({fps:0.} fps)";

        GUI.Label(rect, text, style);
    }
} // http://wiki.unity3d.com/index.php?title=FramesPerSecond
