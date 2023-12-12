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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;

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

        if (player.transform.position.y > transform.position.y + 1f && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
    }

    void AttackPlayer()
    {
        // Attack logic (e.g., reduce player health) goes here
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.gameObject == player)
        {
            player.GetComponent<Player>().TakeDamage(damage);
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
