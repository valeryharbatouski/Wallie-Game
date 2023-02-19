using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Enemy_States.StateArgs;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
    public class ShootBot : MonoBehaviour, IEnemyStateContext
    {
        public Transform MainTransform => transform;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _shootPoint;

        [SerializeField] private int _botHealth = 50;
        private Bullet _bulletDamage;

        [SerializeField] private Slider _botHealthBar;

        [SerializeField] private COINSPAWNER _spawner;

        [SerializeField] private float _mintimeValue = 0.1f;
        [SerializeField] private float _maxtimeValue = 0.5f;

        [SerializeField] private AbstractEnemyState _startState;
        [SerializeField] private List<AbstractEnemyState> _states;

        [SerializeField] private Player _target;

        [Header("DEBUG")] [SerializeField] private AbstractEnemyState _currentState;
        
        private void Start()
        {
            foreach (var state in _states)
            {
                state.Started += StateOnStart;
            }
            
            _startState.OnStart(this, null);
            _botHealthBar.maxValue = _botHealth;
            _spawner = GetComponent<COINSPAWNER>();
        }

        private void FixedUpdate()
        {
            if (_currentState.GetType() == typeof(ChaseEnemyState))
            {
                LookToTarget();
            }
        }
        
        private void LookToTarget()
        {
            transform.rotation = Quaternion.LookRotation(_target.transform.position - transform.position, transform.up);
            _shootPoint.rotation = Quaternion.LookRotation(_target.transform.position - transform.position, transform.up);
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

        private IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range((float)_mintimeValue, (float)_maxtimeValue));
                Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
                
            }
            yield return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_botHealth == 0)
            {
                StopCoroutine(Shoot());
                _botHealthBar.gameObject.SetActive(false);
                Destroy(gameObject);
                _spawner.Spawner();
            }
            if (other.GetComponent<Bullet>())
            {
                _botHealth -= 10;
                _botHealthBar.value = _botHealth;
            }
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