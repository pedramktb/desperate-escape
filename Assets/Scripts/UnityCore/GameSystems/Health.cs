using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UnityCore.GameSystems
{
    public class Health : MonoBehaviour
    {
        [Header("General")]

        [Tooltip("GameObject's maximum health")]
        [SerializeField] float maxHealth;

        [Tooltip("GameObject's maximum shield")]
        [SerializeField] float maxShield;

        [Tooltip("The Ratio in which the health is considered critical")]
        [Range(0, 1)] [SerializeField] float criticalHealthRatio;
        [Range(0, 1)] [SerializeField] float criticalShieldRatio;

        public float CurrentHealth { get; private set; }
        public float CurrentSheild { get; private set; }

        public bool IsInvincible { get; private set; }

        public bool IsDead { get; private set; }

        public float GetHealthRatio() => CurrentHealth / maxHealth;
        public float GetShieldRatio() => CurrentSheild / maxShield;

        public bool IsHealthCritical() => GetHealthRatio() < criticalHealthRatio;
        public bool IsShieldCritical() => GetShieldRatio() < criticalShieldRatio;

        public event Action<Health, float, GameObject> OnDamaged;
        public event Action<Health, float> OnHealed;
        public event Action<Health, float> OnShieldAdded;
        public event Action<Health> OnDeath;
        public event Action<Health> OnShieldBroken;

        #region Unity Functions
        private void Start()
        {
            CurrentHealth = maxHealth;
            CurrentSheild = maxShield;
        }
        #endregion

        #region Public Functions

        public void SetMaxHealth(float maxHealth)
        {
            this.maxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }
        public void SetMaxShield(float maxShield)
        {
            this.maxShield = maxShield;
            CurrentSheild = 0;
        }

        public void Heal(float amount)
        {
            float healthBefore = CurrentHealth;
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);

            float trueHealAmount = CurrentHealth - healthBefore;
            if (OnHealed != null)
            {
                OnHealed.Invoke(this, trueHealAmount);
            }
        }

        public void AddShield(float amount)
        {
            float shieldBefore = CurrentSheild;
            CurrentSheild += amount;
            CurrentSheild = Mathf.Clamp(CurrentSheild, 0f, maxShield);

            float trueHealAmount = CurrentSheild - shieldBefore;
            if (OnHealed != null)
            {
                OnShieldAdded.Invoke(this, trueHealAmount);
            }
        }
        public void TakeDamage(float amount, GameObject damageSource)
        {
            Debug.Log("yes3");
            if (IsInvincible)
                return;

            float healthBefore = CurrentHealth;
            float shieldBefore = CurrentSheild;
            float demageDealtToShield;
            float trueDamageAmount;
            if (CurrentSheild > amount)
            {
                demageDealtToShield = amount;
                CurrentSheild -= amount;
                if (demageDealtToShield > 0f && OnDamaged != null)
                    OnDamaged.Invoke(this, demageDealtToShield, damageSource);
                return;
            }
            demageDealtToShield = CurrentSheild;
            amount -= CurrentSheild;
            if (shieldBefore != 0)
                OnShieldBroken.Invoke(this);
            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);
            trueDamageAmount = healthBefore - CurrentHealth;
            if (trueDamageAmount > 0f && OnDamaged != null)
            {
                OnDamaged.Invoke(this, trueDamageAmount + demageDealtToShield, damageSource);
            }
            CheckForDeath();
        }

        public void Kill()
        {
            CurrentHealth = 0;
            CheckForDeath();
        }
        #endregion

        #region Private Functions
        private void CheckForDeath()
        {
            if (IsDead)
                return;
            if (CurrentHealth <= 0f)
            {
                IsDead = true;
                if (OnDeath != null)
                {
                    OnDeath.Invoke(this);
                }
            }
        }
        #endregion
    }
}