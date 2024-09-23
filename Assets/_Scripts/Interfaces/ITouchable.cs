using UnityEngine;

namespace GrassVsFps
{
    public interface ITouchable
    {
        /// <summary>
        /// Action when touch starts
        /// </summary>
        public void StartTouching();
        /// <summary>
        /// Action during Touch
        /// </summary>
        public void Touching(Vector3 touchPos, float maxDistance);
        /// <summary>
        /// Action when touch stops
        /// </summary>
        public void StopTouching();
    }
}
