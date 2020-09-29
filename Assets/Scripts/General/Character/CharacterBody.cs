using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CharacterBody manages the body parts of the character.
/// </summary>
public class CharacterBody : MonoBehaviour
{
    public List<Collider> ragdoll;

    private void Start()
    {
        SetUpRagdoll();
    }

    public void SetUpRagdoll()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<Weapon>() != null)
            {
                continue;
            }
            collider.isTrigger = true;
            ragdoll.Add(collider);
        }

        GetComponent<Collider>().isTrigger = false;
    }

    /// <summary>
    /// Gets the body part with the given name.
    /// </summary>
    /// <param name="bodyPartName">the name of the body part</param>
    /// <returns></returns>
    public Transform GetBodyPart(string bodyPartName)
    {
        foreach (Collider collider in ragdoll)
        {
            if (collider.name.Equals(bodyPartName))
            {
                return collider.transform;
            }
        }

        return null;
    }
}
