using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private int facingDirection = 1;
    private float baseSpeed;

    [Header("Reference Settings")]
    Rigidbody2D rb;
    PlayerController controller;
    Vector2 movement;
    Animator animator;
    [SerializeField] TrailRenderer trailRenderer;

    [Header("AttackReferences")]
    [SerializeField] Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 10;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 7f;
    [SerializeField] private float dashDuration = 0.05f;
    [SerializeField] private float dashCoolDown = 3f;
    bool isDashing = false;
    bool canDash = true;
    bool dashPressed = false;

    [Header("Knockback Settings")]
    [SerializeField] private float knockBackForce = 8f;
    [SerializeField] private float knockBackDuration = 0.1f;
    bool isKnocked = false;

    [Header("Powerup Settings")]
    public DamagePowerup powerupEffect;

    [Header("RangedSettings")]
    [SerializeField] Transform firePoint;
    [SerializeField] int rangedAttackDamage = 5;

    [Header("Player Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] float currentStamina;
    [SerializeField] Slider staminaBar;
    [SerializeField] float runCost = 10f;
    [SerializeField] float staminaRegenRate = 5f;

    [Header("Shield Settings")]
    [SerializeField] bool isShielded = false;
    [SerializeField] GameObject shieldObject;
    [SerializeField] float ShieldDuration = 1f;
    [SerializeField] float shieldCoolDown = 5f;
    [SerializeField] bool canShield = true;

    void Awake()
    { 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        controller = new PlayerController();
        MovementCalling();
        Dashing();

      
        baseSpeed = speed;

        currentStamina = maxStamina;
        staminaBar.value = currentStamina / maxStamina;
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

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            ActivateCurrentPower();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if(canShield && !isShielded)
        {
            CheckShield();
        }
    }

    private void FixedUpdate()
    {
        if(isDashing || isKnocked)
        {
            return;
        }
         Move();
         Attack();

        if((canDash == true && dashPressed == true) && currentStamina >= 0)
        {
            dashPressed = false;
            StartCoroutine(Dash());
            Debug.Log("Started Dashing");
        }
    }

    void CheckShield()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            shieldObject.SetActive(true);
            isShielded = true;
            NoShield();
        }
    }

    void NoShield()
    {
        StartCoroutine(ShieldTime());
    }

    private void Move()
    {
        Vector2 move = (rb.position + movement * speed * Time.deltaTime);
        rb.MovePosition(move);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Horizontal", movement.y);
        animator.SetFloat("Horizontal", movement.sqrMagnitude);
        currentStamina -= movement.sqrMagnitude * runCost * Time.deltaTime;

        if(currentStamina <= 0f)
        {
            currentStamina = 0f;
            speed = minSpeed;
        }
        else
        {
            if (speed < baseSpeed)
            {
                speed = baseSpeed;
            }
        }

        staminaBar.value = currentStamina / maxStamina;

        if(currentStamina < maxStamina && movement.sqrMagnitude == 0)
        {
            StartCoroutine(RegenerateStamina());
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach(Collider2D hit in hitEnemies)
            {
                if(isShielded)
                {
                    Debug.Log("Player is shielded, no damage taken");
                    return;
                }

                var eh = hit.GetComponent<EnemyHealth>();
                if (eh != null)
                {
                    eh.TakeDamage(attackDamage);
                    Debug.Log("Damage done to enemy ");
                }

                var bh = hit.GetComponent<BossHealth>();
                if (bh != null)
                {
                    bh.BossTakeDamge(attackDamage);
                    Debug.Log("Damage done to boss ");
                }
            }
        }
    }

    void  Shoot()
    {
        ObjectPooler.Instance.SpawnObjects("Projectile", firePoint.position, firePoint.rotation);
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        if(firePoint != null)
        {
            firePoint.Rotate(0, 180, 0);
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        rb.AddForce(movement * dashSpeed, ForceMode2D.Impulse);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        rb.linearVelocity = Vector2.zero;
        trailRenderer.emitting = false;
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

    public void KnockBack(Vector2 hitDirection)
    {
        if(isKnocked)
        {
            return;
        }

        StartCoroutine(KnockBackRoutine(hitDirection));
    }

    IEnumerator KnockBackRoutine(Vector2 hitDirection)
    {
        isKnocked = true;
        rb.linearVelocity = movement.normalized * knockBackForce;
        yield return new WaitForSeconds(knockBackDuration);
        rb.linearVelocity = Vector2.zero;
        isKnocked = false;
    }

    public void ApplyPowerup(SpeedPowerup speedPowerup)
    {
        speedPowerup.ApplyPowerup(this.gameObject);

        var TimePowerup = speedPowerup as TimePowerup;

        if(TimePowerup != null)
        {
            StartCoroutine(TimePowerup.PowerupDuration(gameObject));
        }
    }

    public void ApplyPowerup(DamagePowerup damagePowerup)
    {
        damagePowerup.ApplyPowerup(this.gameObject);
        var TimePowerup = damagePowerup as DamagaeTime;
        if (TimePowerup != null)
        {
            StartCoroutine(TimePowerup.DamageDuration(gameObject));
        }
    }

    public void StorePowerup(DamagePowerup effect)
    {
        powerupEffect = effect;
    }

    void ActivateCurrentPower()
    {
        if(powerupEffect != null)
        {
            ApplyPowerup(powerupEffect);
            powerupEffect = null;
        }
    }

    IEnumerator RegenerateStamina()
    {
       yield return new WaitForSeconds(1f);
        if(currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate / 10f;
            if(currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
            staminaBar.value = currentStamina / maxStamina;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ShieldTime()
    {
        yield return new WaitForSeconds(ShieldDuration);
        shieldObject.SetActive(false);
        isShielded = false;
        canShield = false;
        yield return new WaitForSeconds(shieldCoolDown);
        canShield = true;
    }
}