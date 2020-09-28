using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/StateTransition")]
public class StateTransition : StateData
{
    public float transitionTime;

    public override void UpdateAbility(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (stateInfo.normalizedTime > transitionTime)
        {
            controller.SetTransit(true);
        }
    }

    public override void OnExit(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        controller.SetTransit(false);
    }
}
