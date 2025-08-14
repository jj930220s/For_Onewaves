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


    // 스킬에 사용되는 효과들을 리스트에 추가, 순차적/ 세팅에 따라 비동기적 동작 예정
    protected void AddEffectByType(int type)
    {
        switch (type)
        {
            case (int)EffectType.COST:  // 마나 소모
                costEffect = new CostEffect(info.cost, TimeType.together);
                AddEffectList(costEffect);
                break;
            case (int)EffectType.FIRE:  // 투사체 발사
                fireEffect = new FireEffect(info.prefab, info.range, info.speed, TimeType.sequent);
                AddEffectList(fireEffect);
                break;
            case (int)EffectType.DAMAGE:    // 데미지 적용
                damageEffect = new DamageEffect(info.damage, TimeType.together);
                AddEffectList(damageEffect);
                break;
            case (int)EffectType.CC:        // (필요시)CC 적용
                ccEffect = new CCEffect(TimeType.together);
                AddEffectList(ccEffect);
                break;
            case (int)EffectType.PULL:      // 당겨오기
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
            // 동시에 실행되는 경우(cost와 fire는 동시 실행)
            if (effect.type == TimeType.together)
            {
                effect.Apply(source, target);
            }
            else
            {
                //실행 대기하는 경우(fire 실행 후 명중까지 대기하고 damage 실행)
                isConnect=await effect.Apply(source, target);
            }

            // 효과 연쇄가 끊긴 경우 효과 중단 ex)fire 후 명중하지 못한 경우
            if(!isConnect)
            {
                return isConnect;
            }
        }
        return isConnect;
    }


    // 재사용 대시시간
    protected async void CoolTimeCheck()
    {
        isCoolTime = true;
        await UniTask.Delay(TimeSpan.FromSeconds(info.coolTime));
        isCoolTime = false;
    }

}
