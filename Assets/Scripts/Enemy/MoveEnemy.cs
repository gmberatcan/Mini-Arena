using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;
    Rigidbody enemyRb;
    public Transform target;
    void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        EnemyMoveTarget();
    }
    private void EnemyMoveTarget()
    {
        if(target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            enemyRb.MovePosition(transform.position + direction * enemyStats.speed * Time.fixedDeltaTime);
            /*direction * speed * time = Ne kadar hareket edeceğim?
            position + movement = Yeni pozisyonum ne olacak ?*/
        }
        else
        {
            Debug.Log("Target is null");
            return;
        }
    }
}
