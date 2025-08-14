using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PullEffect : Effect
{
    private FireEffect prev;

    private Actor hitActor;

    private Vector3 endPos;

    private float speed;
    private float elapsed;
    private float duration;
    public PullEffect(FireEffect prevEffect,float speed,TimeType type)
    {
        // ���� ����� �޾ƿ��� ���� fireEffect �޾ƿ���
        this.prev = prevEffect;
        this.speed = speed;
        this.type = type;
    }

    public override async UniTask<bool> Apply(Actor source, Actor target)
    {
        // �߻� ����� �ƴ�, ������ ���
        hitActor= prev.GetHitTarget();


        // �����Լ� ��� �������� 1�Ÿ��� ���� ����;
        Vector3 startPos = hitActor.transform.position;
        Vector3 dir = (source.transform.position - hitActor.transform.position).normalized;
        endPos = source.transform.position - dir * 1f; ;

        float distance=Vector3.Distance(startPos,endPos);

        elapsed = 0;
        duration = distance / speed;

        while(elapsed<duration)
        {
            elapsed += Time.deltaTime;
            float time=elapsed/duration;
            hitActor.transform.position = Vector3.Lerp(startPos, endPos, time);
            await UniTask.Yield();

        }

        return true;
    }
}
