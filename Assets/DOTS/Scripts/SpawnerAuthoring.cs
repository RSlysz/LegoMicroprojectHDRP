using Unity.Entities;

[GenerateAuthoringComponent]
public struct Spawner : IComponentData
{
    public Entity prefab;
    public int countX;
    public int countZ;
    public int spacingX;
    public int spacingY;
}
