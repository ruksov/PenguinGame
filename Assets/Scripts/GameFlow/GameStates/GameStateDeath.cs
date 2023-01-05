public class GameStateDeath : GameState
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
        m_gameFlow.PlayerMotor.animator.SetTrigger("Respawn");
        m_gameFlow.PlayerMotor.ChangeState(m_gameFlow.PlayerMotor.GetComponent<RunningState>());
    }
}