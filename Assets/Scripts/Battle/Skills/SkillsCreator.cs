using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class SkillsCreator : MonoBehaviour
{
    [SerializeField] private GameObject _skillPrefub;
    [SerializeField] private Transform _skillCreatePosition;
    [SerializeField] BattleCharacter _ownerCharacter;
    [SerializeField] BattleCharacter _enemyCharacter;
    public Skill[] CreateSkills(SkillScriptableObject[] skills, BattleCharacter owner, BattleCharacter enemy)
    {
        List<Skill> createdSkills = new List<Skill>();
        for (int i = 0; i < skills.Length; i++)
        {
            DeleteCurrentSkills(owner);
            Skill skill = null;
            if (_skillCreatePosition == null)
            {
                skill = owner.gameObject.AddComponent<Skill>();
            }
            else
            {
                skill = Instantiate(_skillPrefub, _skillCreatePosition).GetComponent<Skill>();
            }

            if (skill != null)
            {
                skill.Set(skills[i], _ownerCharacter, _enemyCharacter);
                createdSkills.Add(skill);
            }
        }
        return createdSkills.ToArray();
    }
    private void DeleteCurrentSkills(BattleCharacter character)
    {
        Skill[] ownerSkills = character.GetComponents<Skill>();
        for (int i = 0; i < ownerSkills.Length; i++)
        {
            ownerSkills[i].Remove();
        }
    }
}
