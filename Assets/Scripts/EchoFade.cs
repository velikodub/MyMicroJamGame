using UnityEngine;

public class EchoFade : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1.5f;
    private SpriteRenderer sr;
    private Color startColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;
    }

    private void Update()
    {
        float fadeSpeed = 1f / lifeTime;
        Color newColor = sr.color;
        newColor.a -= fadeSpeed * Time.deltaTime;
        sr.color = newColor;

        if(newColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
