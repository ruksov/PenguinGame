using UnityEngine;
using System.Collections.Generic;

public enum ECamera
{
    Init = 0,
    InGame,
    Shop,
    Respawn
}

public class GameFlow : MonoBehaviour
{
    // Singleton members
    private static GameFlow m_instance;
    public static GameFlow Instance => m_instance;

    private GameState m_state;
    [SerializeField] private GameState m_firstState;

    [SerializeField] private PlayerMotor m_playerMotor;
    public PlayerMotor PlayerMotor => m_playerMotor;

    [SerializeField] private WorldGenerator[] m_worldGenerators;
    public WorldGenerator[] WorldGenerators => m_worldGenerators;

    [SerializeField] private GameObject[] m_cameras;
    
    private void Awake()
    {
        if (m_instance)
        {
            Destroy(gameObject);
            return;
        }
        
        m_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_state = m_firstState;
        m_state.Enter();
    }

    private void Update()
    {
        m_state.UpdateState(); 
    }

    public void ChangeState(GameState state)
    {
        m_state.Exit();
        m_state = state;
        m_state.Enter();
    }

    public void ChangeCamera(ECamera type)
    {
        foreach(GameObject camera in m_cameras)
        {
            camera.SetActive(false);
        }

        m_cameras[(int)type].SetActive(true);
    }
}