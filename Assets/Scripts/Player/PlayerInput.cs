using UnityEngine;

/// <summary>
/// PlayerInput updates PlayerState using keybroad inputs.
/// </summary>
public class PlayerInput : MonoBehaviour
{
    private PlayerState playerState;

    private void Start()
    {
        playerState = GetComponent<PlayerState>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        playerState.inputH = Input.GetAxis("Horizontal");
        playerState.inputV = Input.GetAxis("Vertical");

        UpdatePlayerCurrentState();

        UpdatePlayerWeaponState();

        playerState.run = Input.GetKey(KeyCode.LeftShift);

        playerState.attack = Input.GetMouseButton(0);

        playerState.block = Input.GetMouseButton(1);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void UpdatePlayerCurrentState()
    {
        if (playerState.currentState == State.DAMAGED)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            playerState.currentState = State.MOVE;
        }
        else
        {
            playerState.currentState = State.IDLE;
        }
    }

    private void UpdatePlayerWeaponState()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerState.currentState == State.IDLE)
        {
            if (playerState.weaponState == WeaponState.UNARMED)
            {
                playerState.weaponState = WeaponState.ARMED_SWORD;
            }
            else
            {
                playerState.weaponState = WeaponState.UNARMED;
            }
        }
    }
}
