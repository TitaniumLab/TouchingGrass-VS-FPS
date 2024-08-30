using UnityEngine;

namespace GrassVsFps
{
    public class AnimatorData : MonoBehaviour
    {
        [SerializeField] private float _maxRotation = 40;
        [SerializeField] private Vector3 _windDirection = new Vector3(-1, 0, 1);
        [SerializeField] private float _noiseScale = 0.1f;
        [SerializeField] private float _noiseSpeed = 1;

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
