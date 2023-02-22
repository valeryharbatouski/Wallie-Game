using System;
using UnityEngine;
using UnityEngine.UI;

namespace Valery
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health = 100;
        // [SerializeField] private Slider _heathBar;
        public int TakeDamage(int _damageValue) => RemoveHealth(_damageValue);
        public int Heal(int _damageValue) => AddHealth(_damageValue);
        public int HealthValue() => _health;

        public event Action<float> HealthUpdated;

        private void Awake()
        {
            // _heathBar.maxValue = _health;
            SetHealth();
        }

        private int RemoveHealth(int _damageValue)
        {
            _health -= _damageValue;
            HealthUpdated?.Invoke(_health);
            // SetHealth();
            return _health;
        }

        private int AddHealth(int _healValue)
        {
            _health += _healValue;
            HealthUpdated?.Invoke(_health);
            // SetHealth();
            return _health;
        }

        private void SetHealth()
        {
            // _heathBar.value = _health;
        }
    }
}