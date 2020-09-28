using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/SetWeapon")]
public class SetWeapon : StateData
{
    [Header("Enter null to set the weapon to null")]
    public string weapon;

    public override void OnEnter(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        controller.SetWeapon(weapon);
    }
}
