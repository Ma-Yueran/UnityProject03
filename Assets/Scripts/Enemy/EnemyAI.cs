using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float distance;

    public float attackPeriod;

    public float attackInterval;

    private float lastAttackTime;

    private EnemyMovement enemyMovement;

    private EnemyState enemyState;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyState = GetComponent<EnemyState>();

        enemyState.currentState = State.IDLE;
        enemyState.weaponState = WeaponState.ARMED_SWORD;
    }

    private void Update()
    {
        enemyState.weaponState = WeaponState.ARMED_SWORD;

        if (enemyState.currentState == State.DAMAGED)
        {
            return;
        }

        if (!IsCloseEnough())
        {
            enemyState.currentState = State.MOVE;
        }
        else
        {
            enemyState.currentState = State.IDLE;
            Attack();
        }
    }

    public void MoveHorizontal(float speedForward, float speedBackward)
    {

    }

    public void MoveVertical(float speedForward, float speedBackward)
    {
        if (!IsCloseEnough())
        {
            enemyMovement.MoveVertical(speedForward);
        }
    }

    public void Rotate()
    {
        enemyMovement.Rotate(target.position);
    }

    private bool IsCloseEnough()
    {
        return (transform.position - target.position).sqrMagnitude < distance * distance;
    }

    private void Attack()
    {
        if (Time.time > lastAttackTime + attackInterval)
        {
            if (Time.time < lastAttackTime + attackInterval + attackPeriod)
            {
                enemyState.attack = true;
            }
            else
            {
                enemyState.attack = false;
                lastAttackTime = Time.time;
            }
        }
    }
}
