using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Renderer))]
[ExecuteAlways]
public class ObjectColorVolumeReader : MonoBehaviour
{
    Renderer renderer;

    public string colorPropertyName = "_BaseColor";
    public LayerMask layerMask = -1;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        VolumeManager.instance.Update(transform, layerMask);
        renderer.material.SetColor(colorPropertyName,
            VolumeManager.instance.stack.GetComponent<ObjectColorVolumeComponent>().color.value);
    }
}
