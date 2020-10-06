using UnityEngine;

public enum Direction
{
    FORWARD,
    BACKWARD,
    LEFT,
    RIGHT,
    FORWARD_LEFT,
    FORWARD_RIGHT,
    BACKWARD_LEFT,
    BACKWARD_RIGHT,
    MIDDLE
}

public abstract class CharacterController : MonoBehaviour
{
    public float velocityLimit;

    protected AnimationProgress animationProgress;

    protected Animator characterAnimator;

    protected Rigidbody characterRigidbody;

    protected void InitializeController()
    {
        animationProgress = new AnimationProgress();
        characterAnimator = GetComponentInChildren<Animator>();
        characterRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Moves the character vertically.
    /// </summary>
    /// <param name="speedForward">the speed of moving forward</param>
    /// <param name="speedBackward">the speed of moving backward</param>
    /// <param name="controllable">does the character control the movement</param>
    public abstract void MoveVertical(float speedForward, float speedBackward, bool controllable);

    /// <summary>
    /// Moves the character horizontally.
    /// </summary>
    /// <param name="speedForward">the speed when moving forward</param>
    /// <param name="speedBackward">the speed when moving backward</param>
    /// <param name="controllable">does the character control the movement</param>
    public abstract void MoveHorizontal(float speedForward, float speedBackward, bool controllable);

    /// <summary>
    /// Rotates the character.
    /// </summary>
    /// <param name="speed">the rotation speed</param>
    /// <param name="controllable">does the character control the rotation</param>
    public abstract void Rotate(float speed, bool controllable);

    /// <summary>
    /// Equips the item to the body part with the given local position and rotation.
    /// </summary>
    /// <param name="itemName">the name of the item</param>
    /// <param name="bodyPartName">the name of the body part</param>
    /// <param name="localPosition">the local position for the item</param>
    /// <param name="localRotation">the local rotation for the item</param>
    public abstract void Equip(string itemName, string bodyPartName, Vector3 localPosition, Vector3 localRotation);

    /// <summary>
    /// Sets the "Transit" value of the animator of the character.
    /// </summary>
    /// <param name="value">the value</param>
    public virtual void SetTransit(bool value)
    {
        characterAnimator.SetBool("Transit", value);
    }

    /// <summary>
    /// Sets the current weapon of the character.
    /// </summary>
    /// <param name="weaponName">the name of the weapon</param>
    public abstract void SetWeapon(string weaponName);

    /// <summary>
    /// Sets the current weapon to be active or inactive, sets the attack power.
    /// </summary>
    /// <param name="isActive">active or not</param>
    /// <param name="attackPower">the attack power</param>
    public abstract void SetWeaponActive(bool isActive, float attackPower);

    /// <summary>
    /// Sets the current state of the character.
    /// </summary>
    /// <param name="state">the state</param>
    public abstract void SetState(State state);

    /// <summary>
    /// Takes damage.
    /// </summary>
    /// <param name="angle">the angle of the damage recieved</param>
    /// <param name="power">the power of the damage</param>
    public abstract void TakeDamage(float angle, float power);

    /// <summary>
    /// Adds the given force to the character.
    /// </summary>
    /// <param name="force">the force</param>
    /// <param name="isRelative">is the force relative</param>
    public virtual void AddForce(Vector3 force, bool isRelative)
    {
        if (!isRelative)
        {
            characterRigidbody.AddForce(force, ForceMode.Impulse);
        }
        else
        {
            characterRigidbody.AddRelativeForce(force, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Gets the AnimationProgress of the character.
    /// </summary>
    /// <returns>the AnimationProgress</returns>
    public virtual AnimationProgress GetAnimationProgress()
    {
        return animationProgress;
    }

    /// <summary>
    /// Turns off all physics of the character.
    /// </summary>
    public virtual void TurnOffPhysics()
    {
        characterRigidbody.isKinematic = true;
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    /// <summary>
    /// Reduces the character's velocity when it gets larger that the limit.
    /// </summary>
    public void ControlVelocity()
    {
        if (characterRigidbody.velocity.sqrMagnitude > velocityLimit)
        {
            characterRigidbody.velocity /= 2;
        }
    }

    /// <summary>
    /// Gets the damage direction.
    /// </summary>
    /// <param name="angle">the angle of the damage</param>
    /// <returns></returns>
    public virtual Direction GetDamageDirection(float angle)
    {
        if (Mathf.Abs(angle) < 30)
        {
            return Direction.FORWARD;
        }
        
        if  (Mathf.Abs(angle) < 60)
        {
            if (angle < 0)
            {
                return Direction.FORWARD_RIGHT;
            }

            return Direction.FORWARD_LEFT;
        }

        if (Mathf.Abs(angle) < 120)
        {
            if (angle < 0)
            {
                return Direction.RIGHT;
            }

            return Direction.LEFT;
        }

        if (Mathf.Abs(angle) < 150)
        {
            if (angle < 0)
            {
                return Direction.BACKWARD_RIGHT;
            }

            return Direction.BACKWARD_LEFT;
        }

        return Direction.BACKWARD;
    }

    /// <summary>
    /// Sets the DamageDirection of the character's controller.
    /// </summary>
    /// <param name="angle">the angle of the damage</param>
    public virtual void SetDamageDirection(float angle)
    {
        Direction direction = GetDamageDirection(angle);

        switch (direction)
        {
            case Direction.FORWARD:
                characterAnimator.SetFloat("DamageDirection", 0);
                return;
            case Direction.BACKWARD:
                characterAnimator.SetFloat("DamageDirection", 1);
                return;
            case Direction.LEFT:
                characterAnimator.SetFloat("DamageDirection", 2);
                return;
            case Direction.RIGHT:
                characterAnimator.SetFloat("DamageDirection", 3);
                return;
            case Direction.FORWARD_LEFT:
                characterAnimator.SetFloat("DamageDirection", 4);
                return;
            case Direction.FORWARD_RIGHT:
                characterAnimator.SetFloat("DamageDirection", 5);
                return;
            case Direction.BACKWARD_LEFT:
                characterAnimator.SetFloat("DamageDirection", 6);
                return;
            case Direction.BACKWARD_RIGHT:
                characterAnimator.SetFloat("DamageDirection", 7);
                return;
            default:
                return;
        }
    }

    /// <summary>
    /// Gets the dodge direction.
    /// </summary>
    /// <returns>the dodge direction</returns>
    public abstract Direction GetDodgeDirection();

    /// <summary>
    /// Sets the DodgeDirection in the character's animator.
    /// </summary>
    /// <param name="direction">the dodge direction</param>
    public virtual void SetDodgeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.FORWARD:
                characterAnimator.SetFloat("DodgeDirection", 0);
                return;
            case Direction.BACKWARD:
                characterAnimator.SetFloat("DodgeDirection", 1);
                return;
            case Direction.LEFT:
                characterAnimator.SetFloat("DodgeDirection", 2);
                return;
            case Direction.RIGHT:
                characterAnimator.SetFloat("DodgeDirection", 3);
                return;
            case Direction.MIDDLE:
                characterAnimator.SetFloat("DodgeDirection", 4);
                return;
        }
    }

    /// <summary>
    /// Gets the death direction of the character.
    /// </summary>
    /// <param name="angle">the angle of the damage</param>
    /// <returns></returns>
    public virtual Direction GetDeathDirection(float angle)
    {
        if (Mathf.Abs(angle) < 45)
        {
            return Direction.FORWARD;
        }
        else if (Mathf.Abs(angle) > 135)
        {
            return Direction.BACKWARD;
        }
        else if (angle < 0)
        {
            return Direction.RIGHT;
        }
        else
        {
            return Direction.LEFT;
        }
    }

    /// <summary>
    /// Sets the DeathDirection in the animator of the character.
    /// </summary>
    /// <param name="angle">the angle of the damage</param>
    public virtual void SetDeathDirection(float angle)
    {
        Direction direction = GetDeathDirection(angle);

        switch (direction)
        {
            case Direction.FORWARD:
                characterAnimator.SetFloat("DeathDirection", 0);
                return;
            case Direction.BACKWARD:
                characterAnimator.SetFloat("DeathDirection", 1);
                return;
            case Direction.LEFT:
                characterAnimator.SetFloat("DeathDirection", 2);
                return;
            case Direction.RIGHT:
                characterAnimator.SetFloat("DeathDirection", 3);
                return;
            default:
                return;
        }
    }

    public abstract void GetTarget();

    public abstract void FollowTarget(float speed, float minDistance, float detectDistance);
}
