using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Actor
{
    // 인스펙터에서 스킬 넘버를 입력하면, 인게임에서 스킬 데이터를 가지고 옴
    [SerializeField] private List<int> mySkillIndex = new List<int>();
    [SerializeField] private Dictionary<int,Skill> mySkillDic = new Dictionary<int, Skill>();

    [SerializeField] private CapsuleCollider enemy;
    // 테스트용 개체
    [SerializeField] private CapsuleCollider enemy2;

    private Skill nowSkill;

    private async void Start()
    {
        // 해당 캐릭터의 스킬들 획득
        for (int i = 0; i < mySkillIndex.Count; i++)
        {
            mySkillDic.Add(i, SkillDataInfo.Instance.skillDic[mySkillIndex[i]]);
        }



        SetSkillInfo();

        // 스킬 명중 테스트용 적군 설정
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
        // 버튼이 눌릴때까지 대기 
        while (true)
        {
            await UniTask.WaitUntil(() => Keyboard.current.qKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame);

            // 눌린 버튼에 따라 다른 스킬 사용
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
    /// 현재 스킬이 1종류이기 때문에 편의상 간략화 하였습니다
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
        // 적이 존재하는지, 내가 살아있는지 등의 검사
        if (target == null)
        {
            return false;
        }
        else if (HP < 0)
        {
            return false;
        }
        // 코스트 검사
        else if (MP < skill.info.cost)
        {
            return false;
        }
        // TODO : 쿨타임 검사
        else if(skill.isCoolTime)
        {
            return false;
        }



        return true;
    }




}
