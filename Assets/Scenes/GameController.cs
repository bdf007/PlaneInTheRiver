using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPrefab;
    private GameObject mainCamera;
    public float spawnRate = 2f;
    public float horizontalLimit = 2.8f;

    public Text scoreText;
    public Text fuelText;

    private int score;
    private float fuel = 100f;

    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // update spawn timer
            spawnTimer -= Time.deltaTime;
            // spawn enemy if timer is less than 0
            if (spawnTimer < 0)
            {
                // reset spawn timer
                spawnTimer = spawnRate;
                // spawn enemy
                SpawnEnemy();

            }
        }

        // delete the enemy if it is out of screen
        foreach (Enemy enemy in GetComponentsInChildren<Enemy>())
        {
            if(mainCamera.transform.position.y - enemy.transform.position.y > Screen.height / 100)
            {
                Destroy(enemy.gameObject);
            }
        }
        
    }

    void SpawnEnemy()
    {
        // create a new enemy
        GameObject enemy = Instantiate(enemyPrefab);
        // set enemy parent
        enemy.transform.SetParent(transform);
        // set enemy position to random x and top of screen according to the playe
        enemy.transform.position = new Vector2(Random.Range(-horizontalLimit, horizontalLimit), player.transform.position.y + Screen.height / 100);
        enemy.GetComponent<Enemy>().OnKill += OnEnemyKill;
        
    }

    void OnEnemyKill()
    {
        score += 25;
        scoreText.text = "Score: " + score;
    }
}
