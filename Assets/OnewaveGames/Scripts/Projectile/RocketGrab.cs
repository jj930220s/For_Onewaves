using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGrab : Projectile
{

    private void OnTriggerEnter(Collider other)
    {
        if (actor.GetTarget().ContainsKey(other))
        {
            isHit = true;
            hitActor = actor.GetTarget().GetValueOrDefault(other);
        }
    }

}
