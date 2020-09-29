using UnityEngine;

/// <summary>
/// StateData updates the given character during the animation.
/// </summary>
public class StateData : ScriptableObject
{
    public virtual void OnEnter(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public virtual void UpdateAbility(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public virtual void OnExit(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
