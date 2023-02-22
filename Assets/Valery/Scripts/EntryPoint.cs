using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Valery
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Score _score;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private Health _health;
        [SerializeField] private HealthView _healthView;

        private void Awake()
        {
            _score.ScoreUpdated += _scoreView.Set;
            _player.CoinColleted += UpdateScore;
            _health.HealthUpdated += _healthView.Set;
            _player.HittedByBullet += TakeDamage;

        }
        
        private void UpdateScore(Coin obj)
        {
            _score.AddScore(obj.Value());
        }

        private void TakeDamage(BotBullet obj)
        {
            _health.TakeDamage(obj.DamageValue());
        }
    }
}