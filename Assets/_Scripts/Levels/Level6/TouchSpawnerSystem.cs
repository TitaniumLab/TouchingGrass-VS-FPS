using Unity.Entities;

namespace GrassVsFps
{
    public partial class TouchSpawnerSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<TouchSpawnerComponent>();
        }
        protected override void OnUpdate()
        {
            //if (TouchController.IsTouching)
            //{
            //    return;
            //}

            Entity prefab = SystemAPI.GetSingleton<TouchSpawnerComponent>().ToucherPrefab;

            var entity = EntityManager.Instantiate(prefab);
            EntityManager.AddComponent(entity, typeof(TouchComponent));
            EntityManager.SetName(entity, new Unity.Collections.FixedString64Bytes("Toucher"));
            TouchController.TouchEntity = entity;
            Enabled = false;
        }
    }
}
