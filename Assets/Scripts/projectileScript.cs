using System.Collections;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lifeTime = 5f;
    private float timeAlive = 0f;
    public float knockbackForce = 5.0f;  // Força do knockback
    private GameObject player;
    [SerializeField] private int damage;
    private AudioSource hitSound;

    private void Start()
    {
        hitSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.position -= transform.up * moveSpeed * Time.deltaTime;
        timeAlive += Time.deltaTime;

        if (timeAlive >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player"){
            Debug.Log("Projectile hit player - applying damage");

            // Aplica dano ao jogador
            player.GetComponent<Player>().TakeDamage(damage);

            // Aplicar knockback
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
            playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            // Move a flecha para longe da cena (para fora do viewport)
            transform.position = new Vector3(transform.position.x, transform.position.y, -100f);

            // Reproduz o som do impacto e destr�i a flecha
            if (hitSound != null)
            {
                StartCoroutine(PlayHitSoundAndDestroy());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator PlayHitSoundAndDestroy()
    {
        hitSound.Play();
        yield return new WaitForSeconds(hitSound.clip.length);
        Destroy(gameObject);
    }
}
