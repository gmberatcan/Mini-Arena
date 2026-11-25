using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //z=6,-15, 14,-14 olmayacak aralıklar
    [SerializeField] private GameObject[] EnemyPrefabs;
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private GameObject[] BuffPrefabs;
    private bool firstSpawn = true;
    private float buffTime = 8f;

    void Start()
    {
        StartCoroutine(SpawnEnemyEnumerator());
        StartCoroutine(SpawnBuffedEnumerator());
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
            yield return new WaitForSeconds(spawnTime);
        }
    }
    void SpawnBuffed()
    {
        int randomIndex = Random.Range(0, BuffPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(14f, -14f), BuffPrefabs[randomIndex].transform.position.y, Random.Range(6f, -15f));
        Instantiate(BuffPrefabs[randomIndex], spawnPos, BuffPrefabs[randomIndex].transform.rotation);
    }
    IEnumerator SpawnBuffedEnumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(buffTime);
            SpawnBuffed();
        }
    }
}
