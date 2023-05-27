using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _initialPointsAmount = 0;
    public ScoreSystem Score { get; set; }

    private void Awake()
    {
        Score = new ScoreSystem(_initialPointsAmount);
    }

    public void EarnPoints()
    {
        Score.Earn(1);
    }

    public void SpendPoints()
    {
        Score.Spend(1);
    }

    public void EarnAllPoints()
    {
        Score.Earn(100);
    }
}
