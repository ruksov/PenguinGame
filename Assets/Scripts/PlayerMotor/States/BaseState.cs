using System;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected PlayerMotor m_playerMotor;
    
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void UpdateState() { }

    private void Awake()
    {
        m_playerMotor = GetComponent<PlayerMotor>();
    }

    public virtual void ProcessMotion(ref Vector3 moveVector)
    {
        Debug.Log("ProcessMotion is not implemented in " + this.ToString());
    }
}
