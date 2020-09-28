using UnityEngine;

/// <summary>
/// The animation states.
/// </summary>
public enum State
{
    IDLE,
    MOVE,
    DAMAGED
}

/// <summary>
/// The state of player's weapon.
/// </summary>
public enum WeaponState
{
    UNARMED,
    ARMED_SWORD
}

/// <summary>
/// The states of the player's character.
/// </summary>
public class PlayerState : MonoBehaviour
{
    public State currentState;

    public WeaponState weaponState;

    public float inputH;
    public float inputV;

    public bool run;
    public bool attack;
    public bool block;

    [Header("SET THIS VALUE!")]
    public float blockAngle;

    public float maxHP;
    public float currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    /// <summary>
    /// Converts the current state into index.
    /// </summary>
    /// <returns>the index of the current state</returns>
    public int getCurrentState()
    {
        switch (currentState)
        {
            case State.IDLE:
                return 0;
            case State.MOVE:
                return 1;
            case State.DAMAGED:
                return 2;
            default:
                return -1;
        }
    }

    /// <summary>
    /// Converts the current weapon state into index.
    /// </summary>
    /// <returns>the index of the current weapon state</returns>
    public int getWeaponState()
    {
        switch (weaponState)
        {
            case WeaponState.UNARMED:
                return 0;
            case WeaponState.ARMED_SWORD:
                return 1;
            default:
                return -1;
        }
    }

    /// <summary>
    /// Checks if the attack can be blocked.
    /// </summary>
    /// <param name="angle">the attack angle</param>
    /// <param name="power">the attack power</param>
    /// <returns>can block or not</returns>
    public bool CanBlock(float angle, float power)
    {
        if (!block)
        {
            return false;
        }

        if (Mathf.Abs(angle) < blockAngle)
        {
            return true;
        }

        return false;
    }
}
