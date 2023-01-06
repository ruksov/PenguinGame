using UnityEngine;

public class JumpingState : BaseState
{
    [SerializeField] private float m_jumpForce = 7.0f;
    
    public override void Enter()
    {
        m_playerMotor.animator.SetTrigger("Jump");
        m_playerMotor.moveVector.y = m_jumpForce;
    }

    public override void UpdateState()
    {
        if (m_playerMotor.moveVector.y < 0.0f)
            m_playerMotor.ChangeState(GetComponent<FallingState>());
    }

    public override void ProcessMotion(ref Vector3 moveVector)
    {
        moveVector.x = m_playerMotor.ActualSideSpeed();
        moveVector.z = m_playerMotor.baseRunSpeed;
    }
}