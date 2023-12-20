using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;  // Graus por segundo
    public float boostMultiplier = 2f;  // Multiplicador de velocidade ao pressionar Shift
    public AudioClip thrusterSound;     // Clipe de áudio para os propulsores

    private Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Rotacionar a nave
        float turnAmount = -Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(0, 0, turnAmount);

        // Mover a nave
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveAmount *= boostMultiplier;
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrusterSound);
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        rb.velocity = transform.up * moveAmount;
    }
}
