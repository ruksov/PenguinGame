using UnityEngine;
using System.Collections;
using System;

public class GameStats : MonoBehaviour
{
    // Singleton members
    private static GameStats m_instance;
    public static GameStats Instance => m_instance;

    // Score
    [SerializeField] private float m_scoreDistanceMultiplier;
    [SerializeField] private int m_scorePointsPerFish;

    [ReadOnly] private int m_score;
    [ReadOnly] private int m_highscore;

    // Fishes
    [ReadOnly] private int m_fishCount;
    [ReadOnly] private int m_totalFishCount;

    // Events
    public Action<int> OnFishCountChanged;
    public Action<int> OnScoreChanged;

    // Properties
    public int Score
    {
        get => m_score;
        private set
        {
            if (m_score == value)
                return;

            m_score = value;
            OnScoreChanged?.Invoke(m_score);
        }
    }

    public int FishCount
    {
        get => m_fishCount;
        private set
        {
            if (m_fishCount == value)
                return;

            m_fishCount = value;
            OnFishCountChanged?.Invoke(m_fishCount);
        }
    }


    public int Highscore => m_highscore;
    public int TotalFishCount => m_totalFishCount;

    // Methods
    private void Awake()
    {
        if (m_instance)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        float score = GameFlow.Instance.PlayerMotor.transform.position.z * m_scoreDistanceMultiplier;
        score += m_fishCount * m_scorePointsPerFish;

        float scoreDiff = score - Score;
        if(scoreDiff > 1.0f)
        {
            Score = Mathf.FloorToInt(score);
        }
    }

    public void CollectFish()
    {
        ++FishCount;
    }

    public void ResetSession()
    {
        Score = 0;
        FishCount = 0;
    }
}

