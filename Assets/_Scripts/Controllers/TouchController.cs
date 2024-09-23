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


        #region Internal
        private void Awake()
        {
            _toggle.isOn = _isTouching;
            _toggle.onValueChanged.AddListener(delegate { Rebuild(); });
        }


        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(delegate { Rebuild(); });
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITouchable touchable))
            {
                touchable.StartTouching();
            }
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out ITouchable touchable))
            {
                //touchable.Touching(
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ITouchable touchable))
            {
                touchable.StopTouching();
            }
        }


        void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                Debug.Log("LeftClick");
            }
        }
        #endregion

        #region Methods
        public void Rebuild()
        {
            _isTouching = _toggle.isOn;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        #endregion
    }
}
