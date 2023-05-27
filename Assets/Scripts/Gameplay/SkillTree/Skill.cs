public abstract class Skill
{
    public string Name { get; }
    public string Description { get; }
    public int Cost { get; }

    public Skill(string name)
    {
        Name = name;
        Description = string.Empty;
        Cost = 0;
    }

    public Skill(string name, string description)
    {
        Name = name;
        Description = description;
        Cost = 0;
    }

    public Skill(string name, string description, int cost)
    {
        Name = name;
        Description = description;
        Cost = cost;
    }

    public abstract void Accept(ISkillVisitor visitor);
}
