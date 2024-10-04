using UnityEngine;

namespace GrassVsFps
{
    public class Level4 : MonoBehaviour
    {
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;
        [SerializeField] private Vector3 _scale = new Vector3(100, 100, 100);
        private Vector3[] _positions;
        private Matrix4x4[] _matrix;
        private RenderParams _renderParams;


        private void Start()
        {
            var count = GridTool.GetUnitsCount;
            _positions = new Vector3[count];
            _matrix = new Matrix4x4[count];
            _renderParams = new RenderParams(_material);
            _renderParams.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            GridTool.CreateGrid((Vector3 pos, int i) => { _positions[i] = pos; });

            UIController.Instance.CurrentHUD.SetCountVolue = count.ToString();
            UIController.Instance.CurrentHUD.TouchToggle.interactable = false;
            UIController.Instance.CurrentHUD.SetTouchTypeText = "Game objects doesn't created";
            UIController.Instance.CurrentHUD.SetSceneName = "GPU Instancing";
        }


        private void Update()
        {
            var time = Time.time;
            for (int i = 0; i < _positions.Length; i++)
            {
                Quaternion rot = _positions[i].GetNoiseRotation(time);
                _matrix[i].SetTRS(_positions[i], rot, _scale);
            }

            Graphics.RenderMeshInstanced(_renderParams, _mesh, 0, _matrix);
        }
    }
}
