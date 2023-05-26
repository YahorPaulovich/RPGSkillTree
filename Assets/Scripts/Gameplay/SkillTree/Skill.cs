public abstract class Skill
{
    public string Name { get; }

    public Skill(string name)
    {
        Name = name;
    }

    public abstract void Accept(ISkillVisitor visitor);
}
