using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/ActivateWeapon")]
public class ActivateWeapon : StateData
{
    public float attackPower;

    public override void OnEnter(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        controller.SetWeaponActive(true, attackPower);
    }
}
