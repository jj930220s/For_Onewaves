using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isHit = false;

    // 타겟
    protected Actor actor;

    // 실제 명중한 대상
    protected Actor hitActor;

    public void SetTarget(Actor actor)
    {
        this.actor = actor;
    }

    public Actor GetTarget()
    {
        return this.hitActor;
    }

}
