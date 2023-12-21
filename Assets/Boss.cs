using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float patrolSpeed = 1.0f;  // Velocidade de patrulha mais lenta
    public float chaseSpeed = 2.0f;   // Velocidade de perseguição mais lenta
    public float chaseDistance = 10.0f;  // Boss pode ter uma distância de perseguição maior
    public float attackDistance = 2.0f;  // Distância de ataque do boss
    public int maxHealth = 300;  // Boss geralmente tem mais vida
    public int damage = 20;  // O dano do boss pode ser maior
    private int currentHealth;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 patrolPointA;
    private Vector2 patrolPointB;
    private Vector2 nextPatrolPoint;
    private Animator animator;
    private bool facingRight = true;
    public float attackCooldown = 5.0f;  // Boss pode ter um cooldown de ataque maior
    private float lastAttackTime = 0;
    public AudioClip bossSound;  // Som específico do boss
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Define patrol points based on current position
        patrolPointA = transform.position;
        patrolPointB = transform.position + new Vector3(10, 0, 0); // Boss pode ter um ponto de patrulha mais distante
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
        animator.SetBool("IsWalking", true);  // Certifique-se de ter uma animação de caminhada para o boss
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
        CheckFlip(player.transform.position.x - transform.position.x);
        animator.SetBool("IsWalking", true);  // Reutiliza a animação de caminhada para perseguição

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(bossSound);
        }
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
        // Verifique se o tempo de ataque expirou antes de atacar novamente
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            player.GetComponent<Player>().TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }

    // Removido o método Jump do boss, já que ele não pula.

    void OnCollisionEnter2D(Collision2D collision)
    {
        // A lógica de colisão permanece a mesma para detectar impactos com balas.
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletScript bullet = collision.gameObject.GetComponent<BulletScript>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }

            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            // Adicionar lógica adicional para quando o boss morre, como tocar uma animação.
        }
    }
}
