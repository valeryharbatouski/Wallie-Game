using System;
using UnityEngine;
using UnityEngine.UI;

namespace Valery
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health = 100;
        public int TakeDamage(int _damageValue) => RemoveHealth(_damageValue);
        public int Heal(int _damageValue) => AddHealth(_damageValue);
        public int HealthValue() => _health;

        public event Action<float> HealthUpdated;

        private int RemoveHealth(int _damageValue)
        {
            _health -= _damageValue;
            HealthUpdated?.Invoke(_health);
            return _health;
        }

        private int AddHealth(int _healValue)
        {
            _health += _healValue;
            HealthUpdated?.Invoke(_health);
            return _health;
        }
    }
}