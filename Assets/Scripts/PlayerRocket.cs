using UnityEngine;

public class PlayerRocket : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject rocketProjectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireSpeed = 10f;

    public bool hasRocket { get; private set; } = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && hasRocket)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        hasRocket = false;
        GameObject rocket = Instantiate(rocketProjectilePrefab, firePoint.position, Quaternion.Euler(0f, 0f, transform.localScale.x > 0 ? -90f : 90f));
        float direction = transform.localScale.x > 0 ? 1 : -1;
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.linearVelocity = new Vector2(direction * fireSpeed, 0);
        }
    }
    public void PickUpRocket()
    {
        hasRocket = true;
        RobotSounds robotSounds = gameObject.GetComponent<RobotSounds>();
        if (robotSounds)
        {
            robotSounds.PickUp();
        }
        Debug.Log("Rocket Acquired!");
    }
}
