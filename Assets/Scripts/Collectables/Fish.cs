using System;
using UnityEngine;
 
public class Fish : MonoBehaviour
{
    private Animator m_animator;
    private Collider m_collider;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Pickup();
    }

    private void Pickup()
    {
        m_collider.enabled = false;
        m_animator.SetTrigger("Pickup");
    }

    private void OnShowChunk()
    {
        m_collider.enabled = true;
        m_animator.SetTrigger("Idle");
    }
}
