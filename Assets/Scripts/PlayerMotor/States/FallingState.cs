using UnityEngine;

public class FallingState : BaseState
{
    public override void Enter()
    {
        base.Enter();
        m_playerMotor.moveVector.y = 0.0f;
    }

    public override void UpdateState()
    {
        if (m_playerMotor.isGrounded)
            m_playerMotor.ChangeState(GetComponent<RunningState>());
    }

    public override void ProcessMotion(ref Vector3 moveVector)
    {
        moveVector.x = m_playerMotor.ActualSideSpeed();
        moveVector.z = m_playerMotor.baseRunSpeed;
    }
}