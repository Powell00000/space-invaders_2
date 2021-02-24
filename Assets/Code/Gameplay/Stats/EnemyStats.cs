using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Units/Enemy")]
public class EnemyStats : UnitStats
{
    [SerializeField, Tooltip("Speed for when the unit is not yet in place")] private float flySpeed = 5;
    [SerializeField, Tooltip("Standard movement speed")] private float movementSpeed = 5;
    [SerializeField] private Sprite[] animationFrames;

    public float FlySpeed => flySpeed;
    public float MoveSpeed => movementSpeed;
    public Sprite[] AnimationFrames => animationFrames;
}
