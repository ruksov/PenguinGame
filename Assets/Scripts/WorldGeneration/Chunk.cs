using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private float m_length;

    private int m_prefabID;

    public float Length => m_length;
    public int PrefabID => m_prefabID;

    public void SetPrefabID(int prefabID)
    {
        m_prefabID = prefabID;
    }
    
    public Chunk Show()
    {
        gameObject.SetActive(true);
        return this;
    }

    public Chunk Hide()
    {
        gameObject.SetActive(false);
        return this;
    }
}
