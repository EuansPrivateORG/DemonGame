using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserDemonRagdoll : MonoBehaviour
{
    private Animator animator;

    private Rigidbody[] ragdollRigidbodies;
    private Collider[] colliders;

    private void Awake()
    {
        colliders = HelperFuntions.AllChildren<Collider>(transform).ToArray();
        ragdollRigidbodies = HelperFuntions.AllChildren<Rigidbody>(transform).ToArray();

        animator = GetComponent<Animator>();
    }

    public void ToggleRagdoll(bool state)
    {
        animator.enabled = !state;

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = !state;
            rb.useGravity = state;
        }

        foreach (Collider col in colliders)
        {
            col.enabled = state;
        }
    }
}
