using UnityEngine;

/// <summary>
/// PlayerMovement manages the movements of the player.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private PlayerState playerState;

    private Camera playerCamera;

    private void Start()
    {
        playerState = GetComponent<PlayerState>();
        playerCamera = GetComponent<PlayerController>().playerCamera;
    }

    /// <summary>
    /// Moves the character horizontally.
    /// </summary>
    /// <param name="speedForward">the speed to move forward</param>
    /// <param name="speedBackward">the speed to move backward</param>
    /// <param name="controllable">is the moving controllable</param>
    public void MoveHorizontal(float speedForward, float speedBackward, bool controllable)
    {
        float moveH;

        if (controllable)
        {
            moveH = Time.deltaTime * playerState.inputH;

            if (playerState.inputV >= 0)
            {
                moveH *= speedForward;
            }
            else
            {
                moveH *= speedBackward;
            }
        }
        else
        {
            moveH = speedForward * Time.deltaTime;
        }

        transform.Translate(moveH, 0f, 0f);
    }

    /// <summary>
    /// Moves the character vertically.
    /// </summary>
    /// <param name="speedForward">the speed to move while moving forward</param>
    /// <param name="speedBackward">the speed to move while moveing backward</param>
    /// <param name="controllable">is the moving controllable</param>
    public void MoveVertical(float speedForward, float speedBackward, bool controllable)
    {
        float moveV;

        if (controllable)
        {
            moveV = Time.deltaTime * playerState.inputV;

            if (moveV > 0)
            {
                moveV *= speedForward;
            }
            else
            {
                moveV *= speedBackward;
            }

            if (playerState.inputH != 0)
            {
                moveV *= 0.6f;
            }
        }
        else
        {
            moveV = speedForward * Time.deltaTime;
        }

        transform.Translate(0f, 0f, moveV);
    }

    /// <summary>
    /// Rotates the character along y-axis.
    /// </summary>
    /// <param name="speed">the rotation speed</param>
    /// <param name="controllable">is the rotation controllable</param>
    public void Rotate(float speed, bool controllable)
    {
        if (controllable)
        {
            Vector3 relativePos = transform.position - playerCamera.transform.position;
            relativePos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.1f);
        }
        else
        {
            transform.Rotate(0f, speed * Time.deltaTime, 0f);
        }
    }
}
