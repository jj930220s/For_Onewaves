using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostEffect : Effect
{
    private float cost;

    public CostEffect(float cost, TimeType type)
    {
        this.cost = cost;
        this.type = type;

    }

    public override async UniTask<bool> Apply(Actor source, Actor target)
    {
        source.ConsumeMP(cost);
        return true;
    }
}
