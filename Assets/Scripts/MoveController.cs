using System.Collections;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _moveDistance = 10f;
    [SerializeField] private float _swipeThreshold = 0.1f;

    private Vector2 _touchStartPos;
    private int collisionCount = 0;
    private GameController _gameController;
    private Rigidbody _rb;
    private bool isSelected = false;
    private bool hasSwiped = false;
    private bool canCollide = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTap(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
            hasSwiped = false;
        }

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                HandleTap(Input.touches[0].position);
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isSelected = false;
                hasSwiped = false;
            }
        }

        if (isSelected && !hasSwiped)
        {
            Vector2 swipeDelta;

            if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - _touchStartPos;
            }
            else if (Input.touchCount > 0)
            {
                swipeDelta = Input.touches[0].position - _touchStartPos;
            }
            else
            {
                return;
            }

            if (swipeDelta.magnitude >= _swipeThreshold)
            {
                hasSwiped = true;

                float moveX = Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y) ? Mathf.Sign(swipeDelta.x) : 0f;
                float moveZ = Mathf.Abs(swipeDelta.x) <= Mathf.Abs(swipeDelta.y) ? Mathf.Sign(swipeDelta.y) : 0f;

                float adjustedMoveDistance = _moveDistance;

                Vector3 moveDirection = new Vector3(moveX * adjustedMoveDistance, 0f, moveZ * adjustedMoveDistance);
                Vector3 newPosition = transform.position + moveDirection;

                _rb.MovePosition(newPosition);
            }
        }
    }

    void HandleTap(Vector2 screenPos)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isSelected = true;
                _touchStartPos = screenPos;
            }
        }
    }

    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(2f);
        canCollide = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Alarm") && canCollide)
        {
            canCollide = false;

            collisionCount++;

            if (collisionCount == 1)
            {
                _gameController.IncreaseNoiseLevel(50);
            }
            else if (collisionCount == 2)
            {
                _gameController.IncreaseNoiseLevel(100);
            }

            StartCoroutine(EnableCollision());
        }
    }
}
