using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace GrassVsFps
{
    [BurstCompile]
    public partial struct AnimationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GrassComponent>();
        }


        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new AnimateJob
            {
                Time = (float)SystemAPI.Time.ElapsedTime
            }.ScheduleParallel(state.Dependency).Complete();
        }
    }

    [BurstCompile]
    internal partial struct AnimateJob : IJobEntity
    {
        public float Time;

        [BurstCompile]
        private void Execute(ref GrassComponent grass, ref LocalTransform transform)
        {
            if (!grass.IsTouched)
            {
                var rot = transform.Position.GetBurstNoiseRotation(Time);
                //  For some reason "transform.Rotate(rot)" doesn't work
                transform.Rotation = rot;
            }
        }
    }
}
