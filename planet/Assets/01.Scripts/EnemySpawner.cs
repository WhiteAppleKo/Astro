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

        // ���� ���̺��� ��� ���� ������ �� ���� ���̺�� ��ȯ
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
            // ������ ���̺긦 ������ �Ŀ� �ʿ��� �۾��� �����ϰų� ���� ���� ���� ó���� �� �� �ֽ��ϴ�.
        }
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;     // ���� ���̺� �� ���� �ֱ�
    public int maxEnemyCount;   // ���� ���̺� �� ���� ����
    public GameObject enemyPrefab;  // ���� ���̺� �� ���� ����
}
