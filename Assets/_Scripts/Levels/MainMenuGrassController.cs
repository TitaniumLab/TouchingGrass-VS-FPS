using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace GrassVsFps
{
    public class MainMenuGrassController : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private float _grassScale = 0.7f;
        [SerializeField] private int _maxGrassCount;
        [SerializeField] private SimpleTouchController _touchController;
        private Transform _toucherTransform;

        private Transform[] _transforms;
        private Vector3[] _cachedPositions;
        private bool[] _isTouched;


        private void Awake()
        {
            _transforms = new Transform[_maxGrassCount];

            var groundSize = transform.lossyScale;
            var pos = transform.position;
            var yPos = pos.y + 10;

            for (int i = 0; i < _maxGrassCount; i++)
            {
                float xPos = Random.Range(pos.x - groundSize.x / 2, pos.x + groundSize.x / 2);
                float zPos = Random.Range(pos.z - groundSize.z / 2, pos.z + groundSize.z / 2);
                var startPos = new Vector3(xPos, yPos, zPos);

                if (Physics.Raycast(startPos, Vector3.down, out RaycastHit hitInfo))
                {
                    _transforms[i] = Instantiate(_prefab, hitInfo.point, Quaternion.identity, _parent).transform;
                    _transforms[i].localScale = Vector3.one * _grassScale;
                }
            }

            _transforms = _transforms.Where(x => x != null).ToArray();
            _cachedPositions = new Vector3[_transforms.Length];
            _isTouched = new bool[_transforms.Length];

            for (int i = 0; i < _transforms.Length; i++)
            {
                _transforms[i].gameObject.AddComponent<MeshCollider>().convex = true;
                _transforms[i].gameObject.AddComponent<Touchable>().Init(i);


                _cachedPositions[i] = _transforms[i].position;
            }

            _toucherTransform = _touchController.transform;
            Touchable.OnTouch += ChangeTouchState;
        }

        private void OnDestroy()
        {
            Touchable.OnTouch -= ChangeTouchState;
        }

        private void Update()
        {
            var time = Time.time;
            var touchPos = _toucherTransform.position;

            for (int i = 0; i < _transforms.Length; i++)
            {
                if (!_isTouched[i])
                {
                    var rot = _cachedPositions[i].GetNoiseRotation(time);
                    _transforms[i].rotation = rot;
                }
                else
                {
                    var rot = _cachedPositions[i].GetTouchRotation(touchPos, _touchController.TouchRadius, _touchController.MaxTouchAngle);
                    _transforms[i].rotation = rot;
                }

            }
        }


        private void ChangeTouchState(int index, bool isTouched)
        {
            _isTouched[index] = isTouched;
        }
    }
}
