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

    private void OnSwipe(InputManager.ESwipeDir swipeDir)
    {
        switch (swipeDir)
        {
            case InputManager.ESwipeDir.Up:
                break;
            case InputManager.ESwipeDir.Down:
                break;
            case InputManager.ESwipeDir.Right:
                break;
            case InputManager.ESwipeDir.Left:
                break;
        }
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 move = new Vector3(0.0f, -1.0f, m_playerMotor.baseRunSpeed);
        return move;
    }
}