using UnityEngine;

public class GameStateInit : GameState
{
    public override void Enter()
    {
        base.Enter();

        m_gameFlow.ChangeCamera(ECamera.Init);
        
        m_gameFlow.PlayerMotor.ResetLane();
        m_gameFlow.PlayerMotor.Teleport(Vector3.zero);
        m_gameFlow.PlayerMotor.ChangeState(m_gameFlow.PlayerMotor.GetComponent<IdleState>());

        foreach (var worldGenerator in m_gameFlow.WorldGenerators)
        {
            worldGenerator.ResetWorld();
        }

        InputManager.Instance.OnTap += OnTap;
    }

    public override void Exit()
    {
        InputManager.Instance.OnTap -= OnTap;
    }

    private void OnTap()
    {
        m_gameFlow.PlayerMotor.ChangeState(m_gameFlow.PlayerMotor.GetComponent<RunningState>());
        m_gameFlow.ChangeState(GetComponent<GameStateInGame>());
    }
}