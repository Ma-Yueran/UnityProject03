using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/DamagedRecover")]
public class DamagedRecover : StateData
{
    public override void OnEnter(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        controller.SetState(State.IDLE);
    }
}
