using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float speed;
    public float health;
    public float damage;
    public float demageInterval;
    public int scoreValue;
}
