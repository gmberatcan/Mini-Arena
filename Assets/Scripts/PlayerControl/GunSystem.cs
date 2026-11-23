using System.Collections;
using UnityEditor.Search;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    PlayerController player;
    [SerializeField] private GunStats gunStat;
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

    
    void Update()
    {
        
    }
    private void SpawnBullet()
    {
        Vector3 bulletSpawnPos = player.transform.position;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos, player.transform.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = player.transform.forward * bulletSpeed;
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
}
