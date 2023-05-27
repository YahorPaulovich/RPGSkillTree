public class SkillLeaf : Skill
{
    public SkillLeaf(string name) : base(name)
    {
    }

    public SkillLeaf(string name, string description) : base(name, description) { }

    public SkillLeaf(string name, string description, int cost) : base(name, description, cost) { }

    public override void Accept(ISkillVisitor visitor)
    {
        visitor.VisitSkill(this);
    }
}
