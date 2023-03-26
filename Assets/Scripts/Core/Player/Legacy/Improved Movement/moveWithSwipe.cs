using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class moveWithSwipe : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float moveSpeed = 10f;
    public int laneCount = 3;
    public float laneDistance = 3f;
    [Range(1, 10)] public int currentLane = 1;
    public bool isMoving = false;
    private bool isSwitchingLanes = false;

    [Header("Jumping Parameters")]
    public float jumpForce = 10f;
    [Range(0.05f, 3f)]public float raycastDistance = 1f;

    private Rigidbody rb;

    // Object pooling variables
    public GameObject obstaclePrefab;
    public int maxObstacles = 10;
    private List<GameObject> obstacles;
    private int obstacleIndex = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Initialize object pool
        obstacles = new List<GameObject>();
        for (int i = 0; i < maxObstacles; i++)
        {
            GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            obstacle.SetActive(false);
            obstacles.Add(obstacle);
        }
    }

    private void FixedUpdate()
{
    // Move forward at a consistent speed
    if (!isMoving)
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
    }
    else
    {
        // Add Y component back to velocity while moving
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
    }
}


    private void Update()
    {
        // Move left
        if ((Input.GetKeyDown(KeyCode.A) || swipeDetector.swipeLeft) && currentLane > 1 && !isSwitchingLanes)
        {
            MoveLane(-1);
        }

        // Move right
        if ((Input.GetKeyDown(KeyCode.D) || swipeDetector.swipeRight) && currentLane < laneCount && !isSwitchingLanes)
        {
            MoveLane(1);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) || swipeDetector.swipeUp)
        {
            Jump();
        }

        // Check for touch input on the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Jump();
            }
        }
    }

    private void MoveLane(int direction)
    {
        currentLane += direction;
        isMoving = true;
        isSwitchingLanes = true;
        Vector3 targetPosition = transform.position + Vector3.right * direction * laneDistance;
        StartCoroutine(MoveToPosition(targetPosition, 0.5f));
    }

    private void Jump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset velocity in X and Z axes
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }


IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
{
    float elapsedTime = 0;
    Vector3 startingPosition = transform.position;
    float startingX = startingPosition.x;
    float targetX = targetPosition.x;
    float distance = Mathf.Abs(startingX - targetX);
    Vector3 prevMoveDirection = transform.forward;

    while (elapsedTime < duration)
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / duration);
        Vector3 newPosition = Vector3.Lerp(startingPosition, targetPosition, t);
        float currentDistance = distance * t;
        float newX = Mathf.Lerp(startingX, targetX, currentDistance / distance);
        newPosition.x = newX;

        rb.MovePosition(newPosition);

        // Update velocity based on movement direction
        Vector3 newMoveDirection = (targetPosition - newPosition).normalized;
        Vector3 newVelocity = rb.velocity + (newMoveDirection - prevMoveDirection) * moveSpeed;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, newVelocity.z);

        prevMoveDirection = newMoveDirection;

        yield return null;
    }

    rb.MovePosition(targetPosition);
    isMoving = false;
    isSwitchingLanes = false;
}


}
