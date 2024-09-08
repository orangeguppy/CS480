using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessWS : MonoBehaviour
{
    public Transform[] enemies;

    public float timeDiff = 7f; //time btw waves
    private float countdown = 3f; //first wave timer

    private int waveNumber = 0;
    public Transform spawnpoint;
    public float spawnDiff = 0.2f;  //time btw each unit in wave

    public bool currSpawning = false;
    public bool gameStarted = false;

    public GameObject arrows;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Wait for player input to start the first wave
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // can change later lol
            {
                gameStarted = true;
                Debug.Log("start gaem");
            }
            return;
        }

        if (!currSpawning)
        {
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeDiff; 
            }

            countdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave()
    {
        currSpawning = true;
        arrows.SetActive(false);
        waveNumber++;
        
        for (int i = 0; i < waveNumber + 2; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDiff);
        }
        currSpawning= false;
        arrows.SetActive(true);
    }

    void SpawnEnemy()
    {
        int rdmIdx = Random.Range(0, enemies.Length);
        Transform enemy = enemies[rdmIdx];
        Instantiate(enemy, spawnpoint.position, spawnpoint.rotation);
    }
}