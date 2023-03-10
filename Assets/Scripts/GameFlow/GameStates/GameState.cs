using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameFlow m_gameFlow;
    
    public virtual void Enter() { Debug.Log("Enter to " + ToString()); }
    public virtual void Exit() { }
    public virtual void UpdateState() { }

    private void Awake()
    {
        m_gameFlow = GetComponent<GameFlow>();
    }
}