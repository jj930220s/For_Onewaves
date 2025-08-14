using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillRocketGrab : Skill
{

    public SkillRocketGrab(SkillInfo info) : base(info)
    {
    }

    public override async UniTask<bool> ApplySkill(Actor source, Actor target)
    {
        CoolTimeCheck();
        return await ActiveSkill(source, target);
         
    }

    private async void CoolTimeCheck()
    {
        isCoolTime = true;
        await UniTask.Delay(TimeSpan.FromSeconds(info.coolTime));
        isCoolTime = false;
    }

}
