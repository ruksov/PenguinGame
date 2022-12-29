using System;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected PlayerMotor m_playerMotor;
    
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }

    private void Awake()
    {
        m_playerMotor = GetComponent<PlayerMotor>();
    }

    public virtual Vector3 ProcessMotion()
    {
        Debug.Log("ProcessMotion is not implemented in " + this.ToString());
        return Vector3.zero; 
    }
}
