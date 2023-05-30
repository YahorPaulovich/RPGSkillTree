using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [HideInInspector] public List<Skill> Skills => _skills;
    [SerializeField] private List<Skill> _skills;
    [HideInInspector] public List<Skill> LearnedSkills;

    [ColorUsage(true, true)] public Color LearnedSkillColor;
    [ColorUsage(true, true)] public Color UnlearnedSkillColor;

    public event EventHandler OnSkillLearned;
    public event EventHandler OnSkillForgotten;

    private Dictionary<Skill, List<Skill>> _skillTree;

    private void Awake()
    {
        if (_skills == null)
        {
            throw new NullReferenceException();
        }
        LearnedSkillColor.a = 255f;
        UnlearnedSkillColor.a = 255f;

        LearnedSkills = new List<Skill>();
        _skillTree = new Dictionary<Skill, List<Skill>>();

        foreach (var skill in _skills)
        {
            if (skill.Name.Contains("Base"))
            {
                LearnedSkills.Add(skill);
            }

            _skillTree[skill] = new List<Skill>();
            foreach (var prerequisite in skill.Prerequisites)
            {
                _skillTree[skill].Add(prerequisite);
            }
        }
    }

    public void AddSkill(Skill skill)
    {
        _skills?.Add(skill);
        _skillTree[skill] = new List<Skill>();
    }

    public bool LearnSkill(Skill skill)
    {
        if (CanLearnSkill(skill))
        {
            skill.IsLearned = true;
            LearnedSkills.Add(skill);
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

    public bool CanLearnSkill(Skill skill)
    {
        if (_skillTree.ContainsKey(skill))
        {
            var prerequisites = _skillTree[skill];
            if (prerequisites.Count == 0)
            {
                return true;
            }

            foreach (var prerequisite in prerequisites)
            {
                if (!prerequisite.IsLearned)
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    public bool ForgetSkill(Skill skill)
    {
        if (CanForgetSkill(skill))
        {
            if (skill.IsBaseSkill)
            {
                return false;
            }

            skill.IsLearned = false;
            LearnedSkills.Remove(skill);
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
        if (skill.IsBaseSkill)
        {
            return false;
        }

        if (_skillTree.ContainsKey(skill))
        {
            var prerequisites = _skillTree[skill];
            if (prerequisites.Count == 0)
            {
                return true;
            }

            foreach (var learnedSkill in LearnedSkills)
            {
                if (learnedSkill == skill && !HasLinkToBaseSkill(learnedSkill))
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }


    private bool HasLinkToBaseSkill(Skill skill)
    {
        if (skill.Name.Contains("Base"))
        {
            return true;
        }

        if (_skillTree.ContainsKey(skill))
        {
            var prerequisites = _skillTree[skill];
            foreach (var prerequisite in prerequisites)
            {
                if (HasLinkToBaseSkill(prerequisite))
                {
                    return true;
                }
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
