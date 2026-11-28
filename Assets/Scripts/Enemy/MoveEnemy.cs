using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;
    static public event Action<int> OnEnemyDeath;
    private float Health;
    public float _health
    {
        get { return Health; }
        set { Health = value; }
    }
    private float demageTimer=0;
    private float damageInterval;
    PlayerController player;
    Rigidbody enemyRb;
    public Transform target;
    void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        GameManager.OnDifficultyLevelUp += UpdateDamageInterval;
    }
    private void OnDisable()
    {
        GameManager.OnDifficultyLevelUp -= UpdateDamageInterval;
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _health = enemyStats.health;
        UpdateDamageInterval(GameManager.CurrentDifficultyLevel);
    }

    // Update is called once per frame
    void Update()
    {
        TakeDemage();
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
            enemyRb.linearVelocity = direction * enemyStats.speed;
            /*direction * speed * time = Ne kadar hareket edeceğim?
            position + movement = Yeni pozisyonum ne olacak ?*/
        }
        else
        {
            Debug.Log("Target is null");
            return;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
    private void TakeDemage()
    {
        if (player == null) return;
        demageTimer += Time.deltaTime;
        if (demageTimer >= damageInterval)
        {
            player.TakeDamage(enemyStats.damage);
            demageTimer = 0;
        }
    }
    private void UpdateDamageInterval(int level)
    {
        damageInterval = Mathf.Max(0.3f, enemyStats.demageInterval - level * 0.1f);
    }
    void isDead()
    {
        if(_health <= 0)
        {
            OnEnemyDeath?.Invoke((int)enemyStats.scoreValue);
            Destroy(gameObject);
        }
    }
    public void TakeBulletDemage(float damage)
    {
        _health -= damage;
        isDead();
    }
}
