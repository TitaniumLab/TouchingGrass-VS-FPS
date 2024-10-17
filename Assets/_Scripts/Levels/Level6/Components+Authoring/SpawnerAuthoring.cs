using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace GrassVsFps
{
    public struct Spawner : IComponentData
    {
        public Entity Prefab;
        public float3 SpawnPos;
        public float NextSpawnTime;
        public float SpawnRate;
    }

    public class SpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float SpawnRate;
    }

    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Spawner
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPos = authoring.transform.position,
                NextSpawnTime = 0.0f,
                SpawnRate = authoring.SpawnRate
            });
        }
    }
}
