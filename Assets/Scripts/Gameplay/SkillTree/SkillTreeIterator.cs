using System.Collections.Generic;

public class SkillTreeIterator : ISkillIterator
{
    private Skill _root;
    private Stack<Skill> _stack = new Stack<Skill>();

    public SkillTreeIterator(Skill root)
    {
        _root = root;
        Reset();
    }

    public Skill GetCurrent()
    {
        return _stack.Peek();
    }

    public bool MoveNext()
    {
        var current = _stack.Pop();

        if (current is SkillGroup group)
        {
            foreach (var skill in group.GetSkills())
            {
                _stack.Push(skill);
            }
        }

        return _stack.Count > 0;
    }

    public void Reset()
    {
        _stack.Clear();
        _stack.Push(_root);
    }
}
