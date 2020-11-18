using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[VolumeComponentMenu("Gameplay/Object Gravity")]
public class ObjectGravityVolumeComponent : VolumeComponent
{
    public BoolParameter gravityActive = new BoolParameter(true);
    public Vector3Parameter gravity = new Vector3Parameter(Vector3.down * 9.81f);
}
