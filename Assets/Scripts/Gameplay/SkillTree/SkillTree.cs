using UnityEngine;

public class SkillTree : MonoBehaviour
{
    private void Start()
    {
        var root = new SkillGroup("Root");

        var group1 = new SkillGroup("Group 1");
        var group2 = new SkillGroup("Group 2");
        var group3 = new SkillGroup("Group 3");
        var group4 = new SkillGroup("Group 4");

        var skill1 = new SkillLeaf("Skill 1");
        var skill2 = new SkillLeaf("Skill 2");
        var skill3 = new SkillLeaf("Skill 3");
        var skill4 = new SkillLeaf("Skill 4");
        var skill5 = new SkillLeaf("Skill 5");
        var skill6 = new SkillLeaf("Skill 6");
        var skill7 = new SkillLeaf("Skill 7");
        var skill8 = new SkillLeaf("Skill 8");
        var skill9 = new SkillLeaf("Skill 9");
        var skill10 = new SkillLeaf("Skill 10");

        group1.AddSkill(skill2);
        group1.AddSkill(skill3);

        group2.AddSkill(skill4);
        group2.AddSkill(skill5);
        group2.AddSkill(skill7);
        group2.AddSkill(skill6);

        group3.AddSkill(skill8);
        group3.AddSkill(skill10);

        group4.AddSkill(skill9);
        group4.AddSkill(skill10);

        root.AddSkill(skill1);
        root.AddSkill(group1);
        root.AddSkill(group2);
        root.AddSkill(group3);
        root.AddSkill(group4);

        // Display the skill tree using the visitor.
        DisplaySkillTree(root);

        // Bypass the skill tree using the iterator.
        SkillTreeBypass(root);

        // Bypass the skill group using the iterator.
        //SkillGroupBypass(group1);
    }

    private static void DisplaySkillTree(SkillGroup root)
    {
        Debug.Log("Displaying the skill tree using the visitor:");
        var displayVisitor = new SkillDisplayVisitor();
        root.Accept(displayVisitor);
        Debug.Log("");
    }

    private static void SkillTreeBypass(SkillGroup root)
    {
        Debug.Log("Bypassing the skill tree using the iterator:");
        var treeIterator = new SkillTreeIterator(root);
        while (treeIterator.MoveNext())
        {
            var skill = treeIterator.GetCurrent();
            Debug.Log(skill.Name);
        }
        Debug.Log("");
    }

    private static void SkillGroupBypass(SkillGroup group)
    {
        Debug.Log("Bypassing a skill group using the iterator:");
        var groupIterator = group.CreateIterator();
        while (groupIterator.MoveNext())
        {
            var skill = groupIterator.GetCurrent();
            Debug.Log(skill.Name);
        }
        Debug.Log("");
    }
}
