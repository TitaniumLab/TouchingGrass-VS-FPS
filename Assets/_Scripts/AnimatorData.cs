using Unity.Mathematics;
using UnityEngine;

namespace GrassVsFps
{
    public class AnimatorData : MonoBehaviour
    {
        [SerializeField] private float _maxRotation = 40;
        [SerializeField] private Vector3 _windDirection = new Vector3(-1, 0, 1);
        [SerializeField] private float _noiseScale = 0.1f;
        [SerializeField] private float _noiseSpeed = 1;

        // Burst supports only readonly/const fields
        public readonly static Vector3 BURST_WIND_DIRECTION = new Vector3(-1, 0, 1);
        public const float BURST_NOISE_SCALE = 0.1f;
        public const float BURST_NOISE_SPEED = 1;
        public const float BURST_MAX_ROTATION = 40;


        public float MaxRotation { get { return _maxRotation; } }
        public Vector3 WindDirection { get { return _windDirection; } }
        public float NoiseScale { get { return _noiseScale; } }
        public float NoiseSpeed { get { return _noiseSpeed; } }

        private void Start()
        {
            AnimatorTool.Init(this);
        }

    }
}
