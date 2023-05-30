using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Button _earnButton;
    public Button LearnButton;
    public Button ForgetButton;
    public Button ForgetAllButton;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();

        InitializeWithSettings();
    }

    private void InitializeWithSettings()
    {
        LearnButton.interactable = false;
        ForgetButton.interactable = false;
    }

    private void OnEnable()
    {
        _earnButton.onClick.AddListener(OnEarnOnePoint);
        LearnButton.onClick.AddListener(OnLearnSelectedSkill);
        ForgetButton.onClick.AddListener(OnForgetSelectedSkill);
        ForgetAllButton.onClick.AddListener(OnForgetAllSkills);
    }

    private void OnEarnOnePoint()
    {
        _player.EarnOnePoint();
    }

    private void OnLearnSelectedSkill()
    {
        _player.LearnSelectedSkill();
    }

    private void OnForgetSelectedSkill()
    {
        _player.ForgetSelectedSkill();
    }

    private void OnForgetAllSkills()
    {
        _player.ForgetAllSkills();
    }
    
    private void OnDisable()
    {
        _earnButton.onClick.RemoveAllListeners();
        LearnButton.onClick.RemoveAllListeners();
        ForgetButton.onClick.RemoveAllListeners();
        ForgetAllButton.onClick.RemoveAllListeners();
    }
}
