using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MergeManager : MonoBehaviour
{
    [SerializeField] private CubeFactory cubeFactory;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private int maxMergeValue = 2048;

    private readonly List<IMergable> mergeHandlers = new();

    public void Register(IMergable handler)
    {
        mergeHandlers.Add(handler);
        handler.OnMerged += HandleMerge;
        handler.OnDestroyed += () => Unregister(handler);
    }

    private void Unregister(IMergable handler)
    {
        if (mergeHandlers.Contains(handler))
        {
            handler.OnMerged -= HandleMerge;
            mergeHandlers.Remove(handler);
        }
    }

    private void HandleMerge(int newValue, Vector3 position)
    {
        if (GameManager.IsGameover)
            return;

        AudioManager.Instance.PlaySFX("Impact");
        cubeFactory.SpawnCube(position, newValue, true);
        scoreManager.AddScore(newValue / 4);

        if(newValue >= maxMergeValue)
        {           
            gameManager.GameOver(true);
        }
    }
}
