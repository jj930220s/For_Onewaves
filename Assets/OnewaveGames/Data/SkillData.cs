using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable][CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Object/New SkillData")]
public class SkillData : ScriptableObject
{

    // 모든 스킬 리스트(인스펙터에서 적용)
    [SerializeField] private List<SkillInfo> skillInfo=new List<SkillInfo>();


    // 스킬들을 딕셔너리에 저장
    public void SetSkillData()
    {
        // 딕셔너리에 SO에 작성된 모든 스킬 클래스 저장, 필요할 때 skillIndex로 꺼내 사용
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
                Debug.LogWarning("적절하지 않은 이름 "+ name);
                return;
            }

            SkillDataInfo.Instance.skillDic[skill.skillIndex] = Activator.CreateInstance(skillComponent, skill) as Skill;
        }
    }
}

// 각 스킬에 필요한 데이터 저장 및 직렬화
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
