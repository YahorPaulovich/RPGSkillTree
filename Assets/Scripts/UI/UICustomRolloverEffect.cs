using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VectorGraphics;
using TMPro;

public class UICustomRolloverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SVGImage _ellipseImage;
    [SerializeField, ColorUsage(true)] private Color _ellipseColor;
    private Color _previousEllipseColor;

    [SerializeField] private SVGImage _costIndicatorImage;
    [SerializeField] private TMP_Text _costIndicatorText;

    private void Awake()
    {
        _previousEllipseColor = _ellipseImage.color;
        //_ellipseColor.a = 255f;

        _costIndicatorImage.CrossFadeAlpha(0.0f, 0f, true);
        _costIndicatorText.CrossFadeAlpha(0.0f, 0f, true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverAnimation();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ExitAnimation();
    }

    private void HoverAnimation()
    {
        _ellipseImage.CrossFadeColor(_ellipseColor, 1f, true, false);
        _costIndicatorImage.CrossFadeAlpha(0.9f, 0.5f, true);
        _costIndicatorText.CrossFadeAlpha(0.9f, 0.5f, true);
    }

    private void ExitAnimation()
    {
        _ellipseImage.CrossFadeColor(_previousEllipseColor, 1f, true, false);
        _costIndicatorImage.CrossFadeAlpha(0.0f, 0.5f, true);
        _costIndicatorText.CrossFadeAlpha(0.0f, 0.5f, true);
    }
}
