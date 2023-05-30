using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaSetter : MonoBehaviour
{
    public Canvas Canvas;
    private RectTransform _panelSafeArea;

    private Rect _currentSafeArea = new Rect();
    private ScreenOrientation _currentOrientation = ScreenOrientation.Portrait;

    private void Start()
    {
        _panelSafeArea = GetComponent<RectTransform>();

        _currentOrientation = Screen.orientation;
        _currentSafeArea = Screen.safeArea;

        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        if (_panelSafeArea == null)
        {
            return;
        }

        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Canvas.pixelRect.width;
        anchorMin.y /= Canvas.pixelRect.height;

        anchorMax.x /= Canvas.pixelRect.width;
        anchorMax.y /= Canvas.pixelRect.height;

        _panelSafeArea.anchorMin = anchorMin;
        _panelSafeArea.anchorMax = anchorMax;

        _currentOrientation = Screen.orientation;
        _currentSafeArea = Screen.safeArea;
    }

    private void Update()
    {
        if (_currentOrientation != Screen.orientation || _currentSafeArea != Screen.safeArea)
        {
            ApplySafeArea();
        }
    }
}
