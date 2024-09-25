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

        public static Quaternion GetTouchRotation(this Vector3 pos, Vector3 touchPos, float maxDistance, float maxAngle)
        {
            var direction = pos - touchPos;
            float distance = direction.magnitude;
            float relativeDistance = 1 - Mathf.Clamp(distance, 0, maxDistance) / maxDistance;
            Vector3 right = -Vector3.Cross(direction, Vector3.up);
            return Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.AngleAxis(maxAngle, right), relativeDistance);
        }
    }
}
