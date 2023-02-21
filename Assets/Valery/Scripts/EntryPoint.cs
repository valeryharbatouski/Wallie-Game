using System;
using UnityEngine;

namespace Valery
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Score _score;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private Coin _coin;

        private void Awake()
        {
            _score.ScoreUpdated += _scoreView.Set;
            _player.CoinColleted += Collect;
            
        }
        

        private void Collect(Coin obj)
        {
            _score.AddScore(obj.Value());
            Debug.Log("test");
        }
    }
}