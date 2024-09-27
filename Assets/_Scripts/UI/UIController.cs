using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrassVsFps
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private MyHUD _myHudPrefab;
        public MyHUD CurrentHUD { get; private set; }
        public static UIController Instance;
        private const int FPS_TOTAL_SAMPLES = 5;
        private float[] _fpsSamples = new float[FPS_TOTAL_SAMPLES];
        private int _fpsSampleIndex;



        private void Awake()
        {
            Instance = this;

            CurrentHUD = Instantiate(_myHudPrefab, transform);
            Application.targetFrameRate = -1;
            InvokeRepeating(nameof(UpdateFPS), 0, 0.5f);
        }


        private void OnEnable()
        {
            CurrentHUD.NextSceneButton.onClick.AddListener(delegate { SceneSwap.ToNextScene(); });
            CurrentHUD.ToMenuButton.onClick.AddListener(delegate { SceneManager.LoadScene("MainMenu"); });
        }


        private void OnDisable()
        {
            CurrentHUD.NextSceneButton.onClick.RemoveListener(delegate { SceneSwap.ToNextScene(); });
            CurrentHUD.ToMenuButton.onClick.RemoveListener(delegate { SceneManager.LoadScene("MainMenu"); });
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
            CurrentHUD.FpsVolue = fps.ToString();
        }
    }
}