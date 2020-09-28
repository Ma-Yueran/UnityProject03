using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    public abstract void MoveVertical(float speedForward, float speedBackward, bool controllable);

    public abstract void MoveHorizontal(float speedForward, float speedBackward, bool controllable);

    public abstract void Rotate(float speed, bool controllable);

    public abstract void Equip(string itemName, string bodyPartName, Vector3 localPosition, Vector3 localRotation);

    public abstract void SetTransit(bool value);

    public abstract void SetWeapon(string weaponName);

    public abstract void SetWeaponActive(bool isActive, float attackPower);

    public abstract void SetState(State state);

    public abstract void TakeDamage(float angle, float power);

    public abstract void AddForce(Vector3 force);

    public abstract AnimationProgress GetAnimationProgress();

    public abstract void TurnOffPhysics();
}
