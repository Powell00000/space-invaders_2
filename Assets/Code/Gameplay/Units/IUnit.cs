namespace Game.Gameplay
{
    public interface IUnit
    {
        EFaction Faction { get; }

        void ReceiveDamage(int amout);
        void ShootIfCan();

        void ForceDeath();
    }
}