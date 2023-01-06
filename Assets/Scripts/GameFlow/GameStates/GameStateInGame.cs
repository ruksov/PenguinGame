public class GameStateInGame : GameState
{
    public override void Enter()
    {
        base.Enter();
        m_gameFlow.ChangeCamera(ECamera.InGame);
    }

    public override void UpdateState()
    {
        foreach (var worldGenerator in m_gameFlow.WorldGenerators)
        {
            worldGenerator.ScanPosition();
        }
    }
}