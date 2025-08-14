using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataInfo : MonoSingleton<SkillDataInfo>
{
    // 본래 DataManager 등의 역할이지만, 이 과제의 경우 스킬만 사용하기에 스크립트 이름을 SkillDataInfo로 설정


    [Header("Skill Data Info")]
    [SerializeField] private SkillData skillDataList;

    public Dictionary<int, SkillInfo> skillData = new Dictionary<int, SkillInfo>();

    public Dictionary<int, Skill> skillDic = new Dictionary<int, Skill>();

    protected override void Awake()
    {
        base.Awake();
        skillDataList.SetSkillData();



    }



}
