using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace GrassVsFps
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    [BurstCompile]
    public partial struct TestTrigger : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new TriggerJob().Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        }
    }

    internal partial struct TriggerJob : ITriggerEventsJob
    {
        [BurstCompile]
        public void Execute(TriggerEvent triggerEvent)
        {
        }
    }
}
