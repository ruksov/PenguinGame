public class GameStateInit : GameState
{
    public override void Enter()
    {
        InputManager.Instance.OnTap += OnTap;
    }

    public override void Exit()
    {
        InputManager.Instance.OnTap -= OnTap;
    }

    private void OnTap()
    {
        m_gameFlow.ChangeState(GetComponent<GameStateInGame>());
    }
}