using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace GrassVsFps
{
    public struct TouchComponent : IComponentData
    {
        public NativeList<TriggerEvent> TriggerEvents;
    }
}
