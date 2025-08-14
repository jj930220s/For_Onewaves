using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataInfo : MonoSingleton<SkillDataInfo>
{
    // ���� DataManager ���� ����������, �� ������ ��� ��ų�� ����ϱ⿡ ��ũ��Ʈ �̸��� SkillDataInfo�� ����


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
