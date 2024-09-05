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
    public float timeDiff = 5f; // time btw each wave
    private float countdown = 2f; // first wave timer

    private float searchTimer = 1f; // optimize enemy search

    private SpawnState spawnState = SpawnState.Counting;

    public Transform[] spawnPoints; //spawnpoint array

    void Start()
    {
        
    }

    void Update()
    {     
        if(spawnState == SpawnState.Waiting)
        {
            if (!EnemiesAlive())
            {
                Debug.Log("wavedone");
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        
        
        if(countdown <= 0f)
        {
            if(spawnState != SpawnState.Spawning)
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
        Debug.Log("wavy");
        spawnState = SpawnState.Spawning;
        for(int i = 0; i < _wave.enemyCount; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1f/_wave.rate);
        }


        spawnState = SpawnState.Waiting;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("hello i am " + enemy.name);
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
        Debug.Log("Wavecomplet");
        spawnState = SpawnState.Counting;
        countdown = timeDiff;

        if( nextWave + 1 > waves.Length - 1 )
        {
            nextWave = 0;
            Debug.Log("loopi");
        }
        else
        {
            nextWave++;
        }
    }
}
