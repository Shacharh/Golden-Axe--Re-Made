using UnityEngine;
using System.Collections.Generic;

public class EnemiesWaveSystem : MonoBehaviour
{
    [System.Serializable]
    public class Wave //parameters settings for each wave
    {
        public GameObject enemyPrefabs;
        public int enemiesInWave;
        public Transform[] spawnPoints;
    }

    [Header("Waves Setup")] //list of all the waves and keeping track of active wave
    public List<Wave> waves = new List<Wave>();
    private int currentWaveIndex = 0;

    private List<GameObject> activeEnemies = new List<GameObject>(); //list that keep track of enemies alive in current wave

    private void Start()
    {
        //spawning first wave at the start of the level
        SpawnWave(currentWaveIndex);
    }

    private void Update()
    {
        //check if wave is cleared and startt next wave
        if(activeEnemies.Count == 0 && currentWaveIndex < waves.Count - 1)
        {
            currentWaveIndex++;
            SpawnWave(currentWaveIndex);
        }
    }

    void SpawnWave(int waveIndex)
    {
        Wave wave = waves[waveIndex];
        int enemiesToSpawn = Mathf.Min(wave.enemiesInWave, wave.spawnPoints.Length);

        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawnPoints = wave.spawnPoints[i];
            GameObject enemy = Instantiate(wave.enemyPrefabs, spawnPoints.position, spawnPoints.rotation);
            

            activeEnemies.Add(enemy);

            HealthSystem enemyHealth = enemy.GetComponent<HealthSystem>();
            if(enemyHealth != null)
            {
                enemyHealth.onDeath += () => { activeEnemies.Remove(enemy); };
            }
        }
        Debug.Log("Spawned Wave" + (waveIndex + 1));
    }

}
