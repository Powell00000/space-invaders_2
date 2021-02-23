using UnityEngine;

//base class for unit stats
public abstract class UnitStats : ScriptableObject
{
    [SerializeField] private int hitsTillDeath = 1;
    [SerializeField] private int pointsForDestroying = 10;
    [SerializeField] private Color color = Color.black;

    [SerializeField] private float baseProjectileSpeed = 10;
    [SerializeField] private float shootTime = 1;

    public int HitsTillDeath => hitsTillDeath;
    public int PointsForDestroying => pointsForDestroying;
    public Color Color => color;

    public float BaseProjectileSpeed => baseProjectileSpeed;
    public float ShootTime => shootTime;

}
