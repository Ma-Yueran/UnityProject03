using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfo : MonoBehaviour
{
    public CharacterController attacker;

    public Collider attackCollider;

    public float attackPower;

    public float activeTime;

    public bool isHit;

    private float endTime;

    private void OnEnable()
    {
        endTime = activeTime + Time.time;
    }

    private void Update()
    {
        if (Time.time > endTime)
        {
            gameObject.SetActive(false);
        }
    }
}
