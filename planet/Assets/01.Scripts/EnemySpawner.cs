using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;

    private Wave currentWave;
    private int currentWaveIndex = 0;

    private void Awake()
    {
        currentWave = waves[currentWaveIndex];
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        int enemyCount = 0;
        while (enemyCount < currentWave.maxEnemyCount)
        {
            GameObject clone = Instantiate(currentWave.enemyPrefab);
            OrbitMob orbitMob = clone.GetComponent<OrbitMob>();
            enemyCount++;
            yield return new WaitForSeconds(currentWave.spawnTime);
        }

        // 현재 웨이브의 모든 적을 생성한 후 다음 웨이브로 전환
        NextWave();
    }

    private void NextWave()
    {
        currentWaveIndex++;
        if (currentWaveIndex < waves.Length)
        {
            currentWave = waves[currentWaveIndex];
            StartCoroutine("SpawnEnemy");
        }
        else
        {
            // 마지막 웨이브를 생성한 후에 필요한 작업을 수행하거나 게임 종료 등의 처리를 할 수 있습니다.
        }
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;     // 현재 웨이브 적 생성 주기
    public int maxEnemyCount;   // 현재 웨이브 적 등장 숫자
    public GameObject enemyPrefab;  // 현재 웨이브 적 등장 종류
}
