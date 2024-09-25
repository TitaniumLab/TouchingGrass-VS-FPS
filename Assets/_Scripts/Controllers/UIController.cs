using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GrassVsFps
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private Toggle _touchToggle;
        [SerializeField] private TextMeshProUGUI _touchTypeText;
        [SerializeField] private TextMeshProUGUI _sceneNameText;
        public static UIController Instance;
        private const int FPS_TOTAL_SAMPLES = 5;
        private float[] _fpsSamples = new float[FPS_TOTAL_SAMPLES];
        private int _fpsSampleIndex;

        public Toggle TouchToggle { get { return _touchToggle; } }


        private void Awake()
        {
            Instance = this;

            Application.targetFrameRate = -1;
            InvokeRepeating(nameof(UpdateFPS), 0, 0.5f);
        }


        private void OnDestroy()
        {
            _touchToggle.onValueChanged.RemoveAllListeners();
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
            fps = Mathf.Clamp(fps, 0, 999999);
            _fpsText.text = $"FPS: {fps}";
        }


        public void SetCountText(int count)
        {
            _countText.text = $"Count: {count}";
        }


        public void SetTouchTypeText(string text)
        {
            _touchTypeText.text = text;
        }

        public void SetSceneName(string sceneName)
        {
            _sceneNameText.text = sceneName;
        }
    }
}