using System.Collections.Generic;

public class SkillGroupIterator : ISkillIterator
{
    private List<Skill> _skills;
    private int _currentIndex = -1;

    public SkillGroupIterator(List<Skill> skills)
    {
        _skills = skills;
    }

    public Skill GetCurrent()
    {
        return _skills[_currentIndex];
    }

    public bool MoveNext()
    {
        _currentIndex++;
        return _currentIndex < _skills.Count;
    }

    public void Reset()
    {
        _currentIndex = -1;
    }
}
