using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GameEntity"))
        {
            if(!GameManager.IsGameover)
            {
                gameManager.GameOver(false);
            }
            Destroy(other.gameObject);
        }
    }
}
