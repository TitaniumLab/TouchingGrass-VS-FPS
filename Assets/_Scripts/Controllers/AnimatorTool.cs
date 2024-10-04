using Unity.Mathematics;
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

        public static quaternion GetBurstNoiseRotation(this Vector3 pos, float time)
        {
            //noise
            float totalXOffset = (pos.x * AnimatorData.BURST_NOISE_SCALE) + (time * AnimatorData.BURST_NOISE_SPEED * AnimatorData.BURST_WIND_DIRECTION.normalized.x);
            float totalZOffset = (pos.z * AnimatorData.BURST_NOISE_SCALE) + (time * AnimatorData.BURST_NOISE_SPEED * AnimatorData.BURST_WIND_DIRECTION.normalized.z);
            float myNoise = noise.cnoise(new float2(totalXOffset, totalZOffset));
            //rotation
            float3 right = -math.cross(AnimatorData.BURST_WIND_DIRECTION, new float3(0, 1, 0));
            quaternion minAngle = quaternion.AxisAngle(right, 0); // IDK Why doesn't it work like in Mathf ¯\_(ツ)_/¯
            quaternion maxAngle = quaternion.AxisAngle(right, (AnimatorData.BURST_MAX_ROTATION / 180) * math.PI);
            return math.normalize(math.slerp(minAngle, maxAngle, myNoise));
        }
    }
}
