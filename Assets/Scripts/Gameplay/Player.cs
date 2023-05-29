using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerControls))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _initialPointsAmount = 0;
    public ScoreSystem Score { get; private set; }
    [SerializeField] private SkillTree _skillTree;
    private PlayerControls _playerControls;

    private void Awake()
    {
        if (_skillTree == null)
        {
            Debug.LogError(new NullReferenceException().Message);
        }
        _playerControls = GetComponent<PlayerControls>();
        Score = new ScoreSystem(_initialPointsAmount);   
    }

    private void Start()
    {
        TryToMakeSkillsActive();
        Score.OnScoreChanged += ScoreSystem_OnScoreChanged;
        _skillTree.OnSkillSelected += _skillTree_OnSkillSelected;
    }

    private void _skillTree_OnSkillSelected(object sender, EventArgs e)
    {
        _playerControls.LearnButton.interactable = true;
        _playerControls.ForgetButton.interactable = true;
    }

    private void ScoreSystem_OnScoreChanged(object sender, EventArgs e)
    {
        TryToMakeSkillsActive();
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
            _skillTree?.LearnSkill(skill);
            Score.Spend(skill.Cost);
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
            _skillTree?.ForgetSkill(skill);
            Score.Earn(skill.Cost);
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
}
