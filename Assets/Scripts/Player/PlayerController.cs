using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerBody))]
public class PlayerController : CharacterController
{
    public Camera playerCamera;

    private PlayerMovement playerMovement;

    private PlayerBody playerBody;

    private Transform target;

    private void Start()
    {
        InitializeController();

        characterState = GetComponent<PlayerState>();
        playerMovement = GetComponent<PlayerMovement>();
        characterInventory = GetComponent<PlayerInventory>();
        playerBody = GetComponent<PlayerBody>();
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
        characterAnimator.SetBool("Dodge", characterState.dodge);

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
        Item item = characterInventory.GetItem(itemName);
        Transform bodyPart = playerBody.GetBodyPart(bodyPartName);

        item.transform.SetParent(bodyPart);
        item.transform.localPosition = localPosition;
        item.transform.localEulerAngles = localRotation;
    }

    public override void SetState(State state)
    {
        characterState.currentState = state;
    }

    public override void TakeDamage(AttackInfo attackInfo)
    {
        SetState(State.DAMAGED);

        float angle = CalcDamageAngle(attackInfo);

        Vector3 hit = new Vector3(-Mathf.Tan(angle * Mathf.Deg2Rad), 0, 1).normalized * attackInfo.attackPower;
        if (Mathf.Abs(angle) > 90)
        {
            hit *= -1;
        }

        characterAnimator.SetFloat("HitH", hit.x);
        characterAnimator.SetFloat("HitV", hit.z);

        if (characterState.CanBlock(angle, attackInfo.attackPower))
        {
            characterAnimator.SetBool("Block", true);
        }
        else
        {
            characterState.currentHP -= hit.sqrMagnitude;

            characterAnimator.SetBool("Block", false);
        }

        AddForce(-hit, true);

        SetDamageDirection(angle);
        SetDeathDirection(angle);
    }

    private float CalcDamageAngle(AttackInfo attackInfo)
    {
        Vector3 direction = transform.position - attackInfo.attacker.transform.position;
        direction.y = 0;

        Vector3 directionRight = Vector3.Cross(Vector3.up, direction);

        float angle = Vector3.Angle(transform.forward, direction);

        float sign = Mathf.Sign(Vector3.Dot(transform.forward, directionRight));

        float finalAngle = angle * sign;

        return finalAngle;
    }

    public override Direction GetDodgeDirection()
    {
        if (characterState.inputV > 0)
        {
            return Direction.FORWARD;
        } 
        else if (characterState.inputV < 0)
        {
            return Direction.BACKWARD;
        }
        else if (characterState.inputH < 0)
        {
            return Direction.LEFT;
        }
        else if (characterState.inputH > 0)
        {
            return Direction.RIGHT;
        }
        else
        {
            return Direction.MIDDLE;
        }
    }

    public override void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    public override void FollowTarget(float speed, float minDistance, float detectDistance)
    {
        Vector3 relativePos = target.position - transform.position;

        if (relativePos.sqrMagnitude > Mathf.Pow(detectDistance, 2))
        {
            Rotate(speed, true);
            MoveVertical(speed, 0, false);

            return;
        }

        Quaternion rotation =  Quaternion.LookRotation(relativePos);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.1f);

        if (relativePos.sqrMagnitude > minDistance)
        {
            MoveVertical(speed, 0, false);
        }
    }
}
