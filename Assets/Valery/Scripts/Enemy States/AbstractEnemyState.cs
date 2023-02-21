using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Valery
{
    public abstract class AbstractEnemyState : MonoBehaviour
    {
        public event Action<AbstractEnemyState> Started; 
        private IEnemyStateContext _enemyStateContext;
        private StateArgs _stateArgs;

        protected IEnemyStateContext StateContext => _enemyStateContext;
        protected StateArgs StateArgs => _stateArgs ;

        public void OnStart(IEnemyStateContext stateContext, [CanBeNull]StateArgs args)
        {
            _enemyStateContext = stateContext;
            _stateArgs = args;
            Started?.Invoke(this);
            StartCoroutine(HandleStart());
        }

        protected virtual IEnumerator HandleStart()
        {
            yield break;
        }
    }
}