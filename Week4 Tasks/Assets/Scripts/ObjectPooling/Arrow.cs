using Unity.Cinemachine;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;
    private float Timer;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        Timer = lifeTime;
        rb.linearVelocity = transform.right * speed;
    }

    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            var enemyHealth = other.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(5);
            }
        }
        
        if(other.gameObject.CompareTag("Boss"))
        {
            var bossHealth = other.GetComponent<BossHealth>();
            if(bossHealth != null)
            {
                bossHealth.BossTakeDamge(5);
            }
        }

        gameObject.SetActive(false);
    }
}
