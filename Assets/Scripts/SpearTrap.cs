using System.Collections;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    [SerializeField] private int damage;

    [Header("Speartrap timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activationTime;
    public float knockbackForce = 5.0f;  // Força do knockback
    private GameObject player;
    private Animator anim;
    private AudioSource trapSound;

    private bool triggered;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        trapSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "RegionTrigger")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateSpearTrap(collision));
            }
        }
    }

    private IEnumerator ActivateSpearTrap(Collider2D collision)
    {
        triggered = true;
        yield return new WaitForSeconds(activationDelay);

        anim.SetBool("active", true);
        trapSound.Play();

        player.GetComponent<Player>().TakeDamage(damage);

        // Aplicar knockback
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
        playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(activationTime);
        anim.SetBool("active", false);
        triggered = false;
    }
}
