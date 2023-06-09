using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBarFade : MonoBehaviour
{
    private const float SPENT_SCORE_FADE_TIMER_MAX = 0.6f;

    [SerializeField] private Image _barImage;
    [SerializeField] private Image _spentBarImage;
    [SerializeField] private TMP_Text _scoreText;

    private Color _spentColor;
    private float _spentScoreFadeTimer;
    [SerializeField] private Player _player;

    private void Awake()
    {
        _spentColor = _spentBarImage.color;
        _spentColor.a = 0f;
        _spentBarImage.color = _spentColor;
    }

    private void Start()
    {
        SetScore(_player.Score.GetScoreNormalized());
        _player.Score.OnSpent += ScoreSystem_OnSpent;
        _player.Score.OnEarned += ScoreSystem_OnEarned;
    }

    private void Update()
    {
        if (_spentColor.a > 0)
        {
            _spentScoreFadeTimer -= Time.deltaTime;
            if (_spentScoreFadeTimer < 0)
            {
                float fadeAmount = 5f;
                _spentColor.a -= fadeAmount * Time.deltaTime;
                _spentBarImage.color = _spentColor;
            }
        }
    }

    private void ScoreSystem_OnEarned(object sender, System.EventArgs e)
    {
        SetScore(_player.Score.GetScoreNormalized());
    }

    private void ScoreSystem_OnSpent(object sender, System.EventArgs e)
    {
        if (_spentColor.a <= 0)
        {
            // Spent bar image is invisible
            _spentBarImage.fillAmount = _barImage.fillAmount;
        }
        _spentColor.a = 1;
        _spentBarImage.color = _spentColor;
        _spentScoreFadeTimer = SPENT_SCORE_FADE_TIMER_MAX;

        SetScore(_player.Score.GetScoreNormalized());
    }

    private void SetScore(float healthNormalized)
    {
        _barImage.fillAmount = healthNormalized;
        _scoreText.text = _player.Score.Amount.ToString();
    }

    private void OnDestroy()
    {
        _player.Score.OnSpent -= ScoreSystem_OnSpent;
        _player.Score.OnEarned -= ScoreSystem_OnEarned;
    }
}
