using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

// Tutorials: https://www.youtube.com/watch?v=7T-MTo8Uaio and https://www.youtube.com/watch?v=pKN8jVnSKyM&t=782s
[System.Serializable]
public class SpawnedEnemy {
    public GameObject enemyPrefab;
    public int cost; // cost of spawning the enemy
    public int probability; // probability of being spawned
}

// public class Wave {
//     public float spawnInterval;
//     public float waveDuration;
// }

public class WaveSpawner : MonoBehaviour {
    // the current wave number
    [SerializeField] int currWave;
    [SerializeField] int maxWaveValue;
    [SerializeField] int minWaveValue;
    // the amount a wave can spend on spawning enemies
    int waveValue;
    [SerializeField] List<GameObject> enemiesToSpawn = new List<GameObject>();
    // array of all the enemy spawn points
    [SerializeField] Transform[] spawnPoints;
    float levelTime = 90f;  // total duration of level
    float waveDuration;
    // The enemies that can be spawned
    [SerializeField] SpawnedEnemy[] possibleEnemies;
    float spawnInterval;
    float spawnTimer;
    // The total number of waves for a scene
    [SerializeField] int totalWaves;
    float restTime = 2f; // rest time between waves

    void Start() {
        waveDuration = levelTime / totalWaves;
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
                    StartCoroutine(waitBetweenWavesCo());
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

    IEnumerator waitBetweenWavesCo() {
        yield return new WaitForSeconds(restTime);
    }

    void SpawnWave() {
        waveValue = Random.Range(minWaveValue, maxWaveValue);
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
            int randEnemyNum = getEnemyToSpawn();
            int randEnemyCost = possibleEnemies[randEnemyNum].cost;
            if (waveValue - randEnemyCost >= 0) {
                spawnedEnemies.Add(possibleEnemies[randEnemyNum].enemyPrefab);
                waveValue -= randEnemyCost;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = spawnedEnemies;
    }

    int getEnemyToSpawn() {
        int cumulProb = 0;
        int currentProb = Random.Range(0, 100);
        for (int i = 0; i < possibleEnemies.Length; i++) {
            cumulProb += possibleEnemies[i].probability;
            if (currentProb <= cumulProb) {
                return i;
            }
        }
        return 0;
    }

    public void StopSpawning() {
        // clear the list of enemies that haven't been spawned
        Debug.Log("stop");
        enemiesToSpawn.Clear();
        // make the already spawned enemies stop moving?
        // clear the already spawned enemies after 0.4f
        // start the same wave again
    }
}
