using System;
using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] private float m_respawnHeight = 25.0f;

    public override void Enter()
    {
        InputManager.Instance.OnSwipe += OnSwipe;

        TeleportPlayer();
        m_playerMotor.animator.SetTrigger("Respawn");
    }

    public override void Exit()
    {
        InputManager.Instance.OnSwipe -= OnSwipe;
    }

    public override void UpdateState()
    {
        if(m_playerMotor.isGrounded && !IsStartFalling())
            m_playerMotor.ChangeState(GetComponent<RunningState>());
    }

    public override void ProcessMotion(ref Vector3 moveVector)
    {
        m_playerMotor.ApplyGravity();

        moveVector.x = m_playerMotor.ActualSideSpeed();
        moveVector.y = m_playerMotor.verticalVelocity;
        moveVector.z = m_playerMotor.baseRunSpeed;
    }

    private bool IsStartFalling()
    {
        return m_playerMotor.gravity > Mathf.Abs(m_playerMotor.verticalVelocity);
    }

    private void TeleportPlayer()
    {
        m_playerMotor.controller.enabled = false;

        m_playerMotor.transform.position = new Vector3(
            0.0f,
            m_respawnHeight,
            m_playerMotor.transform.position.z);

        m_playerMotor.verticalVelocity = 0;
        m_playerMotor.ResetLane();

        m_playerMotor.controller.enabled = true;
    }

    private void OnSwipe(InputManager.ESwipeDir swipeDir)
    {
        switch (swipeDir)
        {
            case InputManager.ESwipeDir.Right:
                m_playerMotor.ChangeLane(1);
                break;
            
            case InputManager.ESwipeDir.Left:
                m_playerMotor.ChangeLane(-1);
                break;
        }
    }
}