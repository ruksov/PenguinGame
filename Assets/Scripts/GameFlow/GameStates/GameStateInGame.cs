public class GameStateInGame : GameState
{
    public override void Enter()
    {
        m_gameFlow.PlayerMotor.ChangeState(m_gameFlow.PlayerMotor.GetComponent<RunningState>());
    }
}