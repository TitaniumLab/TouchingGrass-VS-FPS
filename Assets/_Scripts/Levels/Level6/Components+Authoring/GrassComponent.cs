using Unity.Entities;

namespace GrassVsFps
{
    public struct GrassComponent : IComponentData
    {
        public bool IsTouched;
    }
}
