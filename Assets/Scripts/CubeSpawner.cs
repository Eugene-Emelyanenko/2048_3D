using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Transform firstCubeSpawnPoint;
    [SerializeField] private GameObject cubePrefab;

    [Range(0, 100)]
    [SerializeField] private int twoValueChance = 75;
    [Range(0, 100)]
    [SerializeField] private int fourValueChance = 25;

    private void Start()
    {
        if(SpawnCube(firstCubeSpawnPoint.position).TryGetComponent(out Cube cube))
        {
            cube.Init(value: GetRandomValue(), isKinematic: false);
        }
    }

    public GameObject SpawnCube(Vector3 pos)
    {
        return Instantiate(cubePrefab, pos, Quaternion.identity);
    }

    private int GetRandomValue()
    {
        int roll = Random.Range(0, 100);
        if (roll < twoValueChance)
            return 2;
        else
            return 4;
    }

    private void OnCubeMerge(int newValue, Vector3 MergePos)
    {
        if(SpawnCube(MergePos).TryGetComponent(out Cube cube))
        {
            cube.Init(value: newValue, isKinematic: false);
        }
    }

    private void OnEnable()
    {
        Cube.onCubeMerge += OnCubeMerge;
    }

    private void OnDisable()
    {
        Cube.onCubeMerge -= OnCubeMerge;
    }
}
