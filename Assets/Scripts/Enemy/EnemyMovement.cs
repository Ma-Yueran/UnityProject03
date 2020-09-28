using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyState enemyState;

    private void Start()
    {
        enemyState = GetComponent<EnemyState>();
    }

    public void MoveVertical(float speed)
    {
        float moveV = speed * Time.deltaTime;

        transform.Translate(0, 0, moveV);

        float inputV = speed > 0 ? 1 : speed < 0 ? -1 : 0;

        enemyState.inputV = inputV;
    }

    public void MoveHorizontal(float speed)
    {
        float moveH = speed * Time.deltaTime;

        transform.Translate(0, 0, moveH);

        float inputH = speed > 0 ? 1 : speed < 0 ? -1 : 0;

        enemyState.inputH = inputH;
    }

    public void Rotate(Vector3 target)
    {
        Vector3 relativePos = target - transform.position;
        relativePos.y = 0;

        Quaternion rotation = Quaternion.LookRotation(relativePos);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.1f);
    }

    public void Rotate(float speed)
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime * 0));
    }
}
