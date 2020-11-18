using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[VolumeComponentMenu("Gameplay/Gravity")]
public class GravityVolumeComponent : VolumeComponent
{
    public Vector3Parameter gravity = new Vector3Parameter( Vector3.down * 9.81f);
}
