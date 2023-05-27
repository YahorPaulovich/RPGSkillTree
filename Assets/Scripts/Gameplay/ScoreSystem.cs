using System;

public class ScoreSystem
{
    public event EventHandler OnSpent;
    public event EventHandler OnEarned;

    public int ScoreAmount;
    private int _scoreAmountMax;

    public ScoreSystem(int scoreAmount)
    {
        _scoreAmountMax = 100;
        ScoreAmount = scoreAmount;
    }

    public ScoreSystem(int scoreAmountMax, int scoreAmount)
    {
        _scoreAmountMax = scoreAmountMax;
        ScoreAmount = scoreAmount;
    }

    public void Spend(int amount)
    {
        ScoreAmount -= amount;
        if (ScoreAmount < 0)
        {
            ScoreAmount = 0;
        }
        if (OnSpent != null) OnSpent(this, EventArgs.Empty);
    }

    public void Earn(int amount)
    {
        ScoreAmount += amount;
        if (ScoreAmount > _scoreAmountMax)
        {
            ScoreAmount = _scoreAmountMax;
        }
        if (OnEarned != null) OnEarned(this, EventArgs.Empty);
    }

    public float GetScoreNormalized()
    {
        return (float)ScoreAmount / _scoreAmountMax;
    }
}
