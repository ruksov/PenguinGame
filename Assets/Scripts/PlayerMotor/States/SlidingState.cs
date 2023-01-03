using UnityEngine;

public class SlidingState : BaseState
{
    [SerializeField] private float m_slideDuration = 1.0f;

    private Vector3 m_initialCenter;
    private float m_initialHeight;
    private float m_slideStartTime;

    public override void Enter()
    {
        m_playerMotor.animator.SetTrigger("Slide");

        m_slideStartTime = Time.time;
        InputManager.Instance.OnSwipe += OnSwipe;

        m_initialCenter = m_playerMotor.controller.center;
        m_initialHeight = m_playerMotor.controller.height;

        m_playerMotor.controller.center = m_initialCenter * 0.5f;
        m_playerMotor.controller.height = m_initialHeight * 0.5f;
    }

    public override void Exit()
    {
        m_playerMotor.controller.center = m_initialCenter;
        m_playerMotor.controller.height = m_initialHeight;

        InputManager.Instance.OnSwipe -= OnSwipe;

        m_playerMotor.animator.SetTrigger("Running");
    }

    public override void UpdateState()
    {
        if (!m_playerMotor.isGrounded)
            m_playerMotor.ChangeState(GetComponent<FallingState>());

        if (Time.time - m_slideStartTime > m_slideDuration)
            m_playerMotor.ChangeState(GetComponent<RunningState>());
    }

    public override void ProcessMotion(ref Vector3 moveVector)
    {
        moveVector.x = m_playerMotor.ActualSideSpeed();
        moveVector.z = m_playerMotor.baseRunSpeed;
    }

    private void OnSwipe(InputManager.ESwipeDir swipeDir)
    {
        switch(swipeDir)
        {
            case InputManager.ESwipeDir.Up:
                m_playerMotor.ChangeState(GetComponent<JumpingState>());
                break;

            case InputManager.ESwipeDir.Right:
                m_playerMotor.ChangeLane(1);
                break;

            case InputManager.ESwipeDir.Left:
                m_playerMotor.ChangeLane(-1);
                break;
        }
    }

}

