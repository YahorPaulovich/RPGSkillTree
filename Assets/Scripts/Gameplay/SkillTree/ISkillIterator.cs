public interface ISkillIterator
{
    Skill GetCurrent();
    bool MoveNext();
    void Reset();
}
