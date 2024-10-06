using System;
using UnityEngine;

namespace GrassVsFps
{
    public class Touchable : MonoBehaviour, ITouchable
    {
        private int _index;
        public static event Action<int, bool> OnTouch;


        public void Init(int index)
        {
            _index = index;
        }


        public void StartTouching()
        {
            OnTouch?.Invoke(_index, true);
        }


        public void StopTouching()
        {
            OnTouch?.Invoke(_index, false);
        }


        public void Touching(Vector3 touchPos, float maxDistance, float maxAngle) { }
    }
}
