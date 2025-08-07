using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirstCubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeFactory cubeFactory;
    [SerializeField] private Transform firstCubeSpawnPoint;

    [Range(0, 100)]
    [SerializeField] private int twoValueChance = 75;
    //[Range(0, 100)]
    //[SerializeField] private int fourValueChance = 25;

    private void Start()
    {
        cubeFactory.SpawnCube(firstCubeSpawnPoint.position).GetComponent<IGameEntity>().Init(value: GetRandomValue(), isKinematic: false);
    }

    private int GetRandomValue()
    {
        int roll = Random.Range(0, 100);        
        if (roll < twoValueChance)
            return 2;
        else
            return 4;
    }
}
