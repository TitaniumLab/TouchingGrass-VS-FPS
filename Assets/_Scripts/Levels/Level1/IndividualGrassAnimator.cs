using UnityEngine;

namespace GrassVsFps
{
    public class IndividualGrassAnimator : MonoBehaviour
    {
        private Vector3 _pos; // It grants 5% more fps then "transform.position" :D

        private void Update()
        {
            var rot = _pos.GetNoiseRotation(Time.time);
            transform.rotation = rot;
        }

        public void Init(Vector3 pos)
        {
            _pos = pos;
        }
    }
}
