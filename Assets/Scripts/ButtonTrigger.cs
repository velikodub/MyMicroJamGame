using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private MovableObject linkedObject;
    [SerializeField] private bool moveByBotton;
    [SerializeField] private Vector3 moveOffset;

    private bool playerNearby = false;

    private void Update()
    {
        if(playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if(linkedObject != null)
            {
                if (moveByBotton)
                {
                    linkedObject.MoveByButton(moveOffset);
                }
                else
                {
                    linkedObject.Active();
                }
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
