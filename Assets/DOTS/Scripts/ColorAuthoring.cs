using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

namespace LegoDOTS
{
    [GenerateAuthoringComponent]
    [MaterialProperty("_BaseColor", MaterialPropertyFormat.Float4)]
    public struct ColorDefinition : IComponentData
    {
        public Color color;
    }
}
