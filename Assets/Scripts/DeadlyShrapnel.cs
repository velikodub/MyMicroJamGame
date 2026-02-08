using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadlyShrapnel : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float minLifeTime = 2f;
    [SerializeField] private float maxLifeTime = 5f;

    private SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        float lifeTime = Random.Range(minLifeTime, maxLifeTime);
        StartCoroutine(FadeAndDestroy(lifeTime));
    }
    private IEnumerator FadeAndDestroy(float lifeTime)
    {
        float timer = lifeTime;
        while (timer > 0f)
        {
            float alpha = timer/lifeTime;

            Color newColor = sr.color;
            newColor.a = alpha;
            sr.color = newColor;

            timer -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Wasted by Explosion!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
