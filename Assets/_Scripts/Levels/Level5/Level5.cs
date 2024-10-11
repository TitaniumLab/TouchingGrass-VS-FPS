using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace GrassVsFps
{
    public class Level5 : MonoBehaviour
    {
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;
        [SerializeField] private Vector3 _scale = new Vector3(100, 100, 100);
        [SerializeField] private TouchController _touchController;
        [SerializeField] private Touchable _touchablePrefab;
        [SerializeField] private Transform _parentTransform;
        private Transform _touchTransform;
        private AnimateJob _calcJob;
        private NativeArray<Vector3> _nativePositions;
        private NativeArray<Matrix4x4> _nativeMatrixs;
        private NativeArray<bool> _nativeIsTouched;
        private RenderParams _renderParams;


        private void Start()
        {
            var count = GridTool.GetUnitsCount;
            _nativePositions = new NativeArray<Vector3>(count, Allocator.Persistent);
            _nativeMatrixs = new NativeArray<Matrix4x4>(count, Allocator.Persistent);
            _nativeIsTouched = new NativeArray<bool>(count, Allocator.Persistent);
            _calcJob = new AnimateJob()
            {
                Positions = _nativePositions,
                Matrixs = _nativeMatrixs,
                IsTouched = _nativeIsTouched,
                Scale = _scale,
                MaxDistance = _touchController.ColliderRadius,
                MaxTouchAngle = _touchController.MaxTouchAngle
            };

            _touchTransform = _touchController.transform;

            _renderParams = new RenderParams(_material);
            _renderParams.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            GridTool.CreateGrid((Vector3 pos, int i) =>
            {
                _nativePositions[i] = pos;
                if (TouchController.IsTouching)
                    Instantiate(_touchablePrefab, pos, Quaternion.identity, _parentTransform).Init(i);
            });

            Touchable.OnTouch += ChangeTouchState;

            UIController.Instance.CurrentHUD.SetCountVolue = count.ToString();
            UIController.Instance.CurrentHUD.SetSceneName = "GPU Instancing\n+jobs+burst";
        }


        private void Update()
        {
            _calcJob.Time = Time.time;
            _calcJob.TouchPos = _touchTransform.position;
            _calcJob.Schedule(_nativeMatrixs.Length, 64).Complete();
            Graphics.RenderMeshInstanced(_renderParams, _mesh, 0, _nativeMatrixs);
        }

        private void OnDestroy()
        {
            _nativePositions.Dispose();
            _nativeMatrixs.Dispose();
            _nativeIsTouched.Dispose();
            Touchable.OnTouch -= ChangeTouchState;
        }

        private void ChangeTouchState(int index, bool isTouched)
        {
            _nativeIsTouched[index] = isTouched;
        }

        [BurstCompile]
        private struct AnimateJob : IJobParallelFor
        {
            [ReadOnly] public NativeArray<Vector3> Positions;
            [ReadOnly] public Vector3 Scale;
            [ReadOnly] public NativeArray<bool> IsTouched;
            public NativeArray<Matrix4x4> Matrixs;
            [ReadOnly] public float Time;
            [ReadOnly] public Vector3 TouchPos;
            [ReadOnly] public float MaxDistance;
            [ReadOnly] public float MaxTouchAngle;

            public void Execute(int index)
            {
                if (!IsTouched[index])
                {
                    Quaternion rot = Positions[index].GetBurstNoiseRotation(Time);
                    Matrixs[index] = Matrix4x4.TRS(Positions[index], rot, Scale);
                }
                else
                {
                    Quaternion rot = Positions[index].GetBurstTouchRotation(TouchPos, MaxDistance, MaxTouchAngle);
                    Matrixs[index] = Matrix4x4.TRS(Positions[index], rot, Scale);
                }
            }
        }
    }
}
