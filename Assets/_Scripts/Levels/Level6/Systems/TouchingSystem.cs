using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace GrassVsFps
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    [BurstCompile]
    public partial struct TouchingSystem : ISystem
    {
        private NativeList<TriggerEvent> _triggerEvents;
        // The way to get the radius of colliders in ECC is weird. Just set it here
        private float _maxDistance;
        private float _maxTouchAngle;

        public void OnCreate(ref SystemState state)
        {
            _triggerEvents = new NativeList<TriggerEvent>(1, Allocator.Persistent);
            _maxDistance = 11;
            _maxTouchAngle = 70;
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


            if (!_triggerEvents.IsEmpty)
            {
                for (global::System.Int32 i = 0; i < _triggerEvents.Length; i++)
                {
                    var grassPos = state.EntityManager.GetComponentData<LocalTransform>(_triggerEvents[i].EntityB).Position;
                    var touchPos = state.EntityManager.GetComponentData<LocalTransform>(_triggerEvents[i].EntityA).Position;
                    var rot = grassPos.GetBurstTouchRotation(touchPos, _maxDistance, _maxTouchAngle);
                    state.EntityManager.SetComponentData(_triggerEvents[i].EntityB, LocalTransform.FromPositionRotationScale(grassPos, rot, 1));
                }
            }
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

    //[BurstCompile]
    //internal partial struct AnimateTouchJob : IJobFor
    //{
    //    public NativeList<TriggerEvent> Triggers;
    //    public EntityCommandBuffer Ecb;
    //    public EntityManager Manager;

    //    [BurstCompile]
    //    public void Execute(int index)
    //    {
    //        var grassEntity = Triggers[index].EntityB;
    //        var touchEntity = Triggers[index].EntityA;
    //        //var maxDistance = 
    //        // var rot = Manager.GetComponentData<LocalTransform>(grassEntity).Position.GetBurstTouchRotation(touchPos,)
    //    }
    //}
}