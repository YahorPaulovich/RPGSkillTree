using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VectorGraphics;
using TMPro;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(SVGImage))]
public class Skill : MonoBehaviour, IPointerUpHandler
{
    public string Name => _name;
    [SerializeField] private string _name = "Unnamed Skill";
    public int Cost => _cost;
    [SerializeField] private int _cost = 0;
    public bool IsLearned { get; set; } = false;
    public bool IsSelected { get; set; } = false;
    private SVGImage _ellipseImage;
    [HideInInspector] public Color EllipseColor;
    private Color _previousEllipseColor;

    public TMP_Text CostIndicatorText;
    public GameObject[] SkillConnections;
    public List<Skill> Prerequisites;
    public event EventHandler OnSkillSelected;

    private void Awake()
    {
        _ellipseImage = GetComponent<SVGImage>();
        EllipseColor = _ellipseImage.color;
        EllipseColor.a = 255f;
        _previousEllipseColor = EllipseColor;

        if (CostIndicatorText)
        {
            CostIndicatorText.text = Cost.ToString() + " points";
        }

        InitializeWithSettings();
    }

    private void InitializeWithSettings()
    {
        if (Name.Contains("Base"))
        {
            IsLearned = true;
        }
    }

    public void AddPrerequisite(Skill skill)
    {
        Prerequisites?.Add(skill);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnSkillSelected != null) OnSkillSelected(this, EventArgs.Empty);
    }

    public void SetColorToLearnedColor(Color learnedColor)
    {
        _ellipseImage.CrossFadeColor(learnedColor, 1f, true, false);
        _ellipseImage.color = learnedColor;
    }

    public void SetColorToUnlearnedColor(Color unlearnedColor)
    {
        _ellipseImage.CrossFadeColor(unlearnedColor, 1f, true, false);
        _ellipseImage.color = unlearnedColor;
    }

    public void SetColorToUnlearnedColor()
    {
        _ellipseImage.CrossFadeColor(_previousEllipseColor, 1f, true, false);
        _ellipseImage.color = _previousEllipseColor;
    }
}
