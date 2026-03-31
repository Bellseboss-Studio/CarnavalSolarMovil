using UnityEngine;
using System;

namespace V2.Unidad
{
    public class UnitHealthController : MonoBehaviour
    {
        [SerializeField] private float maxHp = 100f;
        [SerializeField] private float _currentHp;

        // Eventos de daño y muerte
        public event Action OnDeath;
        public event Action<float> OnDamage;

        public void SetHp(float hp)
        {
            maxHp = hp;
            _currentHp = hp;
        }

        public void TakeDamage(float amount)
        {
            if (_currentHp <= 0) return;
            _currentHp -= amount;
            OnDamage?.Invoke(amount);
            if (_currentHp <= 0)
            {
                _currentHp = 0;
                OnDeath?.Invoke();
            }
        }

        public float GetCurrentHp() => _currentHp;
        public float GetMaxHp() => maxHp;
    }
}