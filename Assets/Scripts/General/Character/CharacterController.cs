using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
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
}
