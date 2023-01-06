using System;
using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] private float m_respawnHeight = 25.0f;

    public override void Enter()
    {
        InputManager.Instance.OnSwipe += OnSwipe;

        Vector3 teleportPosition = m_playerMotor.transform.position;
        teleportPosition.x = 0.0f;
        teleportPosition.y = m_respawnHeight;
        m_playerMotor.Teleport(teleportPosition);

        m_playerMotor.moveVector.y = 0;
        m_playerMotor.ResetLane();

        m_playerMotor.animator.SetTrigger("Respawn");

        GameFlow.Instance.ChangeCamera(ECamera.Respawn);
    }

    public override void Exit()
    {
        InputManager.Instance.OnSwipe -= OnSwipe;

        GameFlow.Instance.ChangeCamera(ECamera.InGame);
    }

    public override void UpdateState()
    {
        if (m_playerMotor.isGrounded &&
            Mathf.Approximately(m_playerMotor.moveVector.y, -m_playerMotor.gravity))
        {
            m_playerMotor.ChangeState(GetComponent<RunningState>());
        }
    }

    public override void ProcessMotion(ref Vector3 moveVector)
    {
        moveVector.x = m_playerMotor.ActualSideSpeed();
        moveVector.z = m_playerMotor.baseRunSpeed;
    }

    private bool IsStartFalling()
    {
        return m_playerMotor.gravity > Mathf.Abs(m_playerMotor.moveVector.y);
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