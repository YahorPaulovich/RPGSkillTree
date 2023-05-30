using System;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerControls))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _initialPointsAmount = 0;
    public ScoreSystem Score { get; private set; }
    [SerializeField] private SkillTree _skillTree;
    //public List<Skill> LearnedSkills { get; set; }
    private PlayerControls _playerControls;

    private Color _learnedSkillColor;
    private Color _unlearnedSkillColor;

    private void Awake()
    {
        if (_skillTree == null)
        {
            throw new NullReferenceException();
        }
        else
        {
            _learnedSkillColor = _skillTree.LearnedSkillColor;
            _unlearnedSkillColor = _skillTree.UnlearnedSkillColor;
        }
        _playerControls = GetComponent<PlayerControls>();
        Score = new ScoreSystem(_initialPointsAmount);
    }

    private void Start()
    {
        TryToMakeSkillsActive();
        _skillTree.OnSkillSelected += SkillTree_OnSkillSelected;
        _skillTree.OnSkillDeselected += SkillTree_OnSkillDeselected;
        Score.OnScoreChanged += ScoreSystem_OnScoreChanged;
        _skillTree.OnSkillLearned += SkillTree_OnSkillLearned;
        _skillTree.OnSkillForgotten += SkillTree_OnSkillForgotten;
    }

    private void SkillTree_OnSkillSelected(object sender, EventArgs e)
    {
        _playerControls.LearnButton.interactable = true;
        _playerControls.ForgetButton.interactable = true;
    }

    private void SkillTree_OnSkillDeselected(object sender, EventArgs e)
    {
        _playerControls.LearnButton.interactable = false;
        _playerControls.ForgetButton.interactable = false;
    }

    private void ScoreSystem_OnScoreChanged(object sender, EventArgs e)
    {
        TryToMakeSkillsActive();
    }

    private void SkillTree_OnSkillLearned(object sender, EventArgs e)
    {
        _skillTree.CurrentSelectedSkill.SetColorToLearnedColor(_learnedSkillColor);
        foreach (var connection in _skillTree.CurrentSelectedSkill.SkillConnections)
        {
            connection.SetActive(true);
        }
    }

    private void SkillTree_OnSkillForgotten(object sender, EventArgs e)
    {
        _skillTree.CurrentSelectedSkill.SetColorToUnlearnedColor(_unlearnedSkillColor);
        foreach (var connection in _skillTree.CurrentSelectedSkill.SkillConnections)
        {
            connection.SetActive(false);
        }
    }

    private void TryToMakeSkillsActive()
    {
        var skills = _skillTree.GetAllSkillsThatCanBeLearned();
        foreach (var skill in skills)
        {
            if (Score.Amount >= skill.Cost)
            {
                skill.gameObject.GetComponent<Button>().interactable = true;
            }
            else if (skill.IsLearned == false)
            {
                skill.gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void EarnOnePoint()
    {
        Score?.Earn(1);
    }

    public void LearnSelectedSkill()
    {
        if (_skillTree?.CurrentSelectedSkill)
        {
            LearnSkill(_skillTree?.CurrentSelectedSkill);
        }   
    }

    public void ForgetSelectedSkill()
    {
        if (_skillTree?.CurrentSelectedSkill)
        {
            ForgetSkill(_skillTree?.CurrentSelectedSkill);
        }    
    }

    public void ForgetAllSkills()
    {
        foreach (var skill in _skillTree.Skills)
        {
            ForgetSkill(skill);
        }
    }

    private void LearnSkill(Skill skill)
    {
        if (Score.Amount >= skill.Cost)
        {
            if ((bool)(_skillTree?.LearnSkill(skill)))
            {
                Score.Spend(skill.Cost);
            }       
        }
        else
        {
            Debug.Log("Player doesn't have enough points to learn skill: " + skill.Name);
        }
    }

    private void ForgetSkill(Skill skill)
    {
        if (CanForgetSkill(skill))
        {
            if ((bool)(_skillTree?.ForgetSkill(skill)))
            {
                Score.Earn(skill.Cost);
            }       
        }
        else
        {
            Debug.Log("Cannot forget skill: " + skill.Name);
        }
    }

    private bool CanForgetSkill(Skill skill)
    {
        return _skillTree.CanForgetSkill(skill);
    }

    private void OnDestroy()
    {
        _skillTree.OnSkillSelected -= SkillTree_OnSkillSelected;
        _skillTree.OnSkillDeselected -= SkillTree_OnSkillDeselected;
        Score.OnScoreChanged -= ScoreSystem_OnScoreChanged;
        _skillTree.OnSkillLearned -= SkillTree_OnSkillLearned;
        _skillTree.OnSkillForgotten -= SkillTree_OnSkillForgotten;
    }
}
