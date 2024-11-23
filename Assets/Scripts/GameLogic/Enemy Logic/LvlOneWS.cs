using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LvlOneWS : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting };

    [System.Serializable]
    public class Wave
    {
        public Transform enemyPrefab;
        public int enemyCount;
        public float spawnDiff; // higher number = smaller gap
    }

    public Wave[] waves;
    private int nextWave = 0;
    public float timeDiff = 15f; // time between each wave
    private float countdown = 2f; // first wave timer

    private float searchTimer = 1f; // optimize enemy search

    private SpawnState spawnState = SpawnState.Counting;

    public Transform spawnPoint;

    private bool gameStarted = false; // Track if the game has started
    [Header("UI")]
    public GameObject arrows; // Reference to the arrows GameObject

    public TextMeshProUGUI countdownText;

    public GameObject gameCompleteScreen;
    public int nextLevel;

    [Header("questions")]
    public GameObject[] questions;
    private List<int> idx =  new List<int>(); // index tracker 


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
            // Check for player input to start the game
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameStarted = true; 
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
        if(spawnState == SpawnState.Counting)
        {
            countdownText.gameObject.SetActive(true);

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
                countdownText.text = Mathf.Round(countdown).ToString();
            }
        }
        else
        {
            // Hide the countdown text when not in Counting state
            countdownText.gameObject.SetActive(false);
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
            yield return new WaitForSeconds(_wave.spawnDiff);
        }

        spawnState = SpawnState.Waiting;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawning enemy: " + enemy.name);
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
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
        spawnState = SpawnState.Counting;
        countdown = timeDiff;

        // Show arrows when the wave is complete
        if (arrows != null)
        {
            arrows.SetActive(true);
        }

        // Check if the current wave is the last one
        if (nextWave + 1 > waves.Length - 1)
        {
            EndGame();
        }
        else
        {
            nextWave++;
            ShowQuestion(); // Show a question only if there are more waves
        }
    }

    void EndGame()
    {
        Time.timeScale = 0f;
        gameCompleteScreen.SetActive(true);
        Debug.Log("level won");
        if( nextLevel > PlayerPrefs.GetInt("highestlesson", 1))
        {
            PlayerPrefs.SetInt("highestLesson", nextLevel);
        }
        
    }

    void ShowQuestion()
    {

        if (idx.Count == 0)
        {
            for (int i = 0; i < questions.Length; i++)
            {
                idx.Add(i);
            }
        }

        int randomIndex = Random.Range(0, idx.Count);
        int questionIndex = idx[randomIndex];
        idx.RemoveAt(randomIndex); // Remove it from the list

        // Activate the selected question
        StartCoroutine(ShowQ(questionIndex));
    }

    IEnumerator ShowQ(int questionIndex)
    {
        // Activate the selected question
        questions[questionIndex].SetActive(true);
        Debug.Log("Showing question: " + questions[questionIndex].name);

        // Wait for the predetermined display time
        yield return new WaitForSeconds(timeDiff);

        // Deactivate the question
        questions[questionIndex].SetActive(false);
        Debug.Log("Hiding question: " + questions[questionIndex].name);
    }
}
