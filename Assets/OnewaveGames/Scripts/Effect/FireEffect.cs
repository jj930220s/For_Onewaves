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
                // ���� ������ƮǮ�� �� ���� �����Ƿ� �ı����� ����
                projectileObj.gameObject.SetActive(false);
            }
            if(projectileComponent.isHit)
            {
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
