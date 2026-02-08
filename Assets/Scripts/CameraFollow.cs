using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Settings")]
    [SerializeField] float smoothTime = 0.25f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
