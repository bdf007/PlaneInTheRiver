using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float horizontalSpeed = 3f;
    public float verticalSpeed = 1.5f;
    public float horizontalLimit = 2.8f;

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
    }
}
