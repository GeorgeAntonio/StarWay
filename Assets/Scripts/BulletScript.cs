using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float bulletSpeed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name != "Player")
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy (gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }
}
