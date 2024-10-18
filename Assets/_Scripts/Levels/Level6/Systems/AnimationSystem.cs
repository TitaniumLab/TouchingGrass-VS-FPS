using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GrassVsFps
{
    [BurstCompile]
    public partial struct AnimationSystem : ISystem
    {
        // The way to get the radius of colliders in ECC is weird. Just set it here
        private float _maxDistance;
        private float _maxTouchAngle;


        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GrassComponent>();
            _maxDistance = 11;
            _maxTouchAngle = 70;
        }


        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float3 pos = float3.zero;
            if (SystemAPI.TryGetSingletonEntity<TouchComponent>(out Entity toucher))
            {
                pos = state.EntityManager.GetComponentData<LocalTransform>(toucher).Position;
            }

            new AnimateJob
            {
                Time = (float)SystemAPI.Time.ElapsedTime,
                TouchPos = pos,
                MaxTouchDistance = _maxDistance,
                MaxTouchAngle = _maxTouchAngle
            }.ScheduleParallel(state.Dependency).Complete();
        }
    }

    [BurstCompile]
    internal partial struct AnimateJob : IJobEntity
    {
        public float Time;
        public float3 TouchPos;
        public float MaxTouchDistance;
        public float MaxTouchAngle;

        [BurstCompile]
        private void Execute(ref GrassComponent grass, ref LocalTransform transform)
        {
            if (!grass.IsTouched)
            {
                var rot = transform.Position.GetBurstNoiseRotation(Time);
                //  For some reason "transform.Rotate(rot)" doesn't work
                transform.Rotation = rot;
            }
            else
            {
                var rot = transform.Position.GetBurstTouchRotation(TouchPos, MaxTouchDistance, MaxTouchAngle);
                transform.Rotation = rot;
            }
        }
    }
}
