using Unity.Burst;
using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace GrassVsFps
{
    public partial class GrassSpawnerSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            if (!EntityManager.CreateEntityQuery(typeof(GrassComponent)).IsEmpty)
            {
                Enabled = false;
                return;
            }

            Entity prefab = new Entity();
            float scale;
            foreach (RefRW<GrassSpawnerComponent> spawner in SystemAPI.Query<RefRW<GrassSpawnerComponent>>())
            {
                prefab = spawner.ValueRW.GrassPrefab;
                scale = spawner.ValueRW.PrefabScale;
                Debug.Log(scale);
                EntityManager.SetComponentData(prefab, LocalTransform.FromScale(scale));
            }

            var buffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

            GridTool.CreateGrid((Vector3 pos, int index) =>
            {
                var entity = EntityManager.Instantiate(prefab);
                EntityManager.AddComponent(entity, typeof(GrassComponent));
                EntityManager.SetComponentData(entity, LocalTransform.FromPosition(pos));
            });
        }
    }
}
