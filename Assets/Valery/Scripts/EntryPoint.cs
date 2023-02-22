
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
        [SerializeField] private SceneLoad _loseScreen;

        private void Awake()
        {
            _score.ScoreUpdated += _scoreView.Set;
            _player.CoinColleted += UpdateScore;
            _health.HealthUpdated += _healthView.Set;
            _health.HealthUpdated += PlayerDeath;
            _player.PlayerHittedByBullet += TakeDamage;
            

        }
        
        private void UpdateScore(Coin obj)
        {
            _score.AddScore(obj.Value());
        }

        private void TakeDamage(BotBullet obj)
        {
            _health.TakeDamage(obj.DamageValue());
        }

        private void PlayerDeath(float obj)
        {
            if (_health.HealthValue() == 0)
            {
                _player.gameObject.SetActive(false);
                _enemy.StopAllCoroutines();
                _loseScreen.LoadScene();
            }
        }

        private void OnDestroy()
        {
            _score.ScoreUpdated -= _scoreView.Set;
            _player.CoinColleted -= UpdateScore;
            _health.HealthUpdated -= _healthView.Set;
            _health.HealthUpdated -= PlayerDeath;
            _player.PlayerHittedByBullet -= TakeDamage;
        }
    }
}