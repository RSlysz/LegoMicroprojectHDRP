using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GravityReader : MonoBehaviour
{
    public LayerMask layerMask = -1;

    // Update is called once per frame
    void Update()
    {
        VolumeManager.instance.Update(transform, layerMask);

        Physics.gravity = VolumeManager.instance.stack.GetComponent<GravityVolumeComponent>().gravity.value;
    }
}
