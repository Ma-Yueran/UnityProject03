using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/AddForce")]
public class AddForce : StateData
{
    public bool onEnter;

    public bool onExit;

    public Vector3 force;

    public bool isRelative;

    public override void OnEnter(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (onEnter)
        {
            controller.AddForce(force, isRelative);
        }
    }

    public override void OnExit(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (onExit)
        {
            controller.AddForce(force, isRelative);
        }
    }
}
