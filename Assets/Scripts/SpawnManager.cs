using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //z=6,-15, 14,-14 olmayacak aralıklar
    [SerializeField] private GameObject[] EnemyPrefabs;
    [SerializeField] private GameObject[] BuffPrefabs;
    [SerializeField] private int[] BuffRatios={ 40,20,40 };
    private bool firstSpawn = true;
    private float minEnemySpawnTime = 6f;
    public float _minEnemySpawnTime
    {
        get { return minEnemySpawnTime; }
        set { minEnemySpawnTime=value; }
    }
    private float maxEnemySpawnTime = 9f;
    public float _maxEnemySpawnTime
    {
        get { return maxEnemySpawnTime; }
        set { maxEnemySpawnTime = value; }
    }
    private float minBuffSpawnTime = 12f;
    public float _minBuffSpawnTime
    {
        get { return minBuffSpawnTime; }
        set { minBuffSpawnTime = value; }
    }
    private float maxBuffSpawnTime = 18f;
    public float _maxBuffSpawnTime
    {
        get { return maxBuffSpawnTime; }
        set { maxBuffSpawnTime = value; }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemyEnumerator());
        StartCoroutine(SpawnBuffedEnumerator());
    }
    private void OnEnable()
    {
        GameManager.OnDifficultyLevelUp += LevelUpSpawnTime;
    }
    private void OnDisable()
    {
        GameManager.OnDifficultyLevelUp -= LevelUpSpawnTime;
    }


    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, EnemyPrefabs.Length);
        float spawnX = Random.Range(0,2)==0 ? Random.Range(-14f, -24f) : Random.Range(14f, 6f);
        /*float x;

        if (Random.Range(0, 2) == 0)
        {
            x = Random.Range(-25f, -15f);
        }
        else
        {
            x = Random.Range(6f, 16f);
        } yukarıdaki if else ile aynı işlevi görüyor ama daha kısa ve okunaklı bir şekilde yazılmıştır.
        */
        float spawnZ = Random.Range(0, 2) == 0 ? Random.Range(-25f, -15f) : Random.Range(6f, 16f);
        Vector3 spawnPos = new Vector3(spawnX, EnemyPrefabs[randomIndex].transform.position.y, spawnZ);
        Instantiate(EnemyPrefabs[randomIndex], spawnPos, EnemyPrefabs[randomIndex].transform.rotation);
    }
    IEnumerator SpawnEnemyEnumerator()
    {
        while (true)
        {
            if(firstSpawn)
            {
                firstSpawn = false;
                yield return new WaitForSeconds(2f);
            }
            SpawnEnemy();
            yield return new WaitForSeconds(Random.Range(_minEnemySpawnTime, _maxEnemySpawnTime));
        }
    }
    void SpawnBuffed()
    {
        int randomIndex = 0;
        float totalRatio = 0f;
        foreach (int ratio in BuffRatios)
        {
            totalRatio += ratio;
        }
        float randomValue = Random.Range(0f, totalRatio);
        for (int i = 0; i < BuffRatios.Length; i++)
        {
            randomValue -= BuffRatios[i];
            if (randomValue < 0)
            {
                randomIndex = i;
                break;
            }
        }
        Vector3 spawnPos = new Vector3(Random.Range(10f, -10f), BuffPrefabs[randomIndex].transform.position.y, Random.Range(2f, -8f));
        Instantiate(BuffPrefabs[randomIndex], spawnPos, BuffPrefabs[randomIndex].transform.rotation);
    }
    IEnumerator SpawnBuffedEnumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minBuffSpawnTime, _maxBuffSpawnTime));
            SpawnBuffed();
        }
    }
    void LevelUpSpawnTime(int level)
    {
        _minEnemySpawnTime -= 0.8f;
        _maxEnemySpawnTime -= 0.8f;
        _minBuffSpawnTime -= 0.9f;
        _maxBuffSpawnTime -= 0.9f;
    }
}
