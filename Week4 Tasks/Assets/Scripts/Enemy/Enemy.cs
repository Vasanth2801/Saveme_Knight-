using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    Rigidbody2D rb;
    bool isChasing;
    Animator animator;

    [Header("Enemy Settings")]
    [SerializeField] float speed = 4f;
    [SerializeField] int facingDirection = 1;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 2;
    public LayerMask playerLayer;

    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 1f;
    private bool isAttacking = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) 
        { 
            return; 
        }

        float distance = Vector2.Distance(player.position,transform.position);

        if(distance <= attackRange)
        {
            isChasing = false;
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isRunning", false);

            if (!isAttacking)
            {
                StartCoroutine(AttackCoroutine());
            }

            return;
        }

        if(isChasing == true)
        {
            if(player.position.x > transform.position.x && facingDirection == -1 || player.position.x< transform.position.x && facingDirection == 1)
            {
                Flip();
            }
        }

        if(isChasing == true)
        {
               Chase();
        }
    }

    void Chase()
    {
        animator.SetBool("isRunning", true);
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        while (true)
        {
            if (player == null)
            {
                break;
            }

            float distance = Vector2.Distance(player.position, transform.position);
            if (distance > attackRange)
            {
                break;
            }

            Attack();

            yield return new WaitForSeconds(attackCooldown);
        }

        isAttacking = false;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");   
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach(Collider2D hitCollider in hitPlayer)
        {
            if(player == null)
            { 
                continue; 
            }

            Vector2 pushDir = (player.position - transform.position);
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.KnockBack(pushDir);
            }

            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.PlayerDamage(5);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isRunning", false);
            isChasing = false;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnDrawGizmos()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}