using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private MovableObject linkedObject;

    private bool playerNearby = false;

    private void Update()
    {
        if(playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if(linkedObject != null)
            {
                linkedObject.Active();
                Debug.Log("Button Pressed!");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
