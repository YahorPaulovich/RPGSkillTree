//using UnityEngine;

//public class SkillTreeVisitor : ISkillVisitor<SkillLeaf>
//{
//    //public void VisitSkill(SkillLeaf skill)
//    //{
//    //    throw new System.NotImplementedException();
//    //}

//    //public void VisitSkill(SkillGroup skill) { }
//    private GameObject[] _skillVertexes;
//    public SkillTreeVisitor(ref GameObject[] skillVertexes)
//    {
//        _skillVertexes = skillVertexes;
//    }

//    SkillLeaf ISkillVisitor<SkillLeaf>.VisitSkill(SkillLeaf skill)
//    {
//        Debug.Log($"Skill: {skill.Name}; Cost: {skill.Cost};");
//        for (int i = 0; i < _skillVertexes.Length; i++)
//        {
//            _skillVertexes[i].GetComponent<SkillVertex>().CostIndicatorText.text = skill.Cost.ToString() + " points";
//        }
//        return skill;
//    }

//    SkillLeaf ISkillVisitor<SkillLeaf>.VisitSkill(SkillGroup skill)
//    {
//        // throw new System.NotImplementedException();

//        return new SkillLeaf("");
//    }
//}
