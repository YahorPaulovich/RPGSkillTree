using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    [HideInInspector] public List<Skill> Skills => _skills;
    [SerializeField] private List<Skill> _skills;
    private List<Skill> _learnedSkills;
    [SerializeField] private List<GameObject> _skillConnections;
    public Skill CurrentSelectedSkill { get; set; }
    private Skill _previousSelectedSkill { get; set; }

    public event EventHandler OnSkillLearned;
    public event EventHandler OnSkillForgotten;
    public event EventHandler OnSkillSelected;

    private void Awake()
    {      
        if (_skills == null || _skillConnections == null)
        {
            throw new NullReferenceException();
        }
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
        CurrentSelectedSkill.IsSelected = true;
        
        _previousSelectedSkill = CurrentSelectedSkill;
        if (OnSkillSelected != null) OnSkillSelected(this, EventArgs.Empty);
    }

    public void AddSkill(Skill skill)
    {
        _skills?.Add(skill);
    }

    public void LearnSkill(Skill skill)
    {
        if (CanLearnSkill(skill))
        {
            skill.IsLearned = true;
            _learnedSkills.Add(skill);
            foreach (var connection in skill.SkillConnections)
            {
                connection.SetActive(true);
            }
            skill.gameObject.GetComponent<Button>().interactable = true;
            Debug.Log("Learned skill: " + skill.Name);
            if (OnSkillLearned != null) OnSkillLearned(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Cannot learn skill: " + skill.Name);
        }
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

    public void ForgetSkill(Skill skill)
    {
        if (CanForgetSkill(skill))
        {
            skill.IsLearned = false;
            _learnedSkills.Remove(skill);
            foreach (var connection in skill.SkillConnections)
            {
                connection.SetActive(false);
            }
            skill.gameObject.GetComponent<Button>().interactable = false;
            Debug.Log("Forgot skill: " + skill.Name);
            if (OnSkillForgotten != null) OnSkillForgotten(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Cannot forget skill: " + skill.Name);
        }
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
}
