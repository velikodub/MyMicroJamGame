using UnityEngine;

public class EchoPulse : MonoBehaviour
{
    [Header("Scaner setings")]
    [SerializeField] private int raysCount = 100;
    [SerializeField] private float viewRadius = 10f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float scatterAmount = 0.2f;
    [Header("Points settings")]
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private Transform echoRoot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EmitWave();
        }
    }
    private void EmitWave()
    {
        float angleStep = 360f / raysCount;
        for(int i = 0; i < raysCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, viewRadius, obstacleLayer);

            if(hit.collider != null)
            {
                Vector2 randomOffset = Random.insideUnitCircle * scatterAmount;

                Vector2 finalPosition = hit.point + randomOffset;
                Instantiate(pointPrefab, finalPosition, Quaternion.identity, echoRoot);
            }
        }
    }
}
