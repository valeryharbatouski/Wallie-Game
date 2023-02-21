using UnityEngine;

namespace Valery
{
    public interface IEnemyStateContext
    {
        Transform MainTransform { get;  }
        void SwitchState<TEnemyState>(StateArgs arg = null) where TEnemyState : AbstractEnemyState;
        
    }
}