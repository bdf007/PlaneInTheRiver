using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    public float horizontalSpeed = 3f;
    public float verticalSpeed = 1.5f;
    public float horizontalLimit = 2.8f;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float bulletDestroyTime = 3f;
    private bool fired = false;

    private Rigidbody2D playerRb;

    private void Awake()
    {
        //get the rigidbody component
        playerRb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        // move the player
        playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * horizontalSpeed, verticalSpeed);

        // limit the player's movement
        if (transform.position.x < -horizontalLimit)
        {
            transform.position = new Vector2(-horizontalLimit, transform.position.y);
        }
        else if (transform.position.x > horizontalLimit)
        {
            transform.position = new Vector2(horizontalLimit, transform.position.y);
        }

        // shoot a bullet
        if (Input.GetAxis("Fire1") == 1f)
        {
            if (!fired)
            {
                fired = true;
                Fire();
            }
            
        }
        else
        {
            fired = false;
        }

    }

    void Fire()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.SetParent(transform.parent);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
        Destroy(bullet, bulletDestroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the enemy is hit by a bullet
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Enemy"))
        {
            // destroy the bullet
            Destroy(collision.gameObject);
            // destroy the Player
            Destroy(gameObject);
        }
    }
}
