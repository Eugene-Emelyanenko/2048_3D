using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private TextMeshProUGUI panelText;

    public static bool IsGameover { get; private set; }

    private void Start()
    {
        IsGameover = false;
        gameoverPanel.SetActive(false);
    }

    public void GameOver(bool isWin)
    {
        IsGameover = true;
        switch (isWin)
        {
            case true:
                AudioManager.Instance.PlaySFX("Win");
                gameoverPanel.SetActive(true);
                panelText.text = "You Win!";
                break;
            case false:
                AudioManager.Instance.PlaySFX("Lose");
                gameoverPanel.SetActive(true);
                panelText.text = "You Lose!";
                break;
        }
    }
}
