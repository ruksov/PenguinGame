using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{
    // Configurable fields
    [SerializeField] private int m_firstChunkSpawnPositionOffset = -10;
    [SerializeField] private int m_chunksOnScreen = 5;
    [SerializeField] private float m_despawnChunkDistance = 5f;

    [SerializeField] private List<GameObject> m_chunkPrefabs;
    [SerializeField] private Transform m_cameraTransform;
    
    // Gameplay
    private float m_lastChunkSpawnZ;
    private Queue<Chunk> m_activeChunks = new();
    private List<Chunk> m_chunkPool = new();

    // TO DELETE @@
    private void Awake()
    {
        ResetWorld();
    }

    private void Start()
    {
        if (m_chunkPrefabs.Count == 0)
        {
            Debug.LogError("No chunk prefab found on the world generator, please assign some chunks!");
            return;
        }

        if (!m_cameraTransform)
        {
            m_cameraTransform = Camera.main.transform;
            Debug.Log("Camera.main transform was assigned to World Generator's camera");
        }
    }

    private void Update()
    {
        ScanPosition();
    }

    private void ScanPosition()
    {
        float cameraZ = m_cameraTransform.position.z;
        Chunk firstChunk = m_activeChunks.Peek();

        if (cameraZ >= firstChunk.transform.position.z + firstChunk.Length + m_despawnChunkDistance)
        {
            SpawnNewChunk();
            DeleteFirstChunk();
        }
    }

    private void SpawnNewChunk()
    {
        int randomIndex = Random.Range(0, m_chunkPrefabs.Count);

        Chunk chunk = null;
        
        int poolIndex = m_chunkPool.FindIndex(x => x.PrefabID == randomIndex);
        if (poolIndex >= 0)
        {
            chunk = m_chunkPool[poolIndex];
            m_chunkPool.RemoveAt(poolIndex);
        }
        
        if (!chunk)
        {
            GameObject go = Instantiate(m_chunkPrefabs[randomIndex], transform);
            chunk = go.GetComponent<Chunk>();

            if (!chunk)
            {
                Debug.LogError($"Failed to instantiate new chunk. Chunk prefab with index {randomIndex} has no Chunk component!");
                return;
            }
            
            chunk.SetPrefabID(randomIndex);
        }

        chunk.transform.position = new Vector3(0f, 0f, m_lastChunkSpawnZ);
        m_lastChunkSpawnZ += chunk.Length;
        
        m_activeChunks.Enqueue(chunk);
        chunk.Show();
    }

    private void DeleteFirstChunk()
    {
        m_chunkPool.Add(m_activeChunks.Dequeue().Hide());
    }

    private void ResetWorld()
    {
        m_lastChunkSpawnZ = m_firstChunkSpawnPositionOffset;

        while (m_activeChunks.Count != 0)
        {
            DeleteFirstChunk();
        }

        for (int i = 0; i < m_chunksOnScreen; ++i)
        {
            SpawnNewChunk();
        }
    }
}
