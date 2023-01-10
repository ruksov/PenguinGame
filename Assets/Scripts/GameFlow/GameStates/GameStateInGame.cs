using TMPro;
using UnityEngine;

public class GameStateInGame : GameState
{
    [SerializeField] private GameObject m_gameUI;
    [SerializeField] private TextMeshProUGUI m_fishCountText;
    [SerializeField] private TextMeshProUGUI m_scoreText;

    public override void Enter()
    {
        base.Enter();
        m_gameFlow.ChangeCamera(ECamera.InGame);

        m_fishCountText.text = "xTBD";
        m_scoreText.text = "TBD";

        m_gameUI.SetActive(true);
    }

    public override void Exit()
    {
        m_gameUI.SetActive(false);
        base.Exit();
    }

    public override void UpdateState()
    {
        foreach (var worldGenerator in m_gameFlow.WorldGenerators)
        {
            worldGenerator.ScanPosition();
        }
    }
}