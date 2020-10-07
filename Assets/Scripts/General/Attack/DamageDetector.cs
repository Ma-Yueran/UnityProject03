using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    private CharacterController character;

    private AttackManager attackManager;

    private void Start()
    {
        character = GetComponentInParent<CharacterController>();
        attackManager = AttackManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        AttackInfo attackInfo = attackManager.GetAttackInfo(other);

        if (attackInfo == null)
        {
            return;
        }

        if (attackInfo.attacker.Equals(character))
        {
            return;
        }

        character.TakeDamage(attackInfo);
    }
}
