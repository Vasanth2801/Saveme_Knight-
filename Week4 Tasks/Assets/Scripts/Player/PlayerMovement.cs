using System.Collections;
using Unity.XR.GoogleVr;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private int facingDirection = 1;

    [Header("Reference Settings")]
    Rigidbody2D rb;
    PlayerController controller;
    Vector2 movement;
    Animator animator;
    private EnemyHealth enemyHealth;
    [SerializeField] TrailRenderer trailRenderer;

    [Header("AttackReferences")]
    [SerializeField] Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    public LayerMask enemyLayer;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 7f;
    [SerializeField] private float dashDuration = 0.05f;
    [SerializeField] private float dashCoolDown = 3f;
    bool isDashing = false;
    bool canDash = true;
    bool dashPressed = false;
    

    void Awake()
    {
        enemyHealth = FindObjectOfType<EnemyHealth>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        controller = new PlayerController();
        MovementCalling();
        Dashing();
    }

    void MovementCalling()
    {
        controller.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controller.Player.Move.canceled += ctx => movement = Vector2.zero;
    }

    void Dashing()
    {
        controller.Player.Dash.performed += ctx => dashPressed = true;
        {
            Debug.Log("Button Pressed");
        };
        
    }

    private void OnEnable()
    {
        controller.Player.Enable();
    }

    private void OnDisable()
    {
        controller.Player.Disable();
    }

    private void Update()
    {
        
        if (movement.x > 0 && transform.localScale.x < 0 || movement.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
         Move();
         Attack();

        if(canDash == true && dashPressed == true)
        {
            dashPressed = false;
            StartCoroutine(Dash());
            Debug.Log("Started Dashing");
        }
    }

    private void Move()
    {
        Vector2 move = (rb.position + movement * speed * Time.deltaTime);
        rb.MovePosition(move);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Horizontal", movement.y);

        animator.SetFloat("Horizontal", movement.sqrMagnitude);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D hit in hitEnemies)
            {
                enemyHealth.TakeDamage(10);
                Debug.Log("Damage done to enemy ");
            }
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        rb.AddForce(movement * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashDuration);
        rb.linearVelocity = Vector2.zero;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}