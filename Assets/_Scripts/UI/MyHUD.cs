using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GrassVsFps
{
    public class MyHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private TextMeshProUGUI _sceneName;


        public string SetFpsVolue { set { _fpsText.text = $"FPS: {value}"; } }
        public string SetCountVolue { set { _countText.text = $"Count: {value}"; } }
        public string SetSceneName { set { _sceneName.text = value; } }


        [field: SerializeField] public Toggle TouchToggle { get; private set; }
        [field: SerializeField] public CustomSlider CustomSlider { get; private set; }
        [field: SerializeField] public Button NextSceneButton { get; private set; }
        [field: SerializeField] public Button ToMenuButton { get; private set; }
    }
}
