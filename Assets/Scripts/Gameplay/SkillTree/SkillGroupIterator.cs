using System.Collections.Generic;

public class SkillGroupIterator : ISkillIterator
{
    public int Count => _count;
    private int _count = 0;

    private List<Skill> _skills;
    private int _currentIndex = -1;

    public SkillGroupIterator(List<Skill> skills)
    {
        _skills = skills;
        _count = 1;
    }

    public Skill GetCurrent()
    {
        return _skills[_currentIndex];
    }

    public bool MoveNext()
    {
        _currentIndex++;
        _count++;
        return _currentIndex < _skills.Count;
    }

    public void Reset()
    {
        _currentIndex = -1;
    }
}
