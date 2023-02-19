using Enemy_States.StateArgs;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IEnemyStateContext
    {
        Transform MainTransform { get;  }
        void SwitchState<TEnemyState>(StateArgs arg = null) where TEnemyState : AbstractEnemyState;
        
    }
}