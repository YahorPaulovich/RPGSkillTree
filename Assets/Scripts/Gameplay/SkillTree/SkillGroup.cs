using System.Collections.Generic;

public class SkillGroup : Skill
{
    private List<Skill> skills = new List<Skill>();

    public SkillGroup(string name) : base(name)
    {
    }

    public void AddSkill(Skill skill)
    {
        skills.Add(skill);
    }

    public void RemoveSkill(Skill skill)
    {
        skills.Remove(skill);
    }

    public ISkillIterator CreateIterator()
    {
        return new SkillGroupIterator(skills);
    }

    public List<Skill> GetSkills()
    {
        return skills;
    }

    public override void Accept(ISkillVisitor visitor)
    {
        visitor.VisitSkill(this);
        foreach (var skill in skills)
        {
            skill.Accept(visitor);
        }
    }
}
