using Unity.Entities;
using UnityEngine;

namespace GrassVsFps
{
    public class Level6 : MonoBehaviour
    {
        private World _world;
        private GrassSpawnerSystem _spawnerSystem;
        private TouchSpawnerSystem _touchSpawnerSystem;

        private void Start()
        {
            _world = World.DefaultGameObjectInjectionWorld;
            _spawnerSystem = _world.GetExistingSystemManaged<GrassSpawnerSystem>();
            _touchSpawnerSystem = _world.GetExistingSystemManaged<TouchSpawnerSystem>();
            _spawnerSystem.Enabled = true; // Necessary when returning to the scene
            _touchSpawnerSystem.Enabled = true;

            UIController.Instance.CurrentHUD.SetCountVolue = GridTool.GetUnitsCount.ToString();
            UIController.Instance.CurrentHUD.SetSceneName = "DOTS";
        }
    }
}
