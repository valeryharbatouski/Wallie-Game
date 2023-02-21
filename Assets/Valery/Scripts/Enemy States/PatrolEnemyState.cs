using System.Collections;
using DG.Tweening;
using UnityEngine;


namespace Valery
{
    [RequireComponent(typeof(Collider))]
    public class PatrolEnemyState : AbstractEnemyState
    {
        [SerializeField]private Transform[] _points;
        [SerializeField]private float _speed;

        private Collider _collider;

        private Tween _currentMovement; 
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        protected override IEnumerator HandleStart()
        {
            while (true)
                {
                    foreach (var _point in _points)
                    {
                        _currentMovement = StateContext.MainTransform.DOMove(_point.position, _speed)
                            .SetSpeedBased();
                        yield return _currentMovement.WaitForCompletion();

                        if (_currentMovement == null)
                        {
                            yield break;
                            
                        } 
                    }
                }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                var movement = _currentMovement;
                _currentMovement = null;
                movement.Kill();
                
                StateContext.SwitchState<ChaseEnemyState>(new ChaseEnemyArgs(player.transform));
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                StateContext.SwitchState<PatrolEnemyState>();
            }
        }
    }
}