using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform m_rotatePoint;
    [SerializeField] private Transform m_transform;
    [SerializeField] private float m_speed;

    // Update is called once per frame
    void Update()
    {
        m_transform.RotateAround(m_rotatePoint.position, Vector3.up, m_speed * Time.deltaTime);
    }
}
