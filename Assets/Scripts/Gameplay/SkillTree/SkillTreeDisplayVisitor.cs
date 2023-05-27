using UnityEngine;

public class SkillDisplayVisitor : ISkillVisitor
{
    public void VisitSkill(SkillLeaf skill)
    {
        Debug.Log($"Skill: {skill.Name}; Cost: {skill.Cost};");
    }

    public void VisitSkill(SkillGroup skill)
    {
        Debug.Log($"Skill Group: {skill.Name}");
    }
}
