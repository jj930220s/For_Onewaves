using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    COST=1,
    FIRE,
    DAMAGE,
    CC,
    PULL
}

public enum TimeType
{ 
    together=1,
    sequent
}

[Serializable]
public abstract class Effect
{
    public TimeType type;


    public abstract UniTask<bool> Apply(Actor source, Actor target);
}
