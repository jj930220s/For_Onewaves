using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCEffect : Effect
{

    public CCEffect(TimeType type)
    {
        this.type = type;
    }
    public override UniTask<bool> Apply(Actor source, Actor target)
    {
        throw new System.NotImplementedException();
    }

 
}
