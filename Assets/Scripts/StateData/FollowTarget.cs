using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/FollowTarget")]
public class FollowTarget : StateData
{
    public float speed;

    public AnimationCurve speedGraph;

    public float minDistance;

    public float detectDistance;

    public override void OnEnter(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        controller.GetTarget();
    }

    public override void UpdateAbility(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        float speedMap = speedGraph.Evaluate(stateInfo.normalizedTime);

        controller.FollowTarget(speed * speedMap, minDistance, detectDistance);
    }
}
