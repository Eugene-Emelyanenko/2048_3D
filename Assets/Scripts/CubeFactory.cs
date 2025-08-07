using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private MergeManager mergeManager;

    public GameObject SpawnCube(Vector3 position, int value = 2, bool applyRandomForce = false)
    {
        GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);

        if (cube.TryGetComponent(out IGameEntity entity))
        {
            entity.Init(value, isKinematic: false);
        }

        if (cube.TryGetComponent(out IMergable mergeHandler))
        {
            mergeManager.Register(mergeHandler);
        }

        if (cube.TryGetComponent(out IMovable movable) && applyRandomForce)
        {
            ApplyRandomForce(movable);
        }

        return cube;
    }

    private void ApplyRandomForce(IMovable movable)
    {
        Vector3 randomDir = Vector3.up + new Vector3(
            Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));

        float randomForce = Random.Range(4f, 7f);
        movable.AddForce(randomDir.normalized, randomForce);

        Vector3 randomTorque = new Vector3(
            Random.Range(-10f, 10f),
            Random.Range(-10f, 10f),
            Random.Range(-10f, 10f)
        );
        movable.AddTorque(randomTorque);
    }
}
