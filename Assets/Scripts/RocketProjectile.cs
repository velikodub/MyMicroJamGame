using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestructibleWall wall = collision.GetComponent<DestructibleWall>();
        if(wall != null)
        {
            wall.Explode();
            Destroy(gameObject);
            return;
        }
        if(!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
