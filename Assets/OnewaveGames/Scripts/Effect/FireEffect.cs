using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : Effect
{
    private GameObject projectileObj;
    private GameObject projectile;
    private Projectile projectileComponent;
    private float range;
    private float speed;

    private float duration;
    private float elapsed;

    private bool isMoving;

    private Actor hitActor;
        
    public FireEffect(GameObject projectile,float range,float speed,TimeType type)
    {
        this.projectile = projectile;
        this.type = type;
        this.range = range;
        this.speed = speed;
    }


    public override async UniTask<bool> Apply(Actor source, Actor target)
    {
        // 투사체 생성
        projectileObj = Object.Instantiate(projectile);
        projectileObj.transform.position=source.transform.position;

        projectileComponent = projectileObj.GetComponent<Projectile>() ?? projectileObj.AddComponent<Projectile>();
        projectileComponent.SetTarget(source);

        return await ProjectileMove();

    }


    private async UniTask<bool> ProjectileMove()
    {
        isMoving = true;

        duration = range / speed;
        elapsed = 0;

        while (isMoving)
        {
            elapsed += Time.deltaTime;
            projectileObj.transform.position += Vector3.forward * speed * Time.deltaTime;

            await UniTask.Yield();

            if(elapsed > duration)
            {
                isMoving = false;
                // 이후 오브젝트풀에 쓸 여지 있으므로 파괴하지 않음
                projectileObj.gameObject.SetActive(false);
            }
            if(projectileComponent.isHit)
            {
                // 발사 대상이 꼭 맞은 대상이라고 장담할 수 없기 때문에 target이 아닌 hitActor 사용
                hitActor = projectileComponent.GetTarget();
                projectileObj.gameObject.SetActive(false);
                break;
            }
        }

        return projectileComponent.isHit;

    }

    public Actor GetHitTarget()
    {
        return hitActor;
    }
 
}
