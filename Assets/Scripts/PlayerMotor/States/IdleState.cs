using UnityEngine;

public class IdleState : BaseState
{
    public override void Enter()
    {
        base.Enter();
        m_playerMotor.animator.SetTrigger("Idle");
    }
    
    public override void ProcessMotion(ref Vector3 moveVector)
    {
        moveVector = Vector3.zero;
    }
}
