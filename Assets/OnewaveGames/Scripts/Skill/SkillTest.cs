using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTest : Skill
{
    // 스킬 저장 및 사용 테스트용 클래스.
    public SkillTest(SkillInfo info) : base(info)
    {
    }

    public override async UniTask<bool> ApplySkill(Actor source, Actor target)
    {
        CoolTimeCheck();
        return await ActiveSkill(source, target);
    }



}
