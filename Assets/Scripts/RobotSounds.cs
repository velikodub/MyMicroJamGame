using UnityEngine;
using UnityEngine.Rendering;

public class RobotSounds : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private AudioClip scanSound;
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioClip explodeSound;
    [Header("AudioSources")]
    [SerializeField] private AudioSource mainSource;
    [SerializeField] private AudioSource moveSource;

    private void Awake()
    {
        mainSource = GetComponent<AudioSource>();
        moveSource = gameObject.AddComponent<AudioSource>();
        moveSource.clip = moveSound;
        moveSource.loop = true;
        moveSource.volume = 0.5f;
        moveSource.playOnAwake = false;
    }
    public void PlayJump()
    {
        mainSource.PlayOneShot(jumpSound);
    }
    public void PlayLand()
    {
        mainSource.pitch = Random.Range(0.9f, 1.1f);
        mainSource.PlayOneShot(landSound);
        mainSource.pitch = 1f;
    }
    public void PlayScan()
    {
        mainSource.PlayOneShot(scanSound);
    }
    public void SetMoving(bool isMoving)
    {
        if (isMoving)
        {
            if (!moveSource.isPlaying) moveSource.Play();
        }
        else
        {
            if (moveSource.isPlaying) moveSource.Stop();
        }
    }
    public void PlayExplosion(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(explodeSound, position);
    }
}
