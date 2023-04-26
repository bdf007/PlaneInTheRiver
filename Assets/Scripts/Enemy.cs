using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    public float speed;
    public float shootInterval = 6f;
    private Rigidbody2D enemyRb;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public float bulletSpeed;

    private float shootTimer;

    private void Awake()
    {
        // get the rigidbody component
        enemyRb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // set the shoot timer random 
        shootTimer = Random.Range(0f, shootInterval);
        // move the enemy down
        enemyRb.velocity = new Vector2(0f, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        // update the shoot timer
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0f)
        {
            // reset the shoot timer
            shootTimer = shootInterval;
            // shoot a bullet
            Fire();
        }
    }

    void Fire()
    {
        // create a bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // put the bullet in the component enemy
        bullet.transform.SetParent(transform.parent);
        // put the bullet at the front of the enemy
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        // set the bullet's velocity
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -bulletSpeed);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the enemy is hit by a bullet
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            // destroy the bullet
            gameObject.SetActive(false);
            // destroy the enemy
            Destroy(gameObject);
        }
    }
}
