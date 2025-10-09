using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    private float spawnInterval = 1.8f;
    private float spawnXMin = -3.5f;
    private float spawnXMax = 3.5f;
    private float spawnY = 7.4f;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while (GameManager.Instance.isGameActive && !GameManager.Instance.isBossSpawn)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[randomIndex];

            float randomX = Random.Range(spawnXMin, spawnXMax);
            Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);

            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }

        Debug.Log("éGãõìGÇÃê∂ê¨Çí‚é~ÅBÉ{ÉXêÌäJén!");
    }

    public void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}