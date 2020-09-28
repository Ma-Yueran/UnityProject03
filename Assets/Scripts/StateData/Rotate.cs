using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/Rotate")]
public class Rotate : StateData
{
    public float speed;
    public bool controllable;

    public override void UpdateAbility(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        controller.Rotate(speed, controllable);
    }
}
