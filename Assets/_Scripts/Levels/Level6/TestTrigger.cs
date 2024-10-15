using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace GrassVsFps
{
    [BurstCompile]
    public partial struct TestTrigger : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new TestJob().Schedule(state.Dependency);
        }

    }

    [BurstCompile]
    internal struct TestJob : ITriggerEventsJob
    {
        [BurstCompile]
        public void Execute(TriggerEvent triggerEvent)
        {
            Debug.Log("event");
        }
    }
}
