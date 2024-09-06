using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrassVsFps
{
    public class GridTool : MonoBehaviour
    {
        [SerializeField] private CustomSlider _slider;
        private const int _groundSize = 100;
        private static int _grassDencity = 1;

        private void Awake()
        {
            _slider.value = _grassDencity;
        }


        public void RebuildGrid()
        {
            _grassDencity = Mathf.CeilToInt(_slider.value);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log(_slider.value);
        }


        public static void CreateGrid(Action<Vector3> action)
        {
            int grassInRow = _groundSize * _grassDencity;
            float distance = (float)_groundSize / grassInRow;
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
