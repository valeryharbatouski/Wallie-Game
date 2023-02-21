
using System.Collections;
using UnityEngine;

namespace Valery
{
    public class StayEnemyState : AbstractEnemyState
    {
        [SerializeField]private float _stayingTime = 3.0f;

        protected override IEnumerator HandleStart()
        {
            yield return new WaitForSeconds(_stayingTime);
            StateContext.SwitchState<PatrolEnemyState>();
        }
    }
}