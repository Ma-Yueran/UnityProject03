using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyState))]
[RequireComponent(typeof(EnemyBody))]
[RequireComponent(typeof(EnemyInventory))]
[RequireComponent(typeof(EnemyAI))]
public class EnemyController : CharacterController
{
    private EnemyMovement enemyMovement;

    private EnemyBody enemyBody;

    private EnemyAI enemyAI;

    private void Start()
    {
        InitializeController();

        enemyMovement = GetComponent<EnemyMovement>();
        characterState = GetComponent<EnemyState>();
        enemyBody = GetComponent<EnemyBody>();
        characterInventory = GetComponent<EnemyInventory>();
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        characterAnimator.SetInteger("State", characterState.getCurrentState());
        characterAnimator.SetInteger("WeaponState", characterState.getWeaponState());
        characterAnimator.SetFloat("InputH", characterState.inputH);
        characterAnimator.SetFloat("InputV", characterState.inputV);
        characterAnimator.SetBool("Run", characterState.run);
        characterAnimator.SetBool("Attack", characterState.attack);
        characterAnimator.SetFloat("HP", characterState.currentHP);

        ControlVelocity();
    }

    public override void MoveHorizontal(float speedForward, float speedBackward, bool controllable)
    {
        if (controllable)
        {
            enemyAI.MoveHorizontal(speedForward, speedBackward);
        }
        else
        {
            enemyMovement.MoveHorizontal(speedForward);
        }
    }

    public override void MoveVertical(float speedForward, float speedBackward, bool controllable)
    {
        if (controllable)
        {
            enemyAI.MoveVertical(speedForward, speedBackward);
        }
        else
        {
            enemyMovement.MoveVertical(speedForward);
        }
    }

    public override void Rotate(float speed, bool controllable)
    {
        if (controllable)
        {
            enemyAI.Rotate();
        }
        else
        {
            enemyMovement.Rotate(speed);
        }
    }

    public override void Equip(string itemName, string bodyPartName, Vector3 localPosition, Vector3 localRotation)
    {
        Item item = characterInventory.GetItem(itemName);
        Transform bodyPart = enemyBody.GetBodyPart(bodyPartName);

        item.transform.SetParent(bodyPart);
        item.transform.localPosition = localPosition;
        item.transform.localEulerAngles = localRotation;
    }

    public override void SetState(State state)
    {
        characterState.currentState = state;
    }

    public override Direction GetDodgeDirection()
    {
        throw new System.NotImplementedException();
    }

    public override void GetTarget()
    {
        throw new System.NotImplementedException();
    }

    public override void FollowTarget(float speed, float minDistance, float detectDistance)
    {
        throw new System.NotImplementedException();
    }
}
