using System;

public class GameStateDeath : GameState
{
    public override void Enter()
    {
        base.Enter();
        InputManager.Instance.OnSwipe += OnSwipe;
    }
    public override void Exit()
    {
        InputManager.Instance.OnSwipe -= OnSwipe;
    }

    private void OnSwipe(InputManager.ESwipeDir swipeDir)
    {
        switch (swipeDir)
        {
            case InputManager.ESwipeDir.Up:
                RestartGame();
                break;

            case InputManager.ESwipeDir.Down:
                OpenMenu();
                break;
        }
    }

    private void OpenMenu()
    {
    }

    private void RestartGame()
    {
        m_gameFlow.PlayerMotor.RespawnPlayer();
        m_gameFlow.ChangeState(GetComponent<GameStateInGame>());
    }
}