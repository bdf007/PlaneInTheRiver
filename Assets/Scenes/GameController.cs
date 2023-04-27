using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public ObjectPool enemyPool;
    private GameObject mainCamera;
    public GameObject fuelPrefab;
    public float spawnRate = 2f;
    public float horizontalLimit = 2.8f;
    public float fuelDecreaseRate = 5f;
    public float fuelSpawnRate = 9f;
    private float fuelSpawnTimer;

    public Text scoreText;
    public Text fuelText;

    private int score;
    private float fuel = 100f;
    private float restartTimer = 3f;

    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnRate;
        player.GetComponent<Player>().OnFuel += OnFuel;

        fuelSpawnTimer = Random.Range(0f, fuelSpawnRate);
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

            // update fuel spawn timer
            fuelSpawnTimer -= Time.deltaTime;
            // spawn fuel if timer is less than 0
            if (fuelSpawnTimer < 0)
            {
                // reset fuel spawn timer
                fuelSpawnTimer = fuelSpawnRate;
                // spawn fuel
                SpawnFuel();
               
            }

            fuel -= Time.deltaTime * fuelDecreaseRate;
            fuelText.text = "Fuel: " + Mathf.RoundToInt(fuel);
            if (fuel < 0)
            {
                fuelText.text = "Fuel: 0";
                Destroy(player.gameObject);
            }
        } 
        else
        {
            restartTimer -= Time.deltaTime;
            if (restartTimer <= 0f)
            {
                SceneManager.LoadScene("Game");
            }
        }

        // delete the enemy if it is out of screen
        foreach (Enemy enemy in GetComponentsInChildren<Enemy>())
        {
            if(mainCamera.transform.position.y - enemy.transform.position.y > Screen.height / 100)
            {
                enemy.gameObject.SetActive(false);
            }
        }
        
    }

    void SpawnEnemy()
    {
        // create a new enemy
        GameObject enemy = enemyPool.GetObj();
        // set enemy parent
        enemy.transform.SetParent(transform);
        // set enemy position to random x and top of screen according to the playe
        enemy.transform.position = new Vector2(Random.Range(-horizontalLimit, horizontalLimit), player.transform.position.y + Screen.height / 100);
        enemy.GetComponent<Enemy>().OnKill += OnEnemyKill;
        
    }

    void SpawnFuel()
    {
        // spawn fuel
        GameObject fuel = Instantiate(fuelPrefab);
        // set fuel parent
        fuel.transform.SetParent(transform);
        // set fuel position to random x and top of screen according to the playe
        fuel.transform.position = new Vector2(Random.Range(-horizontalLimit, horizontalLimit), player.transform.position.y + Screen.height / 100);
    }

    void OnEnemyKill()
    {
        score += 25;
        scoreText.text = "Score: " + score;
    }

    void OnFuel()
    {
        fuel = 100f;

    }
}
