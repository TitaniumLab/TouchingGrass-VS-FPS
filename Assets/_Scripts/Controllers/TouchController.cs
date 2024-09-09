using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrassVsFps
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class TouchController : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        private static bool _isTouching = false;
        public static bool IsTouching { get { return _isTouching; } }

        private void Awake()
        {
            _toggle.isOn = _isTouching;
            _toggle.onValueChanged.AddListener(delegate { Rebuild(); });
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(delegate { Rebuild(); });
        }

        public void Rebuild()
        {
            _isTouching = _toggle.isOn;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITouchable touchable))
            {
                Debug.Log("Enter");
            }
        }

        void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                Debug.Log("LeftClick");
            }
        }
    }
}
