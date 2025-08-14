using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable][CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Object/New SkillData")]
public class SkillData : ScriptableObject
{

    // ��� ��ų ����Ʈ(�ν����Ϳ��� ����)
    [SerializeField] private List<SkillInfo> skillInfo=new List<SkillInfo>();


    // ��ų���� ��ųʸ��� ����
    public void SetSkillData()
    {
        foreach(var skill in skillInfo)
        {
            SkillDataInfo.Instance.skillData[skill.skillIndex] = skill;
        }

        foreach(var skill in skillInfo)
        {
            if (string.IsNullOrEmpty(skill.skillName))
            {
                return;
            }
            string name = string.Concat("Skill", skill.skillName);

            Type skillComponent = Type.GetType(name);

            if (!typeof(Skill).IsAssignableFrom(skillComponent))
            {
                Debug.LogWarning("�������� ���� �̸� "+ name);
                return;
            }

            SkillDataInfo.Instance.skillDic[skill.skillIndex] = Activator.CreateInstance(skillComponent, skill) as Skill;
        }
    }
}

// �� ��ų�� �ʿ��� ������ ���� �� ����ȭ
[Serializable]
public class SkillInfo
{
    public int skillIndex;
    public string skillName;
    public float range;
    public float damage;
    public float speed;
    public float coolTime;
    public float cost;
    public GameObject prefab;
    public Skill skill;
    [SerializeField]public List<int> effectList;
}
