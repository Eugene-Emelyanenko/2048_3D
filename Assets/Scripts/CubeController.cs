using UnityEngine;
using UnityEngine.EventSystems;

public class CubeController : MonoBehaviour
{
    [Header("Cube Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxDragDistance = 3f;
    [SerializeField] private float forceImpulse = 10f;

    [Header("Spawn Settings")]
    [SerializeField] private Transform cubeSpawnPoint;
    [SerializeField] private CubeFactory cubeFactory;
    [SerializeField] private float timeToSpawnCube = 1f;

    private IMovable currentEntity;
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
        HandleEditorInput();
#else
        HandleTouchInput();
#endif
    }

    private void HandleEditorInput()
    {
        if (PauseManager.IsPaused || GameManager.IsGameover) return;

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
            BeginDrag(Input.mousePosition);

        if (Input.GetMouseButton(0))
            ContinueDrag(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
            EndDrag();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 0 || PauseManager.IsPaused || GameManager.IsGameover) return;

        Touch touch = Input.GetTouch(0);

        if (IsPointerOverUI(touch.fingerId)) return;

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

    private void BeginDrag(Vector2 touchPos)
    {
        if (currentEntity == null) return;

        isDragging = true;
        startTouchPos = touchPos;
        cubeStartPos = currentEntity.GetPos();
    }

    private void ContinueDrag(Vector2 touchPos)
    {
        if (!isDragging || currentEntity == null) return;

        float deltaX = (touchPos.x - startTouchPos.x) / Screen.width * moveSpeed;
        Vector3 newPos = cubeStartPos + new Vector3(deltaX, 0f, 0f);

        newPos.x = Mathf.Clamp(newPos.x, -maxDragDistance, maxDragDistance);
        currentEntity.SetPos(newPos);
    }

    private void EndDrag()
    {
        if (!isDragging || currentEntity == null) return;

        isDragging = false;
        currentEntity.AddForce(Vector3.forward, forceImpulse);
        currentEntity = null;

        Invoke(nameof(SpawnNewCube), timeToSpawnCube);
    }

    private void SpawnNewCube()
    {
        GameObject newObject = cubeFactory.SpawnCube(cubeSpawnPoint.position);

        if (newObject.TryGetComponent(out IMovable movable))
        {
            currentEntity = movable;
        }
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    private bool IsPointerOverUI(int fingerId)
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(fingerId);
    }
}
