using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/TakeNoDamage")]
public class TakeNoDamage : StateData
{
    public State nextState;

    public override void OnExit(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        controller.SetState(nextState);
    }
}
