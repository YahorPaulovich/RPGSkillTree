using System.Collections.Generic;

public class SkillTreeIterator : ISkillIterator
{
    private Skill root;
    private Stack<Skill> stack = new Stack<Skill>();

    public SkillTreeIterator(Skill root)
    {
        this.root = root;
        Reset();
    }

    public Skill GetCurrent()
    {
        return stack.Peek();
    }

    public bool MoveNext()
    {
        var current = stack.Pop();

        if (current is SkillGroup group)
        {
            foreach (var skill in group.GetSkills())
            {
                stack.Push(skill);
            }
        }

        return stack.Count > 0;
    }

    public void Reset()
    {
        stack.Clear();
        stack.Push(root);
    }
}
