using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Units/Enemy")]
public class EnemyStats : UnitStats
{
    [SerializeField] private float flySpeed = 5;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private Sprite[] animationFrames;

    public float FlySpeed => flySpeed;
    public float MoveSpeed => movementSpeed;
    public Sprite[] AnimationFrames => animationFrames;
}
