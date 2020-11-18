using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class ObjectGravityReader : MonoBehaviour
{
    public LayerMask layerMask = -1;

    Rigidbody rbd;

    private void Start()
    {
        rbd = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        VolumeManager.instance.Update(transform, layerMask);

        var gravityComponent = VolumeManager.instance.stack.GetComponent<ObjectGravityVolumeComponent>();

        var gravityOverride = gravityComponent.gravityActive.value;

        if (gravityOverride)
        {
            rbd.useGravity = false;
            rbd.AddForce(gravityComponent.gravity.value, ForceMode.Acceleration);
        }
        else
            rbd.useGravity = true;
    }
}
