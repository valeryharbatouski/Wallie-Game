using System;
using System.Collections;
using Enemy_States;

namespace UnityEngine
{
    public class ChaseEnemyState : AbstractEnemyState
    {
        [SerializeField] private float _speedDefault = 2.0f;
        [SerializeField] private float _maxDistanceToTarget = 5.0f;
        
        private float _speed;

        protected override IEnumerator HandleStart()
        {
            if (StateArgs is not ChaseEnemyArgs chaseEnemyArgs)
            {
                throw new ArgumentException();
            }

            while (true)
            {
                if (Vector3.Distance(chaseEnemyArgs.Transform.position, StateContext.MainTransform.position) < _maxDistanceToTarget)
                {
                    _speed = 0;
                }
                else
                {
                    _speed = _speedDefault;
                }
                
                StateContext.MainTransform.position = Vector3.MoveTowards(StateContext.MainTransform.position,
                    chaseEnemyArgs.Transform.position,  _speed * Time.deltaTime);
               
                
                
                yield return null;
            }
        }
    }
}