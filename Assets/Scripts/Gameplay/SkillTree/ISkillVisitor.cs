public interface ISkillVisitor
{
    void VisitSkill(SkillLeaf skill);
    void VisitSkill(SkillGroup skill);
}
