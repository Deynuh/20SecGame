using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPrefabs;
    [SerializeField] private Transform targetToFace;

    [SerializeField] private float minInterval = 2f;
    [SerializeField] private float maxInterval = 5f;

    [SerializeField] private Vector3 leftZoneMin;
    [SerializeField] private Vector3 leftZoneMax;
    [SerializeField] private Vector3 rightZoneMin;
    [SerializeField] private Vector3 rightZoneMax;
    [SerializeField] private Vector3 middleZoneMin;
    [SerializeField] private Vector3 middleZoneMax;

    private float timeSinceLastSpawn;
    private float nextSpawnTime;

    private Vector3 nextSpawnLocation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextSpawnTime = Random.Range(minInterval, maxInterval);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= nextSpawnTime)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
            nextSpawnTime = Random.Range(minInterval, maxInterval);
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPrefabs.Length);
        Vector3 spawnPos = GetSpawnLocation();

        GameObject enemy = Instantiate(spawnPrefabs[randomIndex], spawnPos, Quaternion.identity);
    
        enemy.transform.LookAt(targetToFace);
    }

    private Vector3 GetSpawnLocation()
    {
        // Random.Range is inclusive of first number and exclusive of second number for int
        int zoneIndex = Random.Range(0,3);
        
        Vector3 min, max;

        switch (zoneIndex)
        {
            case 0:
                min = leftZoneMin;
                max = leftZoneMax;
                break;
            case 1:
                min = rightZoneMin;
                max = rightZoneMax;
                break;
            case 2:
                min = middleZoneMin;
                max = middleZoneMax;
                break;
            default: // just in case, not really necessary though
                min = middleZoneMin;
                max = middleZoneMax;
                break;
        }

        float randomX = Random.Range(min.x, max.x);
        float randomY = Random.Range(min.y, max.y);
        float randomZ = Random.Range(min.z, max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
