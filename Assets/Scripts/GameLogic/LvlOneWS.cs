using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlOneWS : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting };

    [System.Serializable]
    public class Wave
    {
        public Transform enemyPrefab;
        public int enemyCount;
        public float rate; // higher number = smaller gap
    }

    public Wave[] waves;
    private int nextWave = 0;
    public float timeDiff = 5f; // time between each wave
    private float countdown = 2f; // first wave timer

    private float searchTimer = 1f; // optimize enemy search

    private SpawnState spawnState = SpawnState.Counting;

    public Transform[] spawnPoints; // spawn point array

    private bool gameStarted = false; // Track if the game has started

    public GameObject arrows; // Reference to the arrows GameObject

    void Start()
    {
        // Make the arrows visible initially since no waves have started
        if (arrows != null)
        {
            arrows.SetActive(true);
        }
    }

    void Update()
    {
        // Wait for player input to start the first wave
        if (!gameStarted)
        {
            // Check for player input to start the game (spacebar in this case)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameStarted = true; // Set the flag to indicate the game has started
            }
            return; // Exit Update until the game is started
        }

        if (spawnState == SpawnState.Waiting)
        {
            if (!EnemiesAlive())
            {
                Debug.Log("Wave completed");
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        // Countdown only when not currently spawning a wave
        if (countdown <= 0f)
        {
            if (spawnState != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            countdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Wave spawning");
        spawnState = SpawnState.Spawning;

        // Hide arrows when spawning starts
        if (arrows != null)
        {
            arrows.SetActive(false);
        }

        // Spawn all enemies in the wave
        for (int i = 0; i < _wave.enemyCount; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        spawnState = SpawnState.Waiting;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawning enemy: " + enemy.name);
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, sp.position, sp.rotation);
    }

    bool EnemiesAlive()
    {
        searchTimer -= Time.deltaTime;
        if (searchTimer <= 0f)
        {
            searchTimer = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");
        spawnState = SpawnState.Counting;
        countdown = timeDiff;

        // Show arrows when the wave is complete
        if (arrows != null)
        {
            arrows.SetActive(true);
        }

        // Cycle to the next wave or loop back if at the end
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Looping waves");
        }
        else
        {
            nextWave++;
        }
    }
}
