using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Actor
{
    // �ν����Ϳ��� ��ų �ѹ��� �Է��ϸ�, �ΰ��ӿ��� ��ų �����͸� ������ ��
    [SerializeField] private List<int> mySkillIndex = new List<int>();
    [SerializeField] private Dictionary<int,Skill> mySkillDic = new Dictionary<int, Skill>();

    [SerializeField] private CapsuleCollider enemy;
    // �׽�Ʈ�� ��ü
    [SerializeField] private CapsuleCollider enemy2;

    private Skill nowSkill;

    private async void Start()
    {
        // �ش� ĳ������ ��ų�� ȹ��
        for (int i = 0; i < mySkillIndex.Count; i++)
        {
            mySkillDic.Add(i, SkillDataInfo.Instance.skillDic[mySkillIndex[i]]);
        }



        SetSkillInfo();

        target.Add(enemy, enemy.GetComponentInParent<Actor>());
        target.Add(enemy2, enemy2.GetComponentInParent<Actor>());

        await WaitSkillKey();
    }

    private void SetSkillInfo()
    {
        foreach(var skill in mySkillDic)
        {
            SetMySkill(skill.Value);
        }
    }

    private async UniTask WaitSkillKey()
    {
        // ��ư�� ���������� ���
        while (true)
        {
            await UniTask.WaitUntil(() => Keyboard.current.qKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame);

            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                TrySkill(mySkillDic[0]);
            }
            else
            {
                TrySkill(mySkillDic[1]);
            }
        }
    }

    /// <summary>
    /// ���� ��ų�� 1�����̱� ������ ���ǻ� ����ȭ �Ͽ����ϴ�
    /// </summary>
    private void TrySkill(Skill skill) 
    {
        nowSkill = skill;

        if (!CanSkillUse(nowSkill))
        {
            return;
        }

        ApplySkill(target[enemy],nowSkill);
    }

    private bool CanSkillUse(Skill skill)
    {
        // ���� �����ϴ���, ���� ����ִ��� ���� �˻�
        if (target == null)
        {
            return false;
        }
        else if (HP < 0)
        {
            return false;
        }
        // �ڽ�Ʈ �˻�
        else if (MP < skill.info.cost)
        {
            return false;
        }
        // TODO : ��Ÿ�� �˻�
        else if(skill.isCoolTime)
        {
            return false;
        }



        return true;
    }




}
