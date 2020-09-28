using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AnimationState updates the character during the animation.
/// </summary>
public class AnimationState : StateMachineBehaviour
{
    /// <summary>
    /// The current CharacterController of the state.
    /// </summary>
    private CharacterController controller;

    /// <summary>
    /// stateDatas are the StateData to use in the state.
    /// </summary>
    public List<StateData> stateDatas;

    private void EnterAll(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        foreach (StateData stateData in stateDatas)
        {
            stateData.OnEnter(controller, animator, stateInfo);
        }
    }

    private void UpdateAll(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        foreach (StateData stateData in stateDatas)
        {
            stateData.UpdateAbility(controller, animator, stateInfo);
        }
    }

    private void ExitAll(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        foreach (StateData stateData in stateDatas)
        {
            stateData.OnExit(controller, animator, stateInfo);
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponentInParent<CharacterController>();
        EnterAll(controller, animator, stateInfo);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateAll(controller, animator, stateInfo);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ExitAll(controller, animator, stateInfo);
    }
}
