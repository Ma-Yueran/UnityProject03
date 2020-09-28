using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new state", menuName = "StateData/ActivateWeapon")]
public class ActivateWeapon : StateData
{
    public float activeTime;

    public float unactiveTime;

    public float attackPower;

    public override void OnEnter(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        AnimationProgress progress = controller.GetAnimationProgress();
        progress.ResetProgress();
    }

    public override void UpdateAbility(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        AnimationProgress progress = controller.GetAnimationProgress();
        
        if (stateInfo.normalizedTime > unactiveTime)
        {
            controller.SetWeaponActive(false, attackPower);
            return;
        }

        if (progress.SetAttackActive)
        {
            return;
        }
        
        if (stateInfo.normalizedTime > activeTime)
        {
            controller.SetWeaponActive(true, attackPower);
            progress.SetAttackActive = true;
        }
    }

    public override void OnExit(CharacterController controller, Animator animator, AnimatorStateInfo stateInfo)
    {
        AnimationProgress progress = controller.GetAnimationProgress();
        progress.ResetProgress();
    }
}
