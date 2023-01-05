public class GameStateInit : GameState
{
    public override void Enter()
    {
        base.Enter();
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