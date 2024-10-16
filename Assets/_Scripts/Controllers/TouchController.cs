using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrassVsFps
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class TouchController : MonoBehaviour
    {
        [Tooltip("Normal to the plane"), SerializeField]
        private Vector3 _touchPlaneNorm = Vector3.up;
        [Tooltip("Distance from the toucher to the plane while touching"), SerializeField]
        private float _minDistance = 1.0f;
        private float _maxDistance;
        [SerializeField]
        private float _verticalSpeed = 1f;
        [SerializeField]
        private float _maxTouchAngle = 70;
        private Toggle _toggle;
        private float _relDis = 1.0f;
        private Plane _plane;
        private Camera _camera;
        private Vector3 _pos;
        private float _collRad;
        private static bool _canTouch = false;
        private bool _isTouching = false;
        private EntityManager _entityManager;
        public static Entity TouchEntity { get; set; }
        public static bool IsTouching { get { return _canTouch; } }
        public float MaxTouchAngle { get { return _maxTouchAngle; } }
        public float ColliderRadius { get { return _collRad; } }


        #region Internal
        private void Awake()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            _collRad = GetComponent<SphereCollider>().radius;
            _maxDistance = _minDistance + _collRad;

            _camera = Camera.main;

            _plane = new Plane(_touchPlaneNorm, 0);
        }

        private void OnEnable()
        {
            _toggle = UIController.Instance.CurrentHUD.TouchToggle;
            _toggle.isOn = _canTouch;
            _toggle.onValueChanged.AddListener(delegate { Rebuild(); });
        }

        private void OnDisable()
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
                touchable.Touching(_pos, _collRad, _maxTouchAngle);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ITouchable touchable))
            {
                touchable.StopTouching();
            }
        }

        // Calc position of couch controller
        void Update()
        {
            _pos = transform.position;

            if (_isTouching && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                // If can raycast on plane
                if (_plane.Raycast(ray, out float enter))
                {
                    var hit = ray.GetPoint(enter);
                    _relDis = _relDis > 0 ? _relDis - Time.deltaTime * _verticalSpeed : 0;
                    transform.position = CalculateVerticalPosition(hit);
                    _entityManager.SetComponentData(TouchEntity, LocalTransform.FromPosition(hit));
                }
                else
                {
                    _relDis = _relDis < 1 ? _relDis + Time.deltaTime * _verticalSpeed : 1;
                    var pos = transform.position;
                    transform.position = CalculateVerticalPosition(pos);
                }
            }
            else if (_relDis < 1)
            {
                _relDis = _relDis < 1 ? _relDis + Time.deltaTime * _verticalSpeed : 1;
                var pos = transform.position;
                transform.position = CalculateVerticalPosition(pos);
            }
        }
        #endregion

        #region Methods
        public void Rebuild()
        {
            _canTouch = _toggle.isOn;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        public void OnTouch(InputAction.CallbackContext context)
        {
            if (Cursor.visible)
                switch (context.ReadValue<float>())
                {
                    case 1:
                        {
                            _isTouching = true;
                            break;
                        }
                    case 0:
                        {
                            _isTouching = false;
                            break;
                        }
                }
        }


        private Vector3 CalculateVerticalPosition(Vector3 pos)
        {
            var dis = Mathf.Lerp(_minDistance, _maxDistance, _relDis);
            pos[1] = dis;
            return pos;
        }
        #endregion
    }
}
