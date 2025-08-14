using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Actor : MonoBehaviour
{
    [SerializeField] protected Dictionary<Collider, Actor> target = new Dictionary<Collider, Actor>();

    [SerializeField] protected float HP;
    [SerializeField] protected float MP;


    public void SetMySkill(Skill skillInfo)
    {
        Skill skill = SkillDataInfo.Instance.skillDic[skillInfo.info.skillIndex];
        skill.SetSkillInfo(skillInfo.info);
        skill.SetEffectInfo();
    }

    public void ApplySkill(Actor target,Skill Skill)
    {
        Skill.ApplySkill(this,target);
    }
    public void ConsumeMP(float cost)
    {
        MP -= cost;
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
    }


    public Dictionary<Collider, Actor> GetTarget()
    {
        return target;
    }
}
