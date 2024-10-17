using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


namespace GrassVsFps
{

    public partial class SpawnerSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<TouchSpawnerComponent>();
            RequireForUpdate<GrassSpawnerComponent>();
        }


        protected override void OnUpdate()
        {
            // Spawn grass
            Entity grassPrefab = SystemAPI.GetSingleton<GrassSpawnerComponent>().GrassPrefab;
            GridTool.CreateGrid((Vector3 pos, int index) =>
            {
                var grassEntity = EntityManager.Instantiate(grassPrefab);
                EntityManager.AddComponent(grassEntity, typeof(GrassComponent));
                EntityManager.SetComponentData(grassEntity, LocalTransform.FromPositionRotationScale(pos, quaternion.identity, 1));
            });

            // Spawn Toucher
            if (TouchController.IsTouching)
            {
                Entity touchPrefab = SystemAPI.GetSingleton<TouchSpawnerComponent>().ToucherPrefab;
                var touchEntity = EntityManager.Instantiate(touchPrefab);
                EntityManager.AddComponent(touchEntity, typeof(TouchComponent));
                EntityManager.SetName(touchEntity, new Unity.Collections.FixedString64Bytes("Toucher"));
                TouchController.TouchEntity = touchEntity;
            }

            // Disable after first frame
            Enabled = false;
        }
    }
}
