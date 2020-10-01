using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/DodgeMovement")]
public class DodgeMovement : StateData
{
    public float speedForward;

    public float speedBackward;

    public float speedLeft;

    public float speedRight;

    public float speedMiddleV;

    public float speedMiddleH;

    public AnimationCurve speedGraph;

    public override void OnEnter(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        controller.GetAnimationProgress().dodgeDirection = controller.GetDodgeDirection();
    }

    public override void UpdateAbility(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        float speedMap = speedGraph.Evaluate(stateInfo.normalizedTime);

        Direction direction = controller.GetAnimationProgress().dodgeDirection;

        switch (direction)
        {
            case Direction.FORWARD:
                controller.MoveVertical(speedForward * speedMap, 0, false);
                return;
            case Direction.BACKWARD:
                controller.MoveVertical(-speedBackward * speedMap, 0, false);
                return;
            case Direction.LEFT:
                controller.MoveHorizontal(-speedLeft * speedMap, 0, false);
                return;
            case Direction.RIGHT:
                controller.MoveHorizontal(speedRight * speedMap, 0, false);
                return;
            case Direction.MIDDLE:
                controller.MoveHorizontal(speedMiddleH * speedMap, 0, false);
                controller.MoveVertical(speedMiddleV * speedMap, 0, false);
                return;
            default:
                return;
        }
    }
}
