using Unity.Entities;
using UnityEngine;

namespace GrassVsFps
{
    public class Level6 : MonoBehaviour
    {
        private World _world;
        private SpawnerSystem _spawnerSystem;
        private SystemHandle _touchingSystem;

        private void Start()
        {
            _world = World.DefaultGameObjectInjectionWorld;
            _spawnerSystem = _world.GetExistingSystemManaged<SpawnerSystem>();
            _touchingSystem = _world.GetExistingSystem<TouchingSystem>();
            _spawnerSystem.Enabled = true; // Necessary when returning to the scene

            UIController.Instance.CurrentHUD.SetCountVolue = GridTool.GetUnitsCount.ToString();
            UIController.Instance.CurrentHUD.SetSceneName = "DOTS";
        }
    }
}
