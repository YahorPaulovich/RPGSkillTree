using System.Collections.Generic;

public class SkillGroupIterator : ISkillIterator
{
    private List<Skill> skills;
    private int currentIndex = -1;

    public SkillGroupIterator(List<Skill> skills)
    {
        this.skills = skills;
    }

    public Skill GetCurrent()
    {
        return skills[currentIndex];
    }

    public bool MoveNext()
    {
        currentIndex++;
        return currentIndex < skills.Count;
    }

    public void Reset()
    {
        currentIndex = -1;
    }
}
