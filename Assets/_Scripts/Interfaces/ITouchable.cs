using UnityEngine;

namespace GrassVsFps
{
    public interface ITouchable
    {
        public void EnableWind(bool isEnabled);
        public void SetRotation(Vector3 eulerRotation);
    }
}
