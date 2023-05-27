using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject[] _skillVertexes = new GameObject[10];

    private void Start()
    {
        var root = new SkillGroup("Root");

        var group1 = new SkillGroup("Group 1");
        var group2 = new SkillGroup("Group 2");
        var group3 = new SkillGroup("Group 3");
        var group4 = new SkillGroup("Group 4");

        var skill1 = new SkillLeaf("Skill 1", "", 4);
        var skill2 = new SkillLeaf("Skill 2", "", 6);
        var skill3 = new SkillLeaf("Skill 3", "", 8);
        var skill4 = new SkillLeaf("Skill 4", "", 10);
        var skill5 = new SkillLeaf("Skill 5", "", 12);
        var skill6 = new SkillLeaf("Skill 6", "", 14);
        var skill7 = new SkillLeaf("Skill 7", "", 16);
        var skill8 = new SkillLeaf("Skill 8", "", 10);
        var skill9 = new SkillLeaf("Skill 9", "", 10);
        var skill10 = new SkillLeaf("Skill 10", "", 10);

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

        // Bypass the skill tree using the iterator.
        SkillTreeBypassV2(root);
    }

    private static void DisplaySkillTree(SkillGroup group)
    {
        Debug.Log("Displaying the skill tree using the visitor:");
        var displayVisitor = new SkillDisplayVisitor();
        group.Accept(displayVisitor);
    }

    private static void SkillTreeBypass(SkillGroup group)
    {
        Debug.Log("Bypassing the skill tree using the iterator:");
        var treeIterator = new SkillTreeIterator(group);
        while (treeIterator.MoveNext())
        {
            var skill = treeIterator.GetCurrent();
            Debug.Log(skill.Name);
        }       
    }

    private void SkillTreeBypassV2(SkillGroup group)
    {
        var treeIterator = new SkillTreeIterator(group);
        var ints = new int[9];
        int iterator = 9;
        while (treeIterator.MoveNext())
        {
            var skill = treeIterator.GetCurrent();

            if (skill.Name.Contains("Skill") && iterator >= 0 && iterator <= 9)
            {
                Debug.Log(skill.Name);
                _skillVertexes[iterator].GetComponent<SkillVertex>().CostIndicatorText.text = skill.Cost.ToString() + " points";
                iterator--;
            }
        }
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
    }

    private static int GetCountNumberOfSkills(SkillGroup group)
    {
        int count = 0;
        var treeIterator = new SkillTreeIterator(group);
        while (treeIterator.MoveNext())
        {
            count++;
        }
        return count;
    }
}
