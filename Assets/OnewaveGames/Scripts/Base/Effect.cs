using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 효과 종류
public enum EffectType
{
    COST=1,
    FIRE,
    DAMAGE,
    CC,
    PULL
}

// 동시 실행 여부 판단
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
