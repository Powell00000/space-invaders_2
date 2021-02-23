using Assets.Code.Gameplay.Stats;
using Assets.Code.Gameplay.Units.Shield;
using Game.Gameplay;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Zenject.Inject] private GameplayController gameplayController;
    [Zenject.Inject] private ShieldBricksPool bricksPool;

    [SerializeField] private int bricksCountInRow;
    [SerializeField] private int width;
    [SerializeField] private int rows;

    [SerializeField] private ShieldStats shieldStats;

    private void Awake()
    {
        gameplayController.OnLevelStarting += RebuildShield;
    }

    private void RebuildShield()
    {
        DestroyAllChildren();
        BuildShield();
    }

    private void BuildShield()
    {
        float brickSize = (float)width / (float)bricksCountInRow;
        float brickRadius = brickSize / 2f;

        Vector3 startingPos = transform.position + Vector3.left * ((float)width / 2) + Vector3.up * ((float)rows * brickSize / 2);
        startingPos += Vector3.right * brickRadius;
        startingPos += Vector3.down * brickRadius;
        for (int i = 0; i < bricksCountInRow; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var brick = bricksPool.Spawn(new ShieldBrick.SpawnContext(shieldStats));
                brick.transform.SetParent(transform);
                brick.transform.localScale = Vector3.one * brickSize;
                brick.transform.position = startingPos + Vector3.right * i * brickSize + Vector3.down * j * brickSize;
            }
        }
    }

    private void DestroyAllChildren()
    {
        var children = GetComponentsInChildren<ShieldBrick>();
        for (int i = 0; i < children.Length; i++)
        {
            bricksPool.Despawn(children[i]);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        float brickSize = (float)width / (float)bricksCountInRow;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, rows * brickSize, 1));

        Vector3 startingPos = transform.position + Vector3.left * ((float)width / 2) + Vector3.up * ((float)rows * brickSize / 2);
        startingPos += Vector3.right * brickSize / 2;
        startingPos += Vector3.down * brickSize / 2;
        Gizmos.DrawSphere(startingPos, 0.1f);

        Gizmos.color = Color.blue;
        for (int i = 0; i < bricksCountInRow; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var pos = startingPos + Vector3.right * i * brickSize + Vector3.down * j * brickSize;

                Gizmos.DrawWireCube(pos, new Vector3(brickSize, brickSize, 1));

            }
        }
    }
#endif
}
