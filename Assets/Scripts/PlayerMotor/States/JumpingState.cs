using UnityEngine;

public class JumpingState : BaseState
{
    [SerializeField] private float m_jumpForce = 7.0f;
    
    public override void Enter()
    {
        m_playerMotor.animator.SetTrigger("Jump");
        m_playerMotor.verticalVelocity = m_jumpForce;
    }

    public override void UpdateState()
    {
        if (m_playerMotor.verticalVelocity < 0)
            m_playerMotor.ChangeState(GetComponent<FallingState>());
    }

    public override void ProcessMotion(ref Vector3 moveVector)
    {
        m_playerMotor.ApplyGravity();

        moveVector.x = m_playerMotor.ActualSideSpeed();
        moveVector.y = m_playerMotor.verticalVelocity;
        moveVector.z = m_playerMotor.baseRunSpeed;
    }
}