using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace GrassVsFps
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    [BurstCompile]
    public partial struct TouchingSystem : ISystem
    {
        // Animation using list iteration reduces fps by a third when touching
        // Moved the implementation to the animation system
        private NativeList<TriggerEvent> _triggerEvents;


        public void OnCreate(ref SystemState state)
        {
            _triggerEvents = new NativeList<TriggerEvent>(1, Allocator.Persistent);
            state.RequireForUpdate<TouchComponent>();
            state.RequireForUpdate<GrassComponent>();
        }


        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // Clear events list
            if (!_triggerEvents.IsEmpty)
            {
                new ClearJob
                {
                    Triggers = _triggerEvents,
                    Ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                    .CreateCommandBuffer(state.WorldUnmanaged)
                }.Schedule(_triggerEvents.Length, state.Dependency).Complete();
                _triggerEvents.Clear();
            }

            // Get new events
            new TriggerJob
            {
                Triggers = _triggerEvents,
                Ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency).Complete();
        }


        public void OnDestroy(ref SystemState state)
        {
            _triggerEvents.Dispose();
        }
    }


    [BurstCompile]
    internal partial struct TriggerJob : ITriggerEventsJob
    {
        public NativeList<TriggerEvent> Triggers;
        public EntityCommandBuffer Ecb;

        [BurstCompile]
        public void Execute(TriggerEvent triggerEvent)
        {
            Ecb.SetComponent(triggerEvent.EntityB, new GrassComponent { IsTouched = true });
            Triggers.Add(triggerEvent);
        }
    }


    [BurstCompile]
    internal partial struct ClearJob : IJobFor
    {
        public NativeList<TriggerEvent> Triggers;
        public EntityCommandBuffer Ecb;

        [BurstCompile]
        public void Execute(int index)
        {
            Ecb.SetComponent(Triggers[index].EntityB, new GrassComponent { IsTouched = false });
        }
    }
}