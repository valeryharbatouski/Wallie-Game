using System;
using System.Collections.Generic;
using UnityEngine;

namespace Valery
{
    [RequireComponent(typeof(Rigidbody))]
    public class Enemy : MonoBehaviour, IEnemyStateContext
    {
        public Transform MainTransform => transform;

        [SerializeField] private Shoot _shoot;
        [SerializeField] private Health _health;

        [SerializeField] private Bullet _bullet;

        [SerializeField] private SpawnCoin _spawner;

        [SerializeField] private AbstractEnemyState _startState;
        [SerializeField] private List<AbstractEnemyState> _states;

        [SerializeField] private Player _target;

        [Header("DEBUG")] [SerializeField] private AbstractEnemyState _currentState;


        private void Awake()
        {
            _shoot = GetComponent<Shoot>();
            _health = GetComponent<Health>();
            foreach (var state in _states)
            {
                state.Started += StateOnStart;
            }

            _startState.OnStart(this, null);
            _spawner = GetComponent<SpawnCoin>();
        }

        private void FixedUpdate()
        {
            if (_currentState.GetType() == typeof(ChaseEnemyState))
            {
                LookToTarget();
            }
        }


        private void OnDestroy()
        {
            foreach (var state in _states)
            {
                state.Started -= StateOnStart;
            }
        }

        private void StateOnStart(AbstractEnemyState state)
        {
            _currentState = state;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                StartCoroutine(_shoot.InfinityShoot());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                StopAllCoroutines();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_health.HealthValue() == 0)
            {
                Destroy(gameObject);
                _spawner.Spawner();
            }

            if (collision.collider.GetComponent<Bullet>())
            {
                _health.TakeDamage(_bullet.DamageValue());
            }
        }

        private void LookToTarget()
        {
            var _targetPosition = _target.transform.position;
            var _currentPosition = transform.position;
            var _positionDelta = _targetPosition - _currentPosition;
            transform.rotation = Quaternion.LookRotation(_positionDelta, transform.up);
            _shoot.ShootPiont().rotation = Quaternion.LookRotation(_positionDelta, transform.up);
        }

        private TEnemyState GetState<TEnemyState>() where TEnemyState : AbstractEnemyState
        {
            foreach (var abstractEnemyState in _states)
            {
                if (abstractEnemyState.GetType() == typeof(TEnemyState))
                {
                    return (TEnemyState)abstractEnemyState;
                }
            }

            throw new NullReferenceException();
        }

        public void SwitchState<TEnemyState>(StateArgs args) where TEnemyState : AbstractEnemyState
        {
            var state = GetState<TEnemyState>();
            state.OnStart(this, args);
        }
    }
}