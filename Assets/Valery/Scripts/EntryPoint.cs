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

        private void Awake()
        {
            _score.ScoreUpdated += _scoreView.Set;
            
        }
    }
}