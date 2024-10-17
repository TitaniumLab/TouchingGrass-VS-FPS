using Unity.Entities;
using UnityEngine;

namespace GrassVsFps
{
    public struct GrassSpawnerComponent : IComponentData
    {
        public Entity GrassPrefab;
        public float PrefabScale;
    }

    public class GrassSpawnerAuthoring : MonoBehaviour
    {
        public GameObject GrassPrefab;
        public float PrefabScale;
    }

    public class GrassSpawnerBaker : Baker<GrassSpawnerAuthoring>
    {
        public override void Bake(GrassSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new GrassSpawnerComponent
            {
                GrassPrefab = GetEntity(authoring.GrassPrefab, TransformUsageFlags.Dynamic),
                PrefabScale = authoring.PrefabScale
            });
        }
    }
}
