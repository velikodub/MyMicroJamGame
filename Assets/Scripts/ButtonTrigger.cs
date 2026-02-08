using System.Runtime.CompilerServices;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private Block[] blocks;

    private bool playerNearby = false;

    private void Update()
    {
        if(playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Move();
        }
    }
    private void Move()
    {
        foreach(var block in blocks)
        {
            if (block.Script != null)
            {
                if (block.isToward)
                {
                    block.Script.MoveTowards(block.Towards);
                }
                else
                {
                    block.Script.MoveOffset(block.Offset);
                }
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
[System.Serializable]
public struct Block
{
    public MovableObject Script;
    public Vector3 Offset;
    public Vector3 Towards;
    public bool isToward;
}