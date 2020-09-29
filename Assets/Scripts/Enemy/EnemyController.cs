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

    private Animator enemyAnimator;

    private Rigidbody enemyRigidbody;

    private AnimationProgress enemyAnimationProgress;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyState = GetComponent<EnemyState>();
        enemyBody = GetComponent<EnemyBody>();
        enemyInventory = GetComponent<EnemyInventory>();
        enemyAI = GetComponent<EnemyAI>();
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRigidbody = GetComponent<Rigidbody>();
        enemyAnimationProgress = new AnimationProgress();
    }

    private void Update()
    {
        enemyAnimator.SetInteger("State", enemyState.getCurrentState());
        enemyAnimator.SetInteger("WeaponState", enemyState.getWeaponState());
        enemyAnimator.SetFloat("InputH", enemyState.inputH);
        enemyAnimator.SetFloat("InputV", enemyState.inputV);
        enemyAnimator.SetBool("Attack", enemyState.attack);
        enemyAnimator.SetBool("Block", enemyState.block);
        enemyAnimator.SetFloat("HP", enemyState.currentHP);
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

    public override void AddForce(Vector3 force, bool isRelative)
    {
        if (!isRelative)
        {
            enemyRigidbody.AddForce(force, ForceMode.Impulse);
        } else
        {
            enemyRigidbody.AddRelativeForce(force, ForceMode.Impulse);
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

    public override AnimationProgress GetAnimationProgress()
    {
        return enemyAnimationProgress;
    }

    public override void SetState(State state)
    {
        enemyState.currentState = state;
    }

    public override void SetTransit(bool value)
    {
        enemyAnimator.SetBool("Transit", value);
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
            enemyInventory.currentWeapon.isActive = true;
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

        enemyAnimator.SetFloat("HitH", hit.x);
        enemyAnimator.SetFloat("HitV", hit.z);

        if (enemyState.CanBlock(angle, power))
        {
            enemyAnimator.SetBool("Block", true);
        }
        else
        {
            enemyState.currentHP -= hit.sqrMagnitude;

            enemyAnimator.SetBool("Block", false);
        }

        AddForce(-hit, true);
    }

    public override void TurnOffPhysics()
    {
        enemyRigidbody.isKinematic = true;

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
