using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace GrassVsFps
{
    public partial class GrassSpawnerSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<GrassSpawnerComponent>();
        }

        protected override void OnUpdate()
        {
            Entity prefab = SystemAPI.GetSingleton<GrassSpawnerComponent>().GrassPrefab;

            GridTool.CreateGrid((Vector3 pos, int index) =>
            {
                var entity = EntityManager.Instantiate(prefab);
                EntityManager.AddComponent(entity, typeof(GrassComponent));
                EntityManager.SetComponentData(entity, LocalTransform.FromPositionRotationScale(pos, quaternion.identity, 1));
            });

            Enabled = false;
        }
    }
}
