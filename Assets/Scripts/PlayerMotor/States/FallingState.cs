using UnityEngine;

public class FallingState : BaseState
{
    public override void Exit()
    {
        m_playerMotor.verticalVelocity = 0.0f;
    }

    public override void StateUpdate()
    {
        if (m_playerMotor.isGrounded)
            m_playerMotor.ChangeState(GetComponent<RunningState>());
    }

    public override void ProcessMotion(ref Vector3 moveVector)
    {
        m_playerMotor.ApplyGravity();

        moveVector.x = m_playerMotor.ActualSideSpeed();
        moveVector.y = m_playerMotor.verticalVelocity;
        moveVector.z = m_playerMotor.baseRunSpeed;
    }
}