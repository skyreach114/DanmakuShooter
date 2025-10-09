using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;

    private float spawnY = 7.4f;

    public void StartSpawning()
    {
        Invoke("SpawnBoss", 2f);
    }

    void SpawnBoss()
    {
        if (GameManager.Instance.isGameActive)
        {
            Vector3 spawnPosition = new Vector3(0, spawnY, 0);

            Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

        }
    }
}
