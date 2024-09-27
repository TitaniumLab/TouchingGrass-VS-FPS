using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GrassVsFps
{
    public class MyHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private TextMeshProUGUI _touchTypeText;
        [SerializeField] private TextMeshProUGUI _sceneName;


        public string FpsVolue { set { _fpsText.text = $"FPS: {value}"; } }
        public string CountVolue { set { _countText.text = $"Count: {value}"; } }
        public string TouchTypeText { set { _touchTypeText.text = value; } }
        public string SceneName { set { _sceneName.text = value; } }


        [field: SerializeField] public Toggle TouchToggle { get; private set; }
        [field: SerializeField] public CustomSlider CustomSlider { get; private set; }
        [field: SerializeField] public Button NextSceneButton { get; private set; }
        [field: SerializeField] public Button ToMenuButton { get; private set; }
    }
}
