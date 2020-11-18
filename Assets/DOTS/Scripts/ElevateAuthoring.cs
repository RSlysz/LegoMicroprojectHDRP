using Unity.Entities;

namespace LegoDOTS
{
    [GenerateAuthoringComponent]
    public struct Elevate : IComponentData
    {
        public float minY;
        public float maxY; // statue height
    }
}
