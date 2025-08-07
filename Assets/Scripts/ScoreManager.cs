using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public int Score { get; private set; }

    private int bestScore = 0; 

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        UpdateScoreText();
    }

    public void AddScore(int value)
    {
        Score += value;
        UpdateScoreText();

        if(Score > bestScore)
        {
            bestScore = Score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {Score}";
    }
}
