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

        [Tooltip("The Ratio in which the health is considered critical")]
        [Range(0, 1)] [SerializeField] float criticalHealthRatio;

        public float CurrentHealth { get; private set; }

        public bool IsInvincible { get; private set; }

        public bool IsDead { get; private set; }

        public float GetRatio() => CurrentHealth / maxHealth;

        public bool IsCritical() => GetRatio() < criticalHealthRatio;

        public event Action<Health, float, GameObject> OnDamaged;
        public event Action<Health, float> OnHealed;
        public event Action<Health> OnDeath;

        #region Unity Functions
        private void Start()
        {
            CurrentHealth = maxHealth;
        }
        #endregion

        #region Public Functions
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

        public void TakeDamage(float amount, GameObject damageSource)
        {
            if (IsInvincible)
                return;
            float healthBefore = CurrentHealth;
            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);

            float trueDamageAmount = healthBefore - CurrentHealth;
            if (trueDamageAmount > 0f && OnDamaged != null)
            {
                OnDamaged.Invoke(this, trueDamageAmount, damageSource);
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