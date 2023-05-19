using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VectorGraphics;
using DG.Tweening;

public class HintableText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform _item;
    [SerializeField] private float _endValue = 1f;
    [SerializeField] private float _cycleLength = 1f; // duration
    private SVGImage _ellipse;
    private Color _previousColor;

    private void Awake()
    {
        _ellipse = GetComponent<SVGImage>();
        _previousColor = _ellipse.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AnimateGlow();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimatePreviousColor();
    }

    private void AnimateGlow()
    {
        _ellipse.DOColor(Color.red, 1f);
    }

    private void AnimatePreviousColor()
    {
        _ellipse.DOColor(_previousColor, 1f);
    }
}
