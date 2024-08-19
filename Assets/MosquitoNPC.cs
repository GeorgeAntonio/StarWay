using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoNPC : MonoBehaviour
{
    public float patrolSpeed = 2.0f;
    public float chaseSpeed = 4.0f;
    public float chaseDistance = 7.0f; // Distância maior, pois o mosquito voa
    public float attackDistance = 1.5f;
    public int maxHealth = 50; // Mosquitos geralmente têm menos vida que inimigos maiores
    public int damage = 5;
    private int currentHealth;
    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    public float attackCooldown = 1.5f;  // Mosquito ataca com mais frequência
    private float lastAttackTime = 0;
    public AudioClip buzzingSound;  // Som do mosquito voando
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Toca o som de zumbido continuamente
        if (buzzingSound != null)
        {
            audioSource.clip = buzzingSound;
            audioSource.loop = true;
            audioSource.Play();
        }
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
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
        CheckFlip(player.transform.position.x - transform.position.x);
        animator.SetBool("IsChasing", true);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject == player)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }

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
        Debug.Log("Mosquito current health: " + currentHealth);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
