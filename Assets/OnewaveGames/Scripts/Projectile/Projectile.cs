using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isHit = false;

    // Ÿ��
    protected Actor actor;

    // ���� ������ ���
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
