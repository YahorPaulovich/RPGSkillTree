using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [HideInInspector] public List<Skill> Skills => _skills;
    [SerializeField] private List<Skill> _skills;
    private List<Skill> _learnedSkills;
    public Skill CurrentSelectedSkill { get; set; }
    private Skill _previousSelectedSkill { get; set; }

    [ColorUsage(true, true)] public Color LearnedSkillColor;
    [ColorUsage(true, true)] public Color UnlearnedSkillColor;

    public event EventHandler OnSkillLearned;
    public event EventHandler OnSkillForgotten;
    public event EventHandler OnSkillSelected;
    public event EventHandler OnSkillDeselected;

    private void Awake()
    {      
        if (_skills == null)
        {
            throw new NullReferenceException();
        }
        LearnedSkillColor.a = 255f;
        UnlearnedSkillColor.a = 255f;

        _learnedSkills = new List<Skill>();
        foreach (var skill in _skills)
        {
            if (skill.Name.Contains("Base"))
            {
                _learnedSkills.Add(skill);
            }
        }
    }

    private void Start()
    {
        foreach (var skill in _skills)
        {
            skill.OnSkillSelected += Skill_OnSkillSelected;
        }
    }

    public void Skill_OnSkillSelected(object sender, EventArgs e)
    {
        if (_previousSelectedSkill)
        {
            _previousSelectedSkill.IsSelected = false;
        }

        CurrentSelectedSkill = (Skill)sender;

        if (CanLearnSkill(CurrentSelectedSkill))
        {
            CurrentSelectedSkill.IsSelected = true;
            _previousSelectedSkill = CurrentSelectedSkill;

            if (OnSkillSelected != null) OnSkillSelected(this, EventArgs.Empty);
        }
        else
        {
            CurrentSelectedSkill.IsSelected = false;

            if (OnSkillDeselected != null) OnSkillDeselected(this, EventArgs.Empty);
        }      
    }

    public void AddSkill(Skill skill)
    {
        _skills?.Add(skill);
    }

    public bool LearnSkill(Skill skill)
    {
        if (CanLearnSkill(skill))
        {
            skill.IsLearned = true;
            _learnedSkills.Add(skill);
            Debug.Log("Learned skill: " + skill.Name);
            if (OnSkillLearned != null) OnSkillLearned(this, EventArgs.Empty);

            return true;
        }
        else
        {
            Debug.Log("Cannot learn skill: " + skill.Name);
        }
        return false;
    }

    private bool CanLearnSkill(Skill skill)
    {
        foreach (var prerequisite in skill.Prerequisites)
        {
            if (!prerequisite.IsLearned)
            {
                return false;
            }
        }

        return true;
    }

    public bool ForgetSkill(Skill skill)
    {
        if (CanForgetSkill(skill))
        {
            skill.IsLearned = false;
            _learnedSkills.Remove(skill);
            Debug.Log("Forgot skill: " + skill.Name);
            if (OnSkillForgotten != null) OnSkillForgotten(this, EventArgs.Empty);
            return true;
        }
        else
        {
            Debug.Log("Cannot forget skill: " + skill.Name);
        }
        return false;
    }

    public bool CanForgetSkill(Skill skill)
    {
        if (skill == _skills[0])
        {
            return false;
        }

        foreach (var learnedSkill in _learnedSkills)
        {
            if (learnedSkill != skill && !HasLinkToBaseSkill(learnedSkill))
            {
                return false;
            }
        }

        return true;
    }


    private bool HasLinkToBaseSkill(Skill skill)
    {
        if (skill == _skills[0])
        {
            return true;
        }

        foreach (var prerequisite in skill.Prerequisites)
        {
            if (HasLinkToBaseSkill(prerequisite))
            {
                return true;
            }
        }

        return false;
    }

    public List<Skill> GetAllSkillsThatCanBeLearned()
    {
        var allSkillsThatCanBeLearned = new List<Skill>();
        foreach (var skill in _skills)
        {
            if (CanLearnSkill(skill))
            {
                allSkillsThatCanBeLearned.Add(skill);
            }
        }
        return allSkillsThatCanBeLearned;
    }

    private void OnDestroy()
    {
        foreach (var skill in _skills)
        {
            skill.OnSkillSelected -= Skill_OnSkillSelected;
        }
    }
}
