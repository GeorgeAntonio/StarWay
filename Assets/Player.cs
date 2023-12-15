using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public float runMultiplier = 2.0f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public int maxHealth = 100;
    private int currentHealth;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
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

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        animator.SetBool("IsShooting", false);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // Tratar morte do jogador
        }
    }
}
