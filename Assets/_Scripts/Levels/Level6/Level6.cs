using Unity.Entities;
using UnityEngine;

namespace GrassVsFps
{
    public class Level6 : MonoBehaviour
    {
        private World _world;
        private GrassSpawnerSystem _spawnerSystem;

        private void Start()
        {
            _world = World.DefaultGameObjectInjectionWorld;
            _spawnerSystem = _world.GetExistingSystemManaged<GrassSpawnerSystem>();
            _spawnerSystem.Enabled = true; // Necessary when returning to the scene

            UIController.Instance.CurrentHUD.SetCountVolue = GridTool.GetUnitsCount.ToString();
            UIController.Instance.CurrentHUD.SetSceneName = "DOTS";
        }
    }
}
