using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    [Header("Explosion settings")]
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject shrapnelPrefab;
    [SerializeField] private int shrapnelCount = 30;
    [Range(0f, 45f),SerializeField] private float angleRandomness = 5f;

    public void Explode()
    {
        if(explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        SpawnBlastWave();

        RobotSounds sounds = FindAnyObjectByType<RobotSounds>();
        if(sounds != null)
        {
            sounds.PlayExplosion(transform.position);
        }
        Destroy(gameObject);
    }
    private void SpawnBlastWave()
    {
        float angleStep = 360f / shrapnelCount;

        for(int i = 0; i < shrapnelCount; i++)
        {
            float baseAngle = i * angleStep;

            float RandomOffset = Random.Range(-angleRandomness, angleRandomness);
            float finalAngle = baseAngle + RandomOffset;

            Quaternion rotation = Quaternion.Euler(0, 0, finalAngle);

            Instantiate(shrapnelPrefab, transform.position, rotation);
        }
    }
}
