using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/Attack")]
public class Attack : StateData
{
    public string weaponName;
    public float attackPower;
    public float activeTime;

    public override void OnEnter(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        AttackManager attackManager = AttackManager.instance;
        Collider collider = controller.GetWeapon(weaponName).GetCollider();
        AttackInfo attackInfo = AttackInfoPool.instance.GetAttackInfo(controller, collider, attackPower, activeTime);

        attackManager.AddAttackInfo(attackInfo);
    }
}
