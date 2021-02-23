using Assets.Code.Gameplay.Stats;
using Assets.Code.Gameplay.Units.Shield;
using Game.Gameplay;
using Zenject;

public class ShieldBrick : Unit, IPoolable<ShieldBrick.SpawnContext, ShieldBricksPool>
{
    private ShieldBricksPool brickPool;
    protected override void MovementFunction()
    {
    }

    protected override void Shoot()
    {
    }

    public void OnDespawned()
    {
    }

    protected override void Death()
    {
        base.Death();
        brickPool.Despawn(this);
    }

    public void OnSpawned(SpawnContext context, ShieldBricksPool brickPool)
    {
        faction = EFaction.Obstacle;

        Stats = context.ShieldStats;

        this.brickPool = brickPool;
        Initialize();
    }

    public struct SpawnContext
    {
        private ShieldStats shieldStats;

        public ShieldStats ShieldStats => shieldStats;

        public SpawnContext(ShieldStats shieldStats)
        {
            this.shieldStats = shieldStats;
        }
    }
}
