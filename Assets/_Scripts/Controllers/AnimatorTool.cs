using UnityEngine;

namespace GrassVsFps
{
    public static class AnimatorTool
    {
        private static AnimatorData _data;

        public static void Init(AnimatorData data)
        {
            _data = data;
        }

        public static Quaternion GetNoiseRotation(this Vector3 pos, float time)
        {
            //noise
            float totalXOffset = (pos.x * _data.NoiseScale) + (time * _data.NoiseSpeed * _data.WindDirection.normalized.x);
            float totalZOffset = (pos.z * _data.NoiseScale) + (time * _data.NoiseSpeed * _data.WindDirection.normalized.z);
            float noise = Mathf.PerlinNoise(totalXOffset, totalZOffset);
            //rotation
            Vector3 right = -Vector3.Cross(_data.WindDirection, Vector3.up);
            return Quaternion.Slerp(Quaternion.AngleAxis(-_data.MaxRotation, right), Quaternion.AngleAxis(_data.MaxRotation, right), noise);
        }
    }
}