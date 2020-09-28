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

    /// <summary>
    /// Moves the character horizontally.
    /// </summary>
    /// <param name="speed">the moving speed</param>
    /// <param name="controllable">is the moving controllable by the horizontal input</param>
    public override void MoveHorizontal(float speedForward, float speedBackward, bool controllable)
    {
        playerMovement.MoveHorizontal(speedForward, speedBackward, controllable);
    }

    /// <summary>
    /// Moves the character vertically.
    /// </summary>
    /// <param name="speed">the moving speed</param>
    /// <param name="controllable">is the moving controllable by the vertical input</param>
    public override void MoveVertical(float speedForward, float speedBackward, bool controllable)
    {
        playerMovement.MoveVertical(speedForward, speedBackward, controllable);
    }

    /// <summary>
    /// Rotates the character along y-axis.
    /// </summary>
    /// <param name="speed">the rotation speed</param>
    /// <param name="controllable">is the rotation controllable</param>
    public override void Rotate(float speed, bool controllable)
    {
        playerMovement.Rotate(speed, controllable);
    }

    /// <summary>
    /// Equips the item to the body part.
    /// </summary>
    /// <param name="itemName">the name of the item</param>
    /// <param name="bodyPartName">the name of the body part</param>
    /// <param name="localPosition">the local position of the item</param>
    /// <param name="localRotation">the local rotation of the item</param>
    public override void Equip(string itemName, string bodyPartName, Vector3 localPosition, Vector3 localRotation)
    {
        Item item = playerInventory.GetItem(itemName);
        Transform bodyPart = playerBody.GetBodyPart(bodyPartName);

        item.transform.SetParent(bodyPart);
        item.transform.localPosition = localPosition;
        item.transform.localEulerAngles = localRotation;
    }

    /// <summary>
    /// Sets the Transit value of the animator.
    /// </summary>
    /// <param name="value">the value for Transit</param>
    public override void SetTransit(bool value)
    {
        playerAnimator.SetBool("Transit", value);
    }

    /// <summary>
    /// Sets the current weapon to be the weapon with the given name.
    /// </summary>
    /// <param name="weaponName">the name of the weapon</param>
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

    /// <summary>
    /// Sets the activation of the current weapon.
    /// </summary>
    /// <param name="isActive">is the weapon going to be active</param>
    public override void SetWeaponActive(bool isActive, float attackPower)
    {
        if (playerInventory.currentWeapon != null)
        {
            playerInventory.currentWeapon.isActive = isActive;
            playerInventory.currentWeapon.attackPower = attackPower;
        }
    }

    /// <summary>
    /// Sets the current state of the character.
    /// </summary>
    /// <param name="state">the state</param>
    public override void SetState(State state)
    {
        playerState.currentState = state;
    }

    /// <summary>
    /// Takes damage.
    /// </summary>
    /// <param name="damage">the damage</param>
    public override void TakeDamage(float angle, float power)
    {
        SetState(State.DAMAGED);

        Vector3 damage = new Vector3(-Mathf.Tan(angle * Mathf.Deg2Rad), 0, 1).normalized * power;
        if (Mathf.Abs(angle) > 90)
        {
            damage *= -1;
        }

        playerAnimator.SetFloat("HitH", damage.x);
        playerAnimator.SetFloat("HitV", damage.z);

        if (playerState.CanBlock(angle, power))
        {
            playerAnimator.SetBool("Block", true);
        }
        else
        {
            playerState.currentHP -= damage.sqrMagnitude;

            playerAnimator.SetBool("Block", false);
        }

        AddForce(damage);
    }

    /// <summary>
    /// Adds a force to the character.
    /// </summary>
    /// <param name="force">the force</param>
    public override void AddForce(Vector3 force)
    {
        playerRigidBody.AddForce(force, ForceMode.Impulse);
    }

    /// <summary>
    /// Gets the AnimationProgess object.
    /// </summary>
    /// <returns>the AnimationProgress</returns>
    public override AnimationProgress GetAnimationProgress()
    {
        return playerAnimationProgess;
    }

    /// <summary>
    /// Removes the current force on the character.
    /// </summary>
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
