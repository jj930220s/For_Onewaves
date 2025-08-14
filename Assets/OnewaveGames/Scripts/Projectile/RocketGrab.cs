using System.Collections.Generic;
using UnityEngine;

public class RocketGrab : Projectile
{

    private void OnTriggerEnter(Collider other)
    {
        // 충돌시 GetComponent 사용 방지를 위해 미리 딕셔너리를 설정
        if (actor.GetTarget().ContainsKey(other))
        {
            isHit = true;
            hitActor = actor.GetTarget().GetValueOrDefault(other);
        }
    }

}
