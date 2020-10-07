using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfoPool : MonoBehaviour
{
    public GameObject prefab;

    public List<AttackInfo> attackInfos;

    public static AttackInfoPool instance;

    private void Awake()
    {
        instance = this;
    }

    public AttackInfo GetAttackInfo(CharacterController attacker, Collider attackCollider, float attackPower, float activeTime)
    {
        foreach (AttackInfo attackInfo in attackInfos)
        {
            if (!attackInfo.gameObject.activeInHierarchy)
            {
                attackInfo.gameObject.SetActive(true);
                attackInfo.isHit = false;
                attackInfo.attacker = attacker;
                attackInfo.attackCollider = attackCollider;
                attackInfo.attackPower = attackPower;
                attackInfo.activeTime = activeTime;

                return attackInfo;
            }
        }

        GameObject newAttackInfoObject = Instantiate(prefab);

        AttackInfo newAttackInfo = newAttackInfoObject.GetComponent<AttackInfo>();
        newAttackInfo.isHit = false;
        newAttackInfo.attacker = attacker;
        newAttackInfo.attackCollider = attackCollider;
        newAttackInfo.attackPower = attackPower;
        newAttackInfo.activeTime = activeTime;

        attackInfos.Add(newAttackInfo);

        return newAttackInfo;
    }
}
