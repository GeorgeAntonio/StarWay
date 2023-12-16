using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float patrolSpeed = 2.0f;
    public float chaseSpeed = 4.0f;
    public float chaseDistance = 5.0f;
    public float attackDistance = 1.0f;
    public float jumpForce = 7.0f;
    public int maxHealth = 100;
    public int damage = 10;
    private int currentHealth;
    private GameObject player;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 patrolPointA;
    private Vector2 patrolPointB;
    private Vector2 nextPatrolPoint;
    private Animator animator;
    private bool facingRight = true;
    public float attackCooldown = 2.0f;  // Tempo de espera entre os ataques
    public float knockbackForce = 5.0f;  // Força do knockback
    private float lastAttackTime = 0;
    public AudioClip huntingSound;  // O clipe de áudio para caça
    private AudioSource audioSource;  // Componente AudioSource


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Define patrol points based on current position
        patrolPointA = transform.position;
        patrolPointB = transform.position + new Vector3(5, 0, 0); // 5 units to the right
        nextPatrolPoint = patrolPointB;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < chaseDistance && distanceToPlayer > attackDistance)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= attackDistance)
        {
            AttackPlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextPatrolPoint, patrolSpeed * Time.deltaTime);
        CheckFlip(nextPatrolPoint.x - transform.position.x);
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsHunting", false);
        if (Vector2.Distance(transform.position, nextPatrolPoint) < 0.1f)
        {
            if (nextPatrolPoint == patrolPointA)
            {
                nextPatrolPoint = patrolPointB;
            }
            else
            {
                nextPatrolPoint = patrolPointA;
            }
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
        CheckFlip(player.transform.position.x - transform.position.x);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsHunting", true);
        if (player.transform.position.y > transform.position.y + 1f && isGrounded)
        {
            Jump();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(huntingSound);
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
    }
    void CheckFlip(float horizontalMove)
    {
        if (horizontalMove > 0 && !facingRight || horizontalMove < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void AttackPlayer()
    {
        // Aplicar dano ao jogador
        player.GetComponent<Player>().TakeDamage(damage);

        // Aplicar knockback
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
        playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.gameObject == player)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer(); // Chamada ajustada
                lastAttackTime = Time.time;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
