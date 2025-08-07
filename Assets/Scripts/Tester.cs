using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private Transform firstCubeTransform;
    [SerializeField] private Transform secondCubeTransform;

    [SerializeField] private Transform thirdCubeTransform;

    [SerializeField] private CubeFactory cubeFactory;

    public void CreateTwoCubes()
    {
        cubeFactory.SpawnCube(firstCubeTransform.position, 1024);
        cubeFactory.SpawnCube(secondCubeTransform.position, 1024);
    }

    public void CreateLoseCube()
    {
        cubeFactory.SpawnCube(thirdCubeTransform.position, 2);
    }
}
