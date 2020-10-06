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

    private EnemyState enemyState;

    private EnemyBody enemyBody;

    private EnemyInventory enemyInventory;

    private EnemyAI enemyAI;

    private void Start()
    {
        InitializeController();

        enemyMovement = GetComponent<EnemyMovement>();
        enemyState = GetComponent<EnemyState>();
        enemyBody = GetComponent<EnemyBody>();
        enemyInventory = GetComponent<EnemyInventory>();
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        characterAnimator.SetInteger("State", enemyState.getCurrentState());
        characterAnimator.SetInteger("WeaponState", enemyState.getWeaponState());
        characterAnimator.SetFloat("InputH", enemyState.inputH);
        characterAnimator.SetFloat("InputV", enemyState.inputV);
        characterAnimator.SetBool("Run", enemyState.run);
        characterAnimator.SetBool("Attack", enemyState.attack);
        characterAnimator.SetFloat("HP", enemyState.currentHP);

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
        Item item = enemyInventory.GetItem(itemName);
        Transform bodyPart = enemyBody.GetBodyPart(bodyPartName);

        item.transform.SetParent(bodyPart);
        item.transform.localPosition = localPosition;
        item.transform.localEulerAngles = localRotation;
    }

    public override void SetState(State state)
    {
        enemyState.currentState = state;
    }
    
    public override void SetWeapon(string weaponName)
    {
        if (weaponName.Equals("null"))
        {
            enemyInventory.currentWeapon = null;
        }
        else
        {
            enemyInventory.SetWeapon(weaponName);
        }
    }

    public override void SetWeaponActive(bool isActive, float attackPower)
    {
        if (enemyInventory.currentWeapon != null)
        {
            enemyInventory.currentWeapon.isActive = isActive;
            enemyInventory.currentWeapon.attackPower = attackPower;
        }
    }

    public override void TakeDamage(float angle, float power)
    {
        SetState(State.DAMAGED);

        Vector3 hit = new Vector3(-Mathf.Tan(angle * Mathf.Deg2Rad), 0, 1).normalized * power;
        if (Mathf.Abs(angle) > 90)
        {
            hit *= -1;
        }

        characterAnimator.SetFloat("HitH", hit.x);
        characterAnimator.SetFloat("HitV", hit.z);

        if (enemyState.CanBlock(angle, power))
        {
            characterAnimator.SetBool("Block", true);
        }
        else
        {
            enemyState.currentHP -= hit.sqrMagnitude;

            characterAnimator.SetBool("Block", false);
        }

        AddForce(-hit, true);

        SetDamageDirection(angle);
        SetDeathDirection(angle);

        if (enemyInventory.currentWeapon != null)
        {
            enemyInventory.currentWeapon.isActive = false;
            GetAnimationProgress().setAttackActive = true;
        }
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
