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
    private Skill _currentSelectedSkill;
    private Skill _previousSelectedSkill;
    public event EventHandler OnSkillSelected;
    public event EventHandler OnSkillDeselected;

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
        foreach (var skill in _skillTree.Skills)
        {
            skill.OnSkillSelected += Skill_OnSkillSelected;
        }
        OnSkillDeselected += SkillTree_OnSkillDeselected;
        Score.OnScoreChanged += ScoreSystem_OnScoreChanged;
        _skillTree.OnSkillLearned += SkillTree_OnSkillLearned;
        _skillTree.OnSkillForgotten += SkillTree_OnSkillForgotten;
    }

    private void Skill_OnSkillSelected(object sender, EventArgs e)
    {     
        _currentSelectedSkill = (Skill)sender;

        if (CanLearnSkill(_currentSelectedSkill) && Score.Amount >= _currentSelectedSkill.Cost && !_currentSelectedSkill.IsLearned)
        {
            if (_previousSelectedSkill)
            {
                _previousSelectedSkill.IsSelected = false;
            }

            _currentSelectedSkill.IsSelected = true;
            _previousSelectedSkill = _currentSelectedSkill;
            if (OnSkillDeselected != null) OnSkillDeselected(this, EventArgs.Empty);

            _playerControls.LearnButton.interactable = true;
            if (OnSkillSelected != null) OnSkillSelected(this, EventArgs.Empty);
        }
        else if(CanForgetSkill(_currentSelectedSkill) && _currentSelectedSkill.IsLearned)
        {
            if (_previousSelectedSkill)
            {
                _previousSelectedSkill.IsSelected = false;
            }

            _currentSelectedSkill.IsSelected = true;
            _previousSelectedSkill = _currentSelectedSkill;
            if (OnSkillDeselected != null) OnSkillDeselected(this, EventArgs.Empty);

            _playerControls.ForgetButton.interactable = true;
            if (OnSkillSelected != null) OnSkillSelected(this, EventArgs.Empty);
        }
        else
        {
            _currentSelectedSkill.IsSelected = false;

            if (OnSkillDeselected != null) OnSkillDeselected(this, EventArgs.Empty);
        }
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
        _currentSelectedSkill.SetColorToLearnedColor(_learnedSkillColor);
        foreach (var connection in _currentSelectedSkill.SkillConnections)
        {
            connection.SetActive(true);
        }
    }

    private void SkillTree_OnSkillForgotten(object sender, EventArgs e)
    {
        _currentSelectedSkill.SetColorToUnlearnedColor(_unlearnedSkillColor);
        foreach (var connection in _currentSelectedSkill.SkillConnections)
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
        if (_currentSelectedSkill)
        {
            if (Score.Amount >= _currentSelectedSkill.Cost)
            {
                if ((bool)(_skillTree?.LearnSkill(_currentSelectedSkill)))
                {
                    Score.Spend(_currentSelectedSkill.Cost);
                }
            }
            else
            {
                Debug.Log("Player doesn't have enough points to learn skill: " + _currentSelectedSkill.Name);
            }
        }   
    }

    public void ForgetSelectedSkill()
    {
        if (_currentSelectedSkill)
        {
            ForgetSkill(_currentSelectedSkill);
        }    
    }

    public void ForgetAllSkills()
    {
        foreach (var skill in _skillTree.Skills)
        {
            ForgetSkill(skill);
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

    private bool CanLearnSkill(Skill skill)
    {
        return _skillTree.CanLearnSkill(skill);
    }

    private void OnDestroy()
    {
        foreach (var skill in _skillTree.Skills)
        {
            skill.OnSkillSelected -= Skill_OnSkillSelected;
        }
        OnSkillDeselected -= SkillTree_OnSkillDeselected;
        Score.OnScoreChanged -= ScoreSystem_OnScoreChanged;
        _skillTree.OnSkillLearned -= SkillTree_OnSkillLearned;
        _skillTree.OnSkillForgotten -= SkillTree_OnSkillForgotten;
    }
}
