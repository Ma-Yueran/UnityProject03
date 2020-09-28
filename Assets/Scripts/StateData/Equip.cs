using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/Equip")]
public class Equip : StateData
{
    public string itemName;
    public string bodyPart;
    public Vector3 localPosition;
    public Vector3 localRoatation;

    public override void OnEnter(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        controller.Equip(itemName, bodyPart, localPosition, localRoatation);
    }
}
