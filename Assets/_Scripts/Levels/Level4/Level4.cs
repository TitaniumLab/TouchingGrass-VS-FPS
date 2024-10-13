using UnityEngine;

namespace GrassVsFps
{
    public class Level4 : MonoBehaviour
    {
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;
        [SerializeField] private Vector3 _scale = new Vector3(1, 1, 1);
        [SerializeField] private TouchController _touchController;
        private Transform _touchTransform;
        [SerializeField] private Touchable _touchablePrefab;
        [SerializeField] private Transform _parentTransform;
        private Vector3[] _positions;
        private Matrix4x4[] _matrix;
        private RenderParams _renderParams;
        private bool[] _isTouched;


        private void Start()
        {
            var count = GridTool.GetUnitsCount;
            _positions = new Vector3[count];
            _matrix = new Matrix4x4[count];
            _isTouched = new bool[count];
            _touchTransform = _touchController.transform;

            _renderParams = new RenderParams(_material);
            _renderParams.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            GridTool.CreateGrid((Vector3 pos, int i) =>
            {
                _positions[i] = pos;
                if (TouchController.IsTouching)
                    Instantiate(_touchablePrefab, pos, Quaternion.identity, _parentTransform).Init(i);
            });

            Touchable.OnTouch += ChangeTouchState;

            UIController.Instance.CurrentHUD.SetCountVolue = count.ToString();
            UIController.Instance.CurrentHUD.SetSceneName = "GPU Instancing";
        }


        private void OnDestroy()
        {
            Touchable.OnTouch -= ChangeTouchState;
        }


        private void Update()
        {
            var time = Time.time;
            var touchPos = _touchTransform.position;
            for (int i = 0; i < _positions.Length; i++)
            {
                if (!_isTouched[i])
                {
                    Quaternion rot = _positions[i].GetNoiseRotation(time);
                    _matrix[i].SetTRS(_positions[i], rot, _scale);
                }
                else
                {
                    var rot = _positions[i].GetTouchRotation(touchPos, _touchController.ColliderRadius, _touchController.MaxTouchAngle);
                    _matrix[i].SetTRS(_positions[i], rot, _scale);
                }
            }

            Graphics.RenderMeshInstanced(_renderParams, _mesh, 0, _matrix);
        }


        private void ChangeTouchState(int index, bool isTouched)
        {
            _isTouched[index] = isTouched;
        }
    }
}
