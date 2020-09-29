using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerBody))]
public class PlayerController : CharacterController
{
    public Camera playerCamera;

    private PlayerState playerState;

    private PlayerMovement playerMovement;

    private PlayerInventory playerInventory;

    private PlayerBody playerBody;

    private void Start()
    {
        InitializeController();

        playerState = GetComponent<PlayerState>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInventory = GetComponent<PlayerInventory>();
        playerBody = GetComponent<PlayerBody>();
    }

    private void Update()
    {
        characterAnimator.SetInteger("State", playerState.getCurrentState());
        characterAnimator.SetInteger("WeaponState", playerState.getWeaponState());
        characterAnimator.SetFloat("InputH", playerState.inputH);
        characterAnimator.SetFloat("InputV", playerState.inputV);
        characterAnimator.SetBool("Run", playerState.run);
        characterAnimator.SetBool("Attack", playerState.attack);
        characterAnimator.SetFloat("HP", playerState.currentHP);

        ControlVelocity();
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

        characterAnimator.SetFloat("HitH", hit.x);
        characterAnimator.SetFloat("HitV", hit.z);

        if (playerState.CanBlock(angle, power))
        {
            characterAnimator.SetBool("Block", true);
        }
        else
        {
            playerState.currentHP -= hit.sqrMagnitude;

            characterAnimator.SetBool("Block", false);
        }

        AddForce(-hit, true);

        if (playerInventory.currentWeapon != null)
        {
            playerInventory.currentWeapon.isActive = false;
        }
    }
}
