using UnityEngine;

public class cameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Movement")]
    [Tooltip("Determines how smoothly the camera moves towards the target position.")]
    [Range(0f, 50f)]
    public float smoothness = 0.5f;

    [Tooltip("Determines the speed at which the camera moves towards the target position.")]
    public float speed = 10f;

    [Header("Offset")]
    public Vector3 offset;

    private Vector3 currentVelocity;

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
    }
}
