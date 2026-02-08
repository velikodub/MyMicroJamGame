using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MovableObject : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector3 moveOffset;
    [SerializeField] private float speed = 2f;

    [Header("Sound")]
    [SerializeField] private AudioClip moveSound;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isActivated = false;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.spatialBlend = 0f;

        audioSource.minDistance = 2f;
        audioSource.maxDistance = 15f;
    }

    private void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveOffset;
        if (moveSound != null)
        {
            audioSource.clip = moveSound;
        }
    }
    private void Update()
    {
        Vector3 dest = isActivated ? targetPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, dest) > 0.01f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
    public void MoveByButton(Vector3 to)
    {
        targetPos = to;
        Active();
    }
    public void Active()
    {
        isActivated = !isActivated;
    }
}
