using System.Collections;
using UnityEditor.Search;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    PlayerController player;
    [SerializeField] private GunStats gunStat;
    [SerializeField] private float attackRange=10f;
    [SerializeField] private LayerMask enemyLayer;

    private float fireTime;
    private float damage;
    public float Damage { set => damage = value; get => damage; }
    private float bulletSpeed;
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        fireTime = gunStat.fireTime;
        damage = gunStat.damage;
        bulletSpeed = gunStat.bulletSpeed;
        StartCoroutine(FireGun());
    }
    private void OnEnable()
    {
        GameManager.OnDifficultyLevelUp += LevelUpFireTime;
    }
    private void OnDisable()
    {
        GameManager.OnDifficultyLevelUp -= LevelUpFireTime;
    }

    void Update()
    {
        
    }
    private void SpawnBullet()
    {
        Transform target = FindNearestEnemy();  
        if(target == null) return;
        Vector3 bulletSpawnPos = player.transform.position;
        Vector3 direction= (target.position - bulletSpawnPos).normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos, player.transform.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;
        bullet.GetComponent<BulletScript>().damage = damage;
    }
    IEnumerator FireGun()
    {
        while (true)
        {
            SpawnBullet();
            yield return new WaitForSeconds(fireTime);
        }
    }
    private Transform FindNearestEnemy()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(player.transform.position, attackRange, enemyLayer);
        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        foreach (Collider enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
    }
    private void LevelUpFireTime(int level)
    {
        fireTime -= level*0.05f;
        damage += level ;
    }   
}
