using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateDeath : GameState
{
    [SerializeField] private GameObject m_deathUI;

    [SerializeField] private TextMeshProUGUI m_highscoreText;
    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private TextMeshProUGUI m_totalFishCountText;
    [SerializeField] private TextMeshProUGUI m_fishCountText;

    // Revie circle animation properties
    [SerializeField] private Image m_reviveImage;
    [SerializeField] private float m_timeToDesicion = 2.5f;
    private float m_reviveAnimStartTime;

    public override void Enter()
    {
        base.Enter();

        m_highscoreText.text = "HIGHSCORE: " + "TBD";
        m_scoreText.text = "TBD";
        m_totalFishCountText.text = "FISHES: " + "xTBD";
        m_fishCountText.text = "xTBD";

        m_deathUI.SetActive(true);

        m_reviveImage.gameObject.SetActive(true);
        m_reviveAnimStartTime = Time.time;
    }

    public override void Exit()
    {
        m_deathUI.SetActive(false);
        base.Exit();
    }

    public override void UpdateState()
    {
        if(m_reviveImage.gameObject.activeSelf)
        {
            float ratio = (Time.time - m_reviveAnimStartTime) / m_timeToDesicion;
            if(ratio > 1.0f)
            {
                m_reviveImage.gameObject.SetActive(false);
                return;
            }

            m_reviveImage.color = Color.Lerp(Color.green, Color.red, ratio);
            m_reviveImage.fillAmount = 1 - ratio;
        }
    }

    public void OpenMenu()
    {
        m_gameFlow.ChangeState(GetComponent<GameStateMenu>());
    }

    public void ResumeGame()
    {
        m_gameFlow.ChangeState(GetComponent<GameStateInGame>());
        m_gameFlow.PlayerMotor.RespawnPlayer();
    }
}