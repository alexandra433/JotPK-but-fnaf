using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tutorials: https://www.youtube.com/watch?v=7T-MTo8Uaio and https://www.youtube.com/watch?v=pKN8jVnSKyM&t=782s
[System.Serializable]
public class SpawnedEnemy {
    public GameObject enemyPrefab;
    // cost of spawining the enemy
    public int cost;
}

// public class Wave {
//     public float spawnInterval;
//     public float waveDuration;
// }

public class WaveSpawner : MonoBehaviour {
    // the current wave number
    [SerializeField] int currWave;
    [SerializeField] int waveValueMultiplier;
    // the amount a wave can spend on spawning enemies
    int waveValue;
    [SerializeField] List<GameObject> enemiesToSpawn = new List<GameObject>();
    // array of all the enemy spawn points
    [SerializeField] Transform[] spawnPoints;
    // 4 waves of 21 seconds each for a total of 1m 24s
    float waveDuration = 21f;
    // The enemies that can be spawned
    [SerializeField] List<SpawnedEnemy> possibleEnemies = new List<SpawnedEnemy>();
    float waveTimer;
    float spawnInterval;
    float spawnTimer;
    // The total number of waves for a scene
    int totalWaves = 4;

    void Start() {
        // Wait 2 seconds before 1st wave
        spawnTimer = 2f;
        currWave = 1;
        SpawnWave();
    }

    void FixedUpdate() {
        if (spawnTimer <= 0) {
            // spawn the enemy at the front of the enemiesToSpawn list and then remove it
            // from the list
            if (enemiesToSpawn.Count > 0) {
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemiesToSpawn[0], randomPoint.position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval;
            } else {
                if (currWave + 1 <= totalWaves) {
                    currWave += 1;
                    SpawnWave();
                }
            }
        } else {
            // fixedDeltaTime is the amount of time that has passed since the last FixedUpdate call
            spawnTimer -= Time.fixedDeltaTime;
            // waveTimer -= Time.fixedDeltaTime;
        }

    }

    void SpawnWave() {
        waveValue = currWave * waveValueMultiplier;
        SpawnEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        //waveTimer = waveDuration;
    }

    // Create a list of enemies to spawn in a wave
    void SpawnEnemies() {
        List<GameObject> spawnedEnemies = new List<GameObject>();
        // in loop, grab random enemy and see if we can afford it. If so, add it to the
        // list of enemies to spawn and deduct its cost from waveValue
        while (waveValue > 0) {
            int randEnemyNum = Random.Range(0, possibleEnemies.Count);
            int randEnemyCost = possibleEnemies[randEnemyNum].cost;
            if (waveValue - randEnemyCost >= 0) {
                spawnedEnemies.Add(possibleEnemies[randEnemyNum].enemyPrefab);
                waveValue -= randEnemyCost;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = spawnedEnemies;
    }
}
