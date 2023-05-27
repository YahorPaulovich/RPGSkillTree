using System.Collections.Generic;

public class SkillGroup : Skill
{
    public int Count => _count;
    private int _count = 0;
    private List<Skill> _skills = new List<Skill>();

    public SkillGroup(string name) : base(name)
    {
    }

    public void AddSkill(Skill skill)
    {
        _skills.Add(skill);
        _count++;
    }

    public void RemoveSkill(Skill skill)
    {
        _skills.Remove(skill);
        _count--;
    }

    public ISkillIterator CreateIterator()
    {
        return new SkillGroupIterator(_skills);
    }

    public List<Skill> GetSkills()
    {
        return _skills;
    }

    public override void Accept(ISkillVisitor visitor)
    {
        visitor.VisitSkill(this);
        foreach (var skill in _skills)
        {
            skill.Accept(visitor);
        }
    }
}
