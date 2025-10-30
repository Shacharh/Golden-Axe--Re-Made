using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesWaveSystem : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefabs;
        public int enemiesInWave;
        public Transform[] spawnPoints;
    }

    [Header("Waves Setup")]
    public List<Wave> waves = new List<Wave>();
    private int currentWaveIndex = 0;

    private List<GameObject> activeEnemies = new List<GameObject>();

    [Header("Wave Timing")]
    [SerializeField] private float timeBetweenWaves = 3f; // delay in seconds
    private bool waitingForNextWave = false;

    [SerializeField] private GameObject doorTrigger;
    [SerializeField] private GameObject doorCollider;

    private void Start()
    {
        doorTrigger.SetActive(false);
        SpawnWave(currentWaveIndex);
    }

    private void Update()
    {
        if (activeEnemies.Count == 0 && !waitingForNextWave)
        {
            if (currentWaveIndex < waves.Count - 1)
            {
                currentWaveIndex++;
                StartCoroutine(WaitAndSpawnNextWave(currentWaveIndex));
            }

            else
            {
			if(doorCollider)
                doorCollider.SetActive(false);
			if(doorTrigger)
                doorTrigger.SetActive(true);
            }
        }
    }

    private IEnumerator WaitAndSpawnNextWave(int waveIndex)
    {
        waitingForNextWave = true;
        yield return new WaitForSeconds(timeBetweenWaves);

        SpawnWave(waveIndex);
        waitingForNextWave = false;
    }

    void SpawnWave(int waveIndex)
    {
        Wave wave = waves[waveIndex];
        int enemiesToSpawn = Mathf.Min(wave.enemiesInWave, wave.spawnPoints.Length);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawnPoint = wave.spawnPoints[i];
            GameObject enemy = Instantiate(wave.enemyPrefabs, spawnPoint.position, spawnPoint.rotation);

            activeEnemies.Add(enemy);

            HealthSystem enemyHealth = enemy.GetComponent<HealthSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.onDeath += () => { activeEnemies.Remove(enemy); };
            }
        }
        Debug.Log("Spawned Wave " + (waveIndex + 1));
    }
}
