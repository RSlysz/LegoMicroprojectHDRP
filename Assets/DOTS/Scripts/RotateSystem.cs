using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace LegoDOTS
{
    // v1: all entities
    //public class RotateSystem : SystemBase
    //{
    //    protected override void OnUpdate()
    //    {
    //        var delta = quaternion.RotateY(math.radians(Time.DeltaTime * 360));

    //        Entities.ForEach((ref Rotation rotation) =>
    //        {
    //            rotation.Value = math.normalize(math.mul(rotation.Value, delta));
    //        }).ScheduleParallel();
    //    }
    //}

    // v2: Only entities with Rotate authoring
    public class RotateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var delta = quaternion.RotateY(math.radians(Time.DeltaTime * 360));

            Entities.WithAll<Rotate>().ForEach((ref Rotation rotation) =>
            {
                rotation.Value = math.normalize(math.mul(rotation.Value, delta));
            }).ScheduleParallel();
        }
    }
}
