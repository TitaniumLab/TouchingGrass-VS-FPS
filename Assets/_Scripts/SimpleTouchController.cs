using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace GrassVsFps
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class SimpleTouchController : MonoBehaviour
    {
        [SerializeField] private LayerMask _mask;
        [field: SerializeField] public float MaxTouchAngle = 89;
        public float TouchRadius { get; private set; }
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            TouchRadius = GetComponent<SphereCollider>().radius;
        }

        private void Update()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, _mask.value))
            {
                transform.position = hitInfo.point;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITouchable touchable))
            {
                touchable.StartTouching();
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ITouchable touchable))
            {
                touchable.StopTouching();
            }
        }
    }
}
