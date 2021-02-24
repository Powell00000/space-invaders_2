using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Units/Player")]
public class PlayerStats : UnitStats
{
    [SerializeField] private string shootSound;

    [SerializeField] private float speed = 10;

    public float Speed => speed;

    public string ShootSound => shootSound;
}
