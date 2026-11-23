using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Vector3 startPos;
    public float damage;

    void Start()
    {
        startPos = transform.position;
    }

    
    void Update()
    {
        DestroyBullet();
    }
    void DestroyBullet()
    {
        if (Mathf.Abs(startPos.x-transform.position.x) > 50f || Mathf.Abs(startPos.z-transform.position.z) > 50f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            MoveEnemy enemy = other.GetComponent<MoveEnemy>();
            if (enemy != null)
            {
                enemy.TakeBulletDemage(damage);
                Destroy(gameObject);
            }
        }
    }
}
