﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon script
/// </summary>
public class Weapon : MonoBehaviour
{
    private CharacterController user;

    private void Start()
    {
        user = GetComponentInParent<CharacterController>();
    }

    public CharacterController GetUser()
    {
        return user;
    }

    public Collider GetCollider()
    {
        return GetComponent<Collider>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!isActive)
    //    {
    //        return;
    //    }
        
    //    CharacterController hitCharacter = other.GetComponentInParent<CharacterController>();

    //    if (hitCharacter != null && !hitCharacter.Equals(user))
    //    {
    //        Vector3 direction = hitCharacter.transform.position - user.transform.position;
    //        direction.y = 0;

    //        Vector3 directionRight = Vector3.Cross(Vector3.up, direction);

    //        float angle = Vector3.Angle(-hitCharacter.transform.forward, direction);

    //        float sign = Mathf.Sign(Vector3.Dot(-hitCharacter.transform.forward, directionRight));

    //        float finalAngle = angle * sign;
    //        hitCharacter.TakeDamage(finalAngle, attackPower);
    //        isActive = false;
    //    }
    //}
}
