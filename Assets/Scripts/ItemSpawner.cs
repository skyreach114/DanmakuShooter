using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject hpItemPrefab;
    public GameObject expItemPrefab;

    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 9f;
    private float spawnTimer;

    private float spawnXMin = -3.5f;
    private float spawnXMax = 3.5f;
    private float spawnY = 7.4f;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        if (!GameManager.Instance.isGameActive) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnItem();
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void SpawnItem()
    {
        GameObject itemToSpawn = GetRandomItemPrefab();

        float randomX = Random.Range(spawnXMin, spawnXMax);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);

        Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
    }

    GameObject GetRandomItemPrefab()
    {
        // 40%‚ÌŠm—¦‚ÅHPItem
        if (Random.value < 0.4f)
        {
            return hpItemPrefab;
        }
        else
        {
            return expItemPrefab;
        }
    }
}