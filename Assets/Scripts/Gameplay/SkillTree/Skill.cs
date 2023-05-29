using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class Skill : MonoBehaviour, IPointerUpHandler
{
    public string Name => _name;
    [SerializeField] private string _name = "Unnamed Skill";
    public int Cost => _cost;
    [SerializeField] private int _cost = 0;
    public bool IsLearned { get; set; } = false;
    public bool IsSelected { get; set; } = false;

    public TMP_Text CostIndicatorText;
    public GameObject[] SkillConnections;
    public List<Skill> Prerequisites;
    public event EventHandler OnSkillSelected;

    private void Awake()
    {
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
}
