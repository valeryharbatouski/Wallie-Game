using UnityEngine;
using System;

    public class Score : MonoBehaviour
    {
        [SerializeField] private int _currentScore;
        
        private const string KeyScore = "Score";

        public event Action<int> ScoreUpdated;
        
        // ReSharper disable once UnusedMember.Global
        public int Current => _currentScore;
        
        private void Start()
        {
            _currentScore = PlayerPrefs.GetInt(KeyScore, 0);
            ScoreUpdated?.Invoke(_currentScore);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt(KeyScore, _currentScore);
        }

        public void AddScore(int value)
        {
            _currentScore += value;
            ScoreUpdated?.Invoke(_currentScore);
        }
    }