using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace LegoDOTS
{
    public class ElevateSystem : SystemBase
    {

        Transform playerPosition;

        protected override void OnUpdate()
        {
            if (playerPosition == null || playerPosition.Equals(null))
                playerPosition = GameObject.FindGameObjectWithTag("Player")?.transform;

            if (playerPosition == null)
                return;

            Vector2 position = new Vector2(playerPosition.position.x, playerPosition.position.z);

            Entities.ForEach((ref Translation entityPosition, in Elevate elevate) =>
            {
                var dist = math.distancesq(new float2(entityPosition.Value.x, entityPosition.Value.z), position);
                var coef = dist > 31.416 ? 0f : math.min(1f, 0.525f + .525f * math.cos(.1f * dist)); 
                entityPosition.Value.y = elevate.minY + (elevate.maxY - elevate.minY) * coef;
            }).ScheduleParallel();
        }
    }
}
