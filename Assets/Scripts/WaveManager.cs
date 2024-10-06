using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WaveManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public GameObject currentEnemyPrefab;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private Coroutine spawnCoroutine;
    private bool isSpawning = false;
	public float waittime = 3.0f;

    public void StartGame()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartSpawning();
        }
    }

    public void StopGame()
    {
        isSpawning = false;
        StopSpawning();
        ClearEnemies();
    }

    private void StartSpawning()
    {
        currentEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(waittime);
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(currentEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.InitVelocity();
        activeEnemies.Add(enemy);
    }

    public void ClearEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            Destroy(enemy);
        }
        activeEnemies.Clear();
    }
}