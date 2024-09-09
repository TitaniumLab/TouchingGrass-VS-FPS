using TMPro;
using UnityEngine;

namespace GrassVsFps
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText;
        [SerializeField] private TextMeshProUGUI _countText;
        public static UIController Instance;
        private const int FPS_TOTAL_SAMPLES = 20;
        private float[] _fpsSamples = new float[FPS_TOTAL_SAMPLES];
        private int _fpsSampleIndex;


        private void Awake()
        {
            Instance = this;

            Application.targetFrameRate = -1;
            InvokeRepeating(nameof(UpdateFPS), 0, 0.5f);
        }


        private void Update()
        {
            _fpsSamples[_fpsSampleIndex++] = Time.deltaTime;
            if (_fpsSampleIndex >= FPS_TOTAL_SAMPLES)
            {
                _fpsSampleIndex = 0;
            }
        }


        private void UpdateFPS()
        {
            float totalTime = 0;
            for (int i = 0; i < FPS_TOTAL_SAMPLES; i++)
            {
                totalTime += _fpsSamples[i];
            }
            int fps = (int)(1 / (totalTime / FPS_TOTAL_SAMPLES));
            _fpsText.text = $"FPS: {fps}";
        }


        public void SetCountText(int count)
        {
            _countText.text = $"Count: {count}";
        }
    }
}