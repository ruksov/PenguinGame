using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private float m_length;

    public float Length => m_length;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
