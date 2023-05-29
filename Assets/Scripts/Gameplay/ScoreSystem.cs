using System;

public class ScoreSystem
{
    public event EventHandler OnSpent;
    public event EventHandler OnEarned;
    public event EventHandler OnScoreChanged;

    public int Amount;
    private int _amountMax;

    public ScoreSystem(int scoreAmount)
    {
        _amountMax = 100;
        Amount = scoreAmount;
    }

    public ScoreSystem(int scoreAmountMax, int scoreAmount)
    {
        _amountMax = scoreAmountMax;
        Amount = scoreAmount;
    }

    public void Spend(int amount)
    {
        Amount -= amount;
        if (Amount < 0)
        {
            Amount = 0;
        }
        if (OnSpent != null) OnSpent(this, EventArgs.Empty);
        if (OnScoreChanged != null) OnScoreChanged(this, EventArgs.Empty);
    }

    public void Earn(int amount)
    {
        Amount += amount;
        if (Amount > _amountMax)
        {
            Amount = _amountMax;
        }
        if (OnEarned != null) OnEarned(this, EventArgs.Empty);
        if (OnScoreChanged != null) OnScoreChanged(this, EventArgs.Empty);
    }

    public float GetScoreNormalized()
    {
        return (float)Amount / _amountMax;
    }
}
