using UnityEngine;
using System.Collections;


// Define the Wave class
[System.Serializable]
public class Wave {
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public int meleeCount;
    public int rangedCount;
}
public class EnemySpawner : MonoBehaviour {
    public Transform[] spawnPoints;
    public Wave[] waves;
    public float timeBetweenWaves = 5f;

    private Wave _currentWave;
    private int _currentWaveIndex;
    private float _waveCountdown;
    private bool _isWaitingForWaveEnd;
    private int _activeEnemies = 0;

    void Start() {
        _waveCountdown = timeBetweenWaves;
    }
    
    void Update() {
        Debug.Log("Current Enemies: " + _activeEnemies);
        // Check if it's time to start a new wave
        if (!_isWaitingForWaveEnd && _waveCountdown <= 0f && _activeEnemies <= 0) {
            if (_currentWaveIndex < waves.Length) {
                StartCoroutine(StartWave(_currentWaveIndex));
            }
        } else {
            _waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator StartWave(int index) {
        _isWaitingForWaveEnd = true;
        _currentWave = waves[index];

        // Spawn enemies and increment active enemies count
        for (int i = 0; i < _currentWave.meleeCount; i++) {
            SpawnEnemy(_currentWave.meleeEnemyPrefab);
            _activeEnemies++;
            yield return new WaitForSeconds(1f); // staggered spawn
        }

        for (int i = 0; i < _currentWave.rangedCount; i++) {
            SpawnEnemy(_currentWave.rangedEnemyPrefab);
            _activeEnemies++;
            yield return new WaitForSeconds(1f); // staggered spawn
        }

        _currentWaveIndex++;
        _isWaitingForWaveEnd = false;
    }

    void SpawnEnemy(GameObject enemyPrefab) {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Find the player GameObject
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming the player has a "Player" tag

        // Set the player as the target for the enemy
        if (player != null) {
            enemy.GetComponent<Enemy>().SetTarget(player.transform);
        }
    }

    public void EnemyDefeated()
    {
        _activeEnemies--;
    }
}