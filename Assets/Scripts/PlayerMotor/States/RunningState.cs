 using UnityEngine;

public class RunningState : BaseState
{
    public override void Enter()
    {
        InputManager.Instance.OnSwipe += OnSwipe;
    }

    public override void Exit()
    {
        InputManager.Instance.OnSwipe -= OnSwipe;
    }

    public override void StateUpdate()
    {
        if (!m_playerMotor.isGrounded)
        {
            m_playerMotor.ChangeState(GetComponent<FallingState>());
        }
    }

    private void OnSwipe(InputManager.ESwipeDir swipeDir)
    {
        switch (swipeDir)
        {
            case InputManager.ESwipeDir.Up:
                m_playerMotor.ChangeState(GetComponent<JumpingState>());
                break;
            
            case InputManager.ESwipeDir.Down:
                break;
            
            case InputManager.ESwipeDir.Right:
                m_playerMotor.ChangeLane(1);
                break;
            
            case InputManager.ESwipeDir.Left:
                m_playerMotor.ChangeLane(-1);
                break;
        }
    }

    public override void ProcessMotion(ref Vector3 moveVector)
    {
        moveVector.x = m_playerMotor.ActualSideSpeed();
        moveVector.z = m_playerMotor.baseRunSpeed;
    }
}