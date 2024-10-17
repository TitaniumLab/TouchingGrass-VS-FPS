using Unity.Entities;
using UnityEngine;

namespace GrassVsFps
{
    public class ToucherSpawnerAuthoring : MonoBehaviour
    {
        public GameObject TouchPrefab;
    }

    public struct TouchSpawnerComponent : IComponentData
    {
        public Entity ToucherPrefab;
    }

    public class TouchSpawnerBaker : Baker<ToucherSpawnerAuthoring>
    {
        public override void Bake(ToucherSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new TouchSpawnerComponent
            {
                ToucherPrefab = GetEntity(authoring.TouchPrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}
