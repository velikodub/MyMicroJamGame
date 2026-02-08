using UnityEngine;

public class RocketPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player");
            PlayerRocket player = collision.GetComponent<PlayerRocket>();
            if (player!=null && !player.hasRocket)
            {
                Debug.Log("pickUp");
                player.PickUpRocket();
                Destroy(gameObject);
            }
        }
    }
}
