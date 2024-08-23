using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public float runMultiplier = 2.0f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public int maxHealth = 100;
    private int currentHealth = 100;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true;
    private Animator animator;
    private LevelCompleteManager levelCompleteManager;
    public AudioClip shootingSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        levelCompleteManager = FindObjectOfType<LevelCompleteManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        bool isWalking = move != 0;
        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
        bool isShooting = Input.GetButtonDown("Fire1");
        bool isJumping = Input.GetKeyDown(KeyCode.Space) && isGrounded;

        // Atualizar o Animator
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsShooting", isShooting);

        // Movimento horizontal
        rb.velocity = new Vector2(move * (isRunning ? speed * runMultiplier : speed), rb.velocity.y);

        // Pular
        if (isJumping)
        {
            Jump(move);
        }

        // Atirar
        if (isShooting)
        {
            Shoot();
        }

        // Virar o sprite, se necessário
        if (move > 0 && !facingRight || move < 0 && facingRight)
        {
            Flip();
        }
        if (isGrounded)
        {
            animator.SetBool("IsJumpingUpward", false);
            animator.SetBool("IsJumpingForward", false);
        }
    }

    void Jump(float move)
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;

        bool isJumpingUpward = move == 0;
        bool isJumpingForward = move != 0;

        animator.SetBool("IsJumpingUpward", isJumpingUpward);
        animator.SetBool("IsJumpingForward", isJumpingForward);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        // Inverte a direção do bulletSpawn quando o jogador vira
        bulletSpawn.Rotate(0f, 180f, 0f);
    }

    void Shoot()
    {
        // Verifica a última direção que o jogador estava voltado e ajusta o bulletSpawn antes de atirar
        if ((facingRight && bulletSpawn.eulerAngles.y > 0f) || (!facingRight && bulletSpawn.eulerAngles.y == 0f))
        {
            bulletSpawn.Rotate(0f, 180f, 0f);
        }

        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        //animator.SetBool("IsShooting", false);
        audioSource.PlayOneShot(shootingSound);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Player current health: " + currentHealth);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // Tratar morte do jogador
            Die();
        }
    }

    public int GetCurrentHealth()  // Adicionando este método
    {
        return currentHealth;
    }

    private void Die()
    {
        Debug.Log("Die method called.");
        // Desativa o jogador ou qualquer outra lógica de morte
        gameObject.SetActive(false);

        // Chama o método no LevelCompleteManager para mostrar o painel de habitabilidade como 0%
        if (levelCompleteManager != null)
        {
            levelCompleteManager.PlayerDied();
        }
        else
        {
            Debug.LogError("LevelCompleteManager not found.");
        }
    }
}
