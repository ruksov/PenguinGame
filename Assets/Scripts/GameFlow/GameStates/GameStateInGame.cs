using System;
using TMPro;
using UnityEngine;

public class GameStateInGame : GameState
{
    [SerializeField] private GameObject m_gameUI;
    [SerializeField] private TextMeshProUGUI m_fishCountText;
    [SerializeField] private TextMeshProUGUI m_scoreText;

    private void Start()
    {
        GameStats.Instance.OnScoreChanged += OnScoreChanged;
        GameStats.Instance.OnFishCountChanged += OnFishCountChanged;
    }

    private void OnDestroy()
    {
        GameStats.Instance.OnScoreChanged -= OnScoreChanged;
        GameStats.Instance.OnFishCountChanged -= OnFishCountChanged;
    }

    public override void Enter()
    {
        base.Enter();
        m_gameFlow.ChangeCamera(ECamera.InGame);
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

    private void OnFishCountChanged(int fishCount)
    {
        m_fishCountText.text = "x" + fishCount.ToString();
    }

    private void OnScoreChanged(int score)
    {
        m_scoreText.text = score.ToString();
    }
}