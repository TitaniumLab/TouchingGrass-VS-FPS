using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace GrassVsFps
{
    public class Level5 : MonoBehaviour
    {
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;
        [SerializeField] internal Vector3 _scale = new Vector3(100, 100, 100);
        private AnimateJob _calcJob;
        private NativeArray<Vector3> _nativePositions;
        private NativeArray<Matrix4x4> _nativeMatrixs;
        private RenderParams _renderParams;


        private void Start()
        {

            var count = GridTool.GetUnitsCount;
            _nativePositions = new NativeArray<Vector3>(count, Allocator.Persistent);
            _nativeMatrixs = new NativeArray<Matrix4x4>(count, Allocator.Persistent);
            _calcJob = new AnimateJob()
            {
                Positions = _nativePositions,
                Matrixs = _nativeMatrixs,
                Scale = _scale
            };


            _renderParams = new RenderParams(_material);
            _renderParams.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            GridTool.CreateGrid((Vector3 pos, int i) => { _nativePositions[i] = pos; });

            UIController.Instance.CurrentHUD.SetCountVolue = count.ToString();
            UIController.Instance.CurrentHUD.TouchToggle.interactable = false;
            UIController.Instance.CurrentHUD.SetTouchTypeText = "Game objects doesn't created";
            UIController.Instance.CurrentHUD.SetSceneName = "GPU Instancing\n+jobs+burst";
        }


        private void Update()
        {
            _calcJob.Time = Time.time;
            _calcJob.Schedule(_nativeMatrixs.Length, 64).Complete();
            Graphics.RenderMeshInstanced(_renderParams, _mesh, 0, _nativeMatrixs);
        }

        private void OnDestroy()
        {
            _nativePositions.Dispose();
            _nativeMatrixs.Dispose();
        }

        [BurstCompile]
        private struct AnimateJob : IJobParallelFor
        {
            [ReadOnly] public NativeArray<Vector3> Positions;
            [ReadOnly] public Vector3 Scale;
            public NativeArray<Matrix4x4> Matrixs;
            public float Time;

            public void Execute(int index)
            {
                Quaternion rot = Positions[index].GetBurstNoiseRotation(Time);
                Matrixs[index] = Matrix4x4.TRS(Positions[index], rot, Scale);
            }
        }
    }
}
