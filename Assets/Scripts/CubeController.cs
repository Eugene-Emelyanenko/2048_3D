using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxDragDistance = 3f;
    [SerializeField] private float forceImpulse = 10f;
    [SerializeField] private Transform cubeSpawnPoint;
    [SerializeField] private CubeSpawner cubeSpawner;
    [SerializeField] private float timeToSpawnCube = 1f;

    private Cube currentCube;
    private bool isDragging = false;
    private Vector2 startTouchPos;
    private Vector3 cubeStartPos;

    private void Start()
    {
        SpawnNewCube();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            BeginDrag(Input.mousePosition);
        if (Input.GetMouseButton(0))
            ContinueDrag(Input.mousePosition);
        if (Input.GetMouseButtonUp(0))
            EndDrag();
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    BeginDrag(touch.position);
                    break;
                case TouchPhase.Moved:
                    ContinueDrag(touch.position);
                    break;
                case TouchPhase.Ended:
                    EndDrag();
                    break;
            }
        }
#endif
    }

    private void BeginDrag(Vector2 touchPos)
    {
        if (currentCube == null) return;

        isDragging = true;
        startTouchPos = touchPos;
        cubeStartPos = currentCube.transform.position;
    }

    private void ContinueDrag(Vector2 touchPos)
    {
        if (!isDragging || currentCube == null) return;

        float deltaX = (touchPos.x - startTouchPos.x) / Screen.width * moveSpeed;
        Vector3 newPos = cubeStartPos + new Vector3(deltaX, 0, 0);

        newPos.x = Mathf.Clamp(newPos.x, -maxDragDistance, maxDragDistance);

        currentCube.transform.position = newPos;
    }

    private void EndDrag()
    {
        if (!isDragging || currentCube == null) return;

        isDragging = false;
        currentCube.Throw(forceImpulse);

        currentCube = null;
        Invoke(nameof(SpawnNewCube), timeToSpawnCube);
    }

    private void SpawnNewCube()
    {
        if (cubeSpawner.SpawnCube(cubeSpawnPoint.position).TryGetComponent(out Cube cube))
        {
            currentCube = cube;
            cube.Init();
        }
    }
}
