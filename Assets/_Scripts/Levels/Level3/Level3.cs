using UnityEngine;

namespace GrassVsFps
{
    /// <summary>
    /// After creating the ECS scene, dynamic batching stopped working, but the scene remained
    /// </summary>
    public class Level3 : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private TouchController _touchController;
        private Transform _touchTransform;
        private Transform[] _transforms;
        private Vector3[] _positions;
        private bool[] _isTouched;

        private void Start()
        {
            _touchTransform = _touchController.transform;

            var count = GridTool.GetUnitsCount;
            _transforms = new Transform[count];
            _positions = new Vector3[count];
            _isTouched = new bool[count];

            GridTool.CreateGrid((Vector3 pos, int i) =>
            {
                var obj = Instantiate(_prefab, pos, Quaternion.identity, _parent);
                if (TouchController.IsTouching)
                {
                    obj.AddComponent<MeshCollider>().convex = true;
                    obj.AddComponent<Touchable>().Init(i);
                }

                _transforms[i] = obj.transform;
                _positions[i] = pos;
            });
            Touchable.OnTouch += ChangeTouchState;

            UIController.Instance.CurrentHUD.SetCountVolue = count.ToString();
            UIController.Instance.CurrentHUD.SetSceneName = "Draw call batching";
        }


        private void OnDestroy()
        {
            Touchable.OnTouch -= ChangeTouchState;
        }


        private void Update()
        {
            var time = Time.time;
            var touchPos = _touchTransform.position;
            for (int i = 0; i < _transforms.Length; i++)
            {
                if (!_isTouched[i])
                {
                    var rot = _positions[i].GetNoiseRotation(time);
                    _transforms[i].rotation = rot;
                }
                else
                {
                    var rot = _positions[i].GetTouchRotation(touchPos, _touchController.ColliderRadius, _touchController.MaxTouchAngle);
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

