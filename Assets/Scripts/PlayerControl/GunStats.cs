using UnityEngine;

[CreateAssetMenu(fileName = "GunStats", menuName = "Stats/GunStats")]
public class GunStats : ScriptableObject
{
    public float damage;
    public float fireTime;
    public float bulletSpeed;
}
