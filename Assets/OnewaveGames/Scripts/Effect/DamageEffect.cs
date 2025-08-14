using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : Effect
{
    private float damage;

    public DamageEffect(float damage, TimeType type)
    {
        this.damage = damage;
        this.type = type;

    }

    public override async UniTask<bool> Apply(Actor source, Actor target)
    {
        target.TakeDamage(damage);
        return true;
    }
}
