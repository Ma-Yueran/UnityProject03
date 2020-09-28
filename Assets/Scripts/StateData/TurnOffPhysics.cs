using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/TurnOffPhysics")]
public class TurnOffPhysics : StateData
{
    public override void OnEnter(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        controller.TurnOffPhysics();
    }
}
