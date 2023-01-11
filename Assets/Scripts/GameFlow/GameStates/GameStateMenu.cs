using TMPro;
using UnityEngine;

public class GameStateMenu : GameState
{
    [SerializeField] GameObject m_menuCanvas;
    [SerializeField] TextMeshProUGUI m_highscoreText;
    [SerializeField] TextMeshProUGUI m_fishCountText;

    public override void Enter()
    {
        base.Enter();

        // Core logic
        m_gameFlow.ChangeCamera(ECamera.Menu);
        m_gameFlow.PlayerMotor.ResetPlayer();
        foreach (var worldGenerator in m_gameFlow.WorldGenerators)
        {
            worldGenerator.ResetWorld();
        }

        // UI logic
        m_highscoreText.text = "HIGHSCORE: " + "TBD";
        m_fishCountText.text = "FISHES: " + "TBD";

        m_menuCanvas.SetActive(true);
    }

    public override void Exit()
    {
        m_menuCanvas.SetActive(false);
    }

    public void OnPlayClick()
    {
        GameStats.Instance.ResetSession();
        m_gameFlow.PlayerMotor.ChangeState(m_gameFlow.PlayerMotor.GetComponent<RunningState>());
        m_gameFlow.ChangeState(GetComponent<GameStateInGame>());
    }

    public void OnShopClick()
    {
        // Go to Shop state.
    }
}