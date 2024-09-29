using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrassVsFps
{
    public class GridTool : MonoBehaviour
    {
        private CustomSlider _slider;
        private const int GROUND_SIZE = 100;
        private static int _grassDencity = 1;
        public static int GetUnitsCount => (int)Mathf.Pow(GROUND_SIZE * _grassDencity, 2);

        private void Start()
        {
            _slider = UIController.Instance.CurrentHUD.CustomSlider;
            _slider.value = _grassDencity;
            _slider.OnEndDragAndValueChanged.AddListener(delegate { RebuildGrid(); });
        }

        private void OnDestroy()
        {
            _slider?.OnEndDragAndValueChanged.RemoveListener(delegate { RebuildGrid(); });
        }


        public void RebuildGrid()
        {
            _grassDencity = Mathf.CeilToInt(_slider.value);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        public static void CreateGrid(Action<Vector3, int> action)
        {
            int grassInRow = GROUND_SIZE * _grassDencity;
            float distance = (float)GROUND_SIZE / grassInRow;
            int i = 0;
            for (int x = 0; x < grassInRow; x++)
            {
                for (int z = 0; z < grassInRow; z++)
                {
                    action(new Vector3(x * distance, 0, z * distance), i);
                    i++;
                }
            }
        }
    }
}
