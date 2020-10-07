using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager instance;

    private void Awake()
    {
        instance = this;
    }

    public List<AttackInfo> attackInfos;

    private AttackManager()
    {
        attackInfos = new List<AttackInfo>();
    }

    public void AddAttackInfo(AttackInfo attackInfo)
    {
        attackInfos.Add(attackInfo);
    }

    public void RemoveAttackInfos(List<AttackInfo> attackInfos)
    {
        foreach (AttackInfo attackInfo in attackInfos)
        {
            if (this.attackInfos.Contains(attackInfo))
            {
                this.attackInfos.Remove(attackInfo);
            }
        }
    }

    public AttackInfo GetAttackInfo(Collider attackCollider)
    {
        foreach (AttackInfo attackInfo in attackInfos)
        {
            if (attackInfo.isHit)
            {
                continue;
            }

            if (attackInfo.attackCollider.Equals(attackCollider))
            {
                return attackInfo;
            }
        }

        return null;
    }
}
