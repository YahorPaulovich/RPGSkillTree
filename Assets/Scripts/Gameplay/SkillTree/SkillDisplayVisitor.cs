using UnityEngine;

public class SkillDisplayVisitor : ISkillVisitor
{
    public void VisitSkill(SkillLeaf skill)
    {
        Debug.Log($"Skill: {skill.Name}");
    }

    public void VisitSkill(SkillGroup skill)
    {
        Debug.Log($"Skill Group: {skill.Name}");
    }
}
