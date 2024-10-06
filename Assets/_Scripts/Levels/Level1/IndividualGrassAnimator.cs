using UnityEngine;

namespace GrassVsFps
{
    public class IndividualGrassAnimator : MonoBehaviour, ITouchable
    {
        private Vector3 _pos; // It grants 5% more fps then "transform.position" :D
        private bool _isTouching;

        private void Update()
        {
            if (!_isTouching)
            {
                var rot = _pos.GetNoiseRotation(Time.time);
                transform.rotation = rot;
            }
        }

        public void Init(Vector3 pos)
        {
            _pos = pos;
        }

        public void StartTouching()
        {
            _isTouching = true;
        }


        public void Touching(Vector3 touchPos, float maxDistance, float maxAngle)
        {
            var rot = _pos.GetTouchRotation(touchPos, maxDistance, maxAngle);
            transform.rotation = rot;
        }


        public void StopTouching()
        {
            _isTouching = false;
        }
    }
}