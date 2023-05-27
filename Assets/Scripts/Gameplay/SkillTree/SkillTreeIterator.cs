using System.Collections.Generic;

public class SkillTreeIterator : ISkillIterator
{
    public int CurrentIndex => _currentIndex;
    public int Count => _count;

    private int _currentIndex = -1;
    private int _count = 0;

    private Skill _root;
    private Stack<Skill> _stack = new Stack<Skill>();

    public SkillTreeIterator(Skill root)
    {
        _root = root;
        Reset();
        _currentIndex = 0;
        _count = 1;
    }

    public Skill GetCurrent()
    {
        return _stack.Peek();
    }

    public bool MoveNext()
    {
        var current = _stack.Pop();
        _currentIndex++;

        if (current is SkillGroup group)
        {
            foreach (var skill in group.GetSkills())
            {
                _stack.Push(skill);
                _count++;
            }
        }

        return _stack.Count > 0;
    }

    public void Reset()
    {
        _stack.Clear();
        _stack.Push(_root);
        _currentIndex = -1;
    }
}
