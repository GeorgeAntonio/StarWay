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
        // Se você também quiser excluir outros objetos além do jogador, adicione condições adicionais aqui.
        if (!collision.collider.CompareTag("Player"))
        {
            // Se a bala atingir o inimigo, cause dano
            if (collision.collider.CompareTag("Enemy"))
            {
                // Acessa o script EnemyMelee do objeto que a bala atingiu e chama o método TakeDamage
                EnemyMelee enemy = collision.collider.GetComponent<EnemyMelee>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }

            // Destrua a bala após a colisão
            Destroy(gameObject);
        }
    }
}
