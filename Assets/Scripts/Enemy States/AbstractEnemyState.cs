using System;
using System.Collections;
using DefaultNamespace;
using Enemy_States.StateArgs;
using JetBrains.Annotations;

namespace UnityEngine
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