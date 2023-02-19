using UnityEngine;

namespace Enemy_States
{
    public record ChaseEnemyArgs(Transform Transform) : StateArgs.StateArgs;
}