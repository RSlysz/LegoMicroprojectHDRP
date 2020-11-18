using Unity.Entities;

namespace LegoDOTS
{
    [GenerateAuthoringComponent]
    public struct Rotate : IComponentData
    {
        public float acceleration;
        public float speed;
    }
}
