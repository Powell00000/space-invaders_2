using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        //TODO: create system for updating units during gameplay state
        [Inject] private GameplayController gameplayCtrl = null;
        [Inject] private DiContainer container;
        [Inject] protected UnitDefinitionsProvider unitDefinitionsProvider = null;

        [SerializeField] protected BoxCollider2D boxCollider2d = null;
        [SerializeField] protected SpriteRenderer spriteRenderer = null;

        protected int currentHealth;
        protected EFaction faction;

        //TODO: Change System.Action into custom events implementation for automatic refs cleanup
        public Action OnDamageReceived;
        public Action<Unit> OnDeath;

        public BoxCollider2D BoxCollider2D => boxCollider2d;
        //TODO: strong typing
        public UnitStats Stats { get; protected set; }
        public int CurrentHealth => currentHealth;

        EFaction IUnit.Faction => faction;

        private float shootTimer;
        private float shootMaxTime;
        public virtual bool CanShoot => shootTimer >= shootMaxTime;

        protected virtual void Death()
        {
            if (OnDeath != null)
            {
                OnDeath(this);
            }
        }

        private void Start()
        {
            container.Inject(this);
        }

        protected virtual void Initialize()
        {
            currentHealth = Stats.HitsTillDeath;

            spriteRenderer.color = Stats.Color;
            shootMaxTime = Stats.ShootTime;
            shootTimer = shootMaxTime;
        }

        protected virtual void ClearEvents()
        {
            OnDamageReceived = null;
            OnDeath = null;
        }

        void IUnit.ReceiveDamage(int amount)
        {
            CalculateHealthLeft(amount);

            OnDamageReceived?.Invoke();

            if (currentHealth <= 0)
            {
                Death();
            }
        }

        //for custom implementation (GODMODE)
        protected virtual void CalculateHealthLeft(int damageAmount)
        {
            currentHealth -= damageAmount;
        }

        protected abstract void MovementFunction();

        //TODO: create system for updating units during gameplay state
        protected virtual void Update()
        {
            if (gameplayCtrl.CurrentGameplayState != EGameplayState.Playing)
            {
                return;
            }

            CalculateShootTime();
            MovementFunction();
        }

        private void CalculateShootTime()
        {
            if (shootTimer < shootMaxTime)
            {
                shootTimer += Time.deltaTime;
            }
        }

        public void ForceDeath()
        {
            Death();
        }

        public void ShootIfCan()
        {
            if (!CanShoot)
            {
                return;
            }

            Shoot();
            shootTimer = 0;
        }

        protected abstract void Shoot();
    }
}