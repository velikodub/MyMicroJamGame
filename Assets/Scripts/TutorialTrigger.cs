using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [TextArea(3, 5)]
    [SerializeField] private string[] message;

    private bool hasTriggered = false;

    [SerializeField] private bool isOneTimeUse = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            TutorialManager.Instance.StartDialogue(message);

            if(isOneTimeUse)    Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && hasTriggered)
        {
            if(!isOneTimeUse) hasTriggered = false;
        }
    }
}
