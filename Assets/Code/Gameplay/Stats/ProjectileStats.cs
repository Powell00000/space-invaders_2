using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Projectile")]
public class ProjectileStats : ScriptableObject
{
    [SerializeField] private int damage = 1;
    [SerializeField] private int pointsForDestroying = 5;

    public int Damage => damage;

    public int PointsForDestroying => pointsForDestroying;
}
