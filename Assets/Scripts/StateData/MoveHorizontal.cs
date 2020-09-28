using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/MoveHorizontal")]
public class MoveHorizontal : StateData
{
    public float speedForward;
    public float speedBackward;
    public AnimationCurve speedGraph;
    public bool controllable;

    public override void UpdateAbility(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        float speedMapper = speedGraph.Evaluate(stateInfo.normalizedTime);
        controller.MoveHorizontal(speedForward * speedMapper, speedBackward * speedMapper, controllable);
    }
}
