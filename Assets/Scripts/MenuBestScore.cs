using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuBestScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private void Start()
    {
        bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt("BestScore", 0)}";
    }
}
