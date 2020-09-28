using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/MoveVertical")]
public class MoveVertical : StateData
{
    public float speedForward;
    public float speedBackward;
    public AnimationCurve speedGraph;
    public bool controllable;

    public override void UpdateAbility(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        float speedMapper = speedGraph.Evaluate(stateInfo.normalizedTime);
        controller.MoveVertical(speedForward * speedMapper, speedBackward * speedMapper, controllable);
    }
}
