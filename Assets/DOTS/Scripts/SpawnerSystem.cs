using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class SpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.WithStructuralChanges()
            .ForEach((Entity entity, in Spawner spawner, in LocalToWorld ltw) =>
            {
                for (int x = 0; x < spawner.countX; ++x)
                    for (int z = 0; z < spawner.countZ; ++z)
                    {
                        var posX = spawner.spacingX * (x - (spawner.countX - 1) / 2);
                        var posZ = spawner.spacingY * (z - (spawner.countZ - 1) / 2);
                        var instance = EntityManager.Instantiate(spawner.prefab);
                        SetComponent(instance, new Translation
                        {
                            Value = ltw.Position + new float3(posX, 0, posZ)
                        });
                    }

                EntityManager.DestroyEntity(entity);
            }).Run();
    }
}
