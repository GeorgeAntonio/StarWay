using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackRange = 10f;
    public float projectileSpeed = 5f;
    public float attackDelay = 1f;
    public int maxHealth = 100;
    private int currentHealth;
    private GameObject player;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackDelay)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.transform.position - firePoint.position).normalized;
        rb.velocity = direction * projectileSpeed;
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
