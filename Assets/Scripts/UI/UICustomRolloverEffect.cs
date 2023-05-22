using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VectorGraphics;
using DG.Tweening;
using TMPro;

public class UICustomRolloverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SVGImage _ellipseImage;
    [SerializeField] private Color _ellipseColor;
    private Color _previousEllipseColor;

    [SerializeField] private SVGImage _costIndicatorImage;
    [SerializeField] private TMP_Text _costIndicatorText;

    private void Awake()
    {
        _previousEllipseColor = _ellipseImage.color;
        _ellipseColor.a = 255f;

        _costIndicatorImage.CrossFadeAlpha(0.0f, 0f, true);
        _costIndicatorText.CrossFadeAlpha(0.0f, 0f, true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AnimateGlow();

        _costIndicatorImage.CrossFadeAlpha(0.9f, 0.5f, true);
        _costIndicatorText.CrossFadeAlpha(0.9f, 0.5f, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimatePreviousColor();

        _costIndicatorImage.CrossFadeAlpha(0.0f, 0.5f, true);
        _costIndicatorText.CrossFadeAlpha(0.0f, 0.5f, true);
    }

    private void AnimateGlow()
    {
        _ellipseImage.DOColor(_ellipseColor, 1f);
    }

    private void AnimatePreviousColor()
    {
        _ellipseImage.DOColor(_previousEllipseColor, 1f);
    }
}
