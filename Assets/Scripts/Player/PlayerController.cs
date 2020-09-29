using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The controller for the player's character.
/// </summary>
public class PlayerController : CharacterController
{
    public Camera playerCamera;

    private PlayerState playerState;

    private PlayerMovement playerMovement;

    private PlayerInventory playerInventory;

    private PlayerBody playerBody;

    private Animator playerAnimator;

    private Rigidbody playerRigidBody;

    private AnimationProgress playerAnimationProgess;

    private void Start()
    {
        playerState = GetComponent<PlayerState>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInventory = GetComponent<PlayerInventory>();
        playerBody = GetComponent<PlayerBody>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
        playerAnimationProgess = new AnimationProgress();
    }

    private void Update()
    {
        playerAnimator.SetInteger("State", playerState.getCurrentState());
        playerAnimator.SetInteger("WeaponState", playerState.getWeaponState());
        playerAnimator.SetFloat("InputH", playerState.inputH);
        playerAnimator.SetFloat("InputV", playerState.inputV);
        playerAnimator.SetBool("Run", playerState.run);
        playerAnimator.SetBool("Attack", playerState.attack);
        playerAnimator.SetFloat("HP", playerState.currentHP);
    }

    public override void MoveHorizontal(float speedForward, float speedBackward, bool controllable)
    {
        playerMovement.MoveHorizontal(speedForward, speedBackward, controllable);
    }

    public override void MoveVertical(float speedForward, float speedBackward, bool controllable)
    {
        playerMovement.MoveVertical(speedForward, speedBackward, controllable);
    }

    public override void Rotate(float speed, bool controllable)
    {
        playerMovement.Rotate(speed, controllable);
    }

    public override void Equip(string itemName, string bodyPartName, Vector3 localPosition, Vector3 localRotation)
    {
        Item item = playerInventory.GetItem(itemName);
        Transform bodyPart = playerBody.GetBodyPart(bodyPartName);

        item.transform.SetParent(bodyPart);
        item.transform.localPosition = localPosition;
        item.transform.localEulerAngles = localRotation;
    }

    public override void SetTransit(bool value)
    {
        playerAnimator.SetBool("Transit", value);
    }

    public override void SetWeapon(string weaponName)
    {
        if (weaponName.Equals("null"))
        {
            playerInventory.currentWeapon = null;
        }
        else
        {
            playerInventory.SetWeapon(weaponName);
        }
    }

    public override void SetWeaponActive(bool isActive, float attackPower)
    {
        if (playerInventory.currentWeapon != null)
        {
            playerInventory.currentWeapon.isActive = isActive;
            playerInventory.currentWeapon.attackPower = attackPower;
        }
    }

    public override void SetState(State state)
    {
        playerState.currentState = state;
    }

    public override void TakeDamage(float angle, float power)
    {
        SetState(State.DAMAGED);

        Vector3 hit = new Vector3(-Mathf.Tan(angle * Mathf.Deg2Rad), 0, 1).normalized * power;
        if (Mathf.Abs(angle) > 90)
        {
            hit *= -1;
        }

        playerAnimator.SetFloat("HitH", hit.x);
        playerAnimator.SetFloat("HitV", hit.z);

        if (playerState.CanBlock(angle, power))
        {
            playerAnimator.SetBool("Block", true);
        }
        else
        {
            playerState.currentHP -= hit.sqrMagnitude;

            playerAnimator.SetBool("Block", false);
        }

        AddForce(-hit, true);
    }

    public override void AddForce(Vector3 force, bool isRelative)
    {
        if (!isRelative)
        {
            playerRigidBody.AddForce(force, ForceMode.Impulse);
        } 
        else
        {
            playerRigidBody.AddRelativeForce(force, ForceMode.Impulse);
        }
    }

    public override AnimationProgress GetAnimationProgress()
    {
        return playerAnimationProgess;
    }

    public override void TurnOffPhysics()
    {
        playerRigidBody.isKinematic = true;
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
