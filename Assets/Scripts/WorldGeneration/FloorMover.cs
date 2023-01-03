using UnityEngine;

public class FloorMover : MonoBehaviour
{
    [SerializeField] private Transform m_player;
    [SerializeField] private Material m_material;
    [SerializeField] private float m_tileOffsetMultiplier = -0.25f;
    
    private Vector4 m_tileOffset = Vector4.zero;

    void Update()
    {
        transform.position = Vector3.forward * m_player.position.z;
        
        m_tileOffset.y = m_tileOffsetMultiplier * m_player.position.z;
        m_material.SetVector("tileOffset", m_tileOffset);
    }
}
