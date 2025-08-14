using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    private List<Effect> EffectList {get;} = new();

    public SkillInfo info;
    public bool isCoolTime;


    protected CostEffect costEffect;
    protected FireEffect fireEffect;
    protected DamageEffect damageEffect;
    protected CCEffect ccEffect;
    protected PullEffect pullEffect;

    public Skill(SkillInfo info) { this.info = info; }
  
    public void SetSkillInfo(SkillInfo info)
    {
        this.info = info;
    }

    public void SetEffectInfo()
    {
        if (IsAlreadyEffect() == true)
        {
            return;
        }

        for (int i = 0; i < info.effectList.Count; i++)
        {
            AddEffectByType(info.effectList[i]);
        }

    }

    public abstract  UniTask<bool> ApplySkill(Actor source, Actor target);

    protected void AddEffectList(Effect effect)
    {
        EffectList.Add(effect);
    }

    protected void AddEffectByType(int type)
    {
        switch (type)
        {
            case (int)EffectType.COST:
                costEffect = new CostEffect(info.cost, TimeType.together);
                AddEffectList(costEffect);
                break;
            case (int)EffectType.FIRE:
                fireEffect = new FireEffect(info.prefab, info.range, info.speed, TimeType.sequent);
                AddEffectList(fireEffect);
                break;
            case (int)EffectType.DAMAGE:
                damageEffect = new DamageEffect(info.damage, TimeType.together);
                AddEffectList(damageEffect);
                break;
            case (int)EffectType.CC:
                ccEffect = new CCEffect(TimeType.together);
                AddEffectList(ccEffect);
                break;
            case (int)EffectType.PULL:
                pullEffect = new PullEffect(fireEffect, info.speed, TimeType.together);
                AddEffectList(pullEffect);
                break;
            default:
                Debug.Log("없는 케이스");
                break;
        };
    }

protected bool IsAlreadyEffect()
    {
        return EffectList.Count != 0;
    }

    protected async UniTask<bool> ActiveSkill(Actor source, Actor target)
    {
        bool isConnect = true;
        foreach (var effect in EffectList)
        {
            if (effect.type == TimeType.together)
            {
                effect.Apply(source, target);
            }
            else
            {
                isConnect=await effect.Apply(source, target);
            }

            if(!isConnect)
            {
                return isConnect;
            }
        }
        return isConnect;
    }
}
