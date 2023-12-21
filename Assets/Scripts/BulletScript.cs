using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float bulletSpeed;
    public int damage;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se a bala colidir com qualquer coisa que não seja o jogador, ela se destruirá.
        if (!collision.collider.CompareTag("Player"))
        {
            // Se a bala atingir o inimigo, cause dano
            if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Boss")) // Adicionada verificação para "Boss"
            {
                var enemy = collision.collider.GetComponent<EnemyMelee>(); // Isso pode ser um problema se o boss não tiver o script EnemyMelee
                var boss = collision.collider.GetComponent<Boss>(); // Adicione esta linha se o boss tiver um script diferente
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                else if (boss != null) // E esta condicional
                {
                    boss.TakeDamage(damage); // Chama TakeDamage no boss
                }
            }

            // Destrua a bala após a colisão
            Destroy(gameObject);
        }
    }
}
