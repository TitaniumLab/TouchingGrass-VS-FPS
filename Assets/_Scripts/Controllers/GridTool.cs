using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrassVsFps
{
    public class GridTool : MonoBehaviour
    {
        [SerializeField] private CustomSlider _slider;
        private const int GROUND_SIZE = 100;
        private static int _grassDencity = 1;
        public static int GetUnitsCount => (int)Mathf.Pow(GROUND_SIZE * _grassDencity, 2);


        private void Awake()
        {
            _slider.value = _grassDencity;
        }


        public void RebuildGrid()
        {
            _grassDencity = Mathf.CeilToInt(_slider.value);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        public static void CreateGrid(Action<Vector3> action)
        {
            int grassInRow = GROUND_SIZE * _grassDencity;
            float distance = (float)GROUND_SIZE / grassInRow;
            for (int x = 0; x < grassInRow; x++)
            {
                for (int z = 0; z < grassInRow; z++)
                {
                    action(new Vector3(x * distance, 0, z * distance));
                }
            }
        }
    }
}
