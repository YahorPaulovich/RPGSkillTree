public class SkillLeaf : Skill
{
    public SkillLeaf(string name) : base(name)
    {
    }

    public override void Accept(ISkillVisitor visitor)
    {
        visitor.VisitSkill(this);
    }
}
