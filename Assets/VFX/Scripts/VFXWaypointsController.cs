using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect)), ExecuteAlways]
public class VFXWaypointsController : MonoBehaviour
{
    VisualEffect vfx;
    Texture2D waypointsTexture;
    int waypointsCount_prev = -1;
    List<Color> pixels = new List<Color>();

    Bounds bounds = new Bounds();

    public bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        var waypointsCount = transform.childCount;

        // Check if the number of waypoints have changed, and update the data if needed
        if (waypointsCount != waypointsCount_prev)
        {
            GenerateWaypointTexture(waypointsCount);
            waypointsCount_prev = waypointsCount;
            pixels.Capacity = waypointsCount;
        }

        float totalDistance = 0f;
        bounds.size = Vector3.zero;

        /* Iterate on the waypoints
         * Save the position in pixel color RGB
         * Save the distance to the next waypoint in pixel alpha
         * Last pixel alpha holds the total distance
         * Recalculate the bounds for the VFX
         */
        for (int i=0; i<waypointsCount; i++)
        {
            var t = transform.GetChild(i);

            if (i == 0) bounds.center = t.position;
            bounds.Encapsulate(t.position);

            Vector4 pixel = t.position;

            if (i < (waypointsCount - 1))
            {
                var diff = transform.GetChild(i + 1).position - t.position;
                var dist = Mathf.Max(Mathf.Abs(diff.x), Mathf.Abs(diff.z));
                totalDistance += dist;
                pixel.w = dist;
            }
            else
                pixel.w = totalDistance;

            if ( pixels.Count <= i)
                pixels.Add(pixel);
            else
                pixels[i] = pixel;
        }

        // Expand bounds for safety
        bounds.Expand(3f);

        // Update data
        vfx.SetVector3("Bounds Center", bounds.center);
        vfx.SetVector3("Bounds Size", bounds.size);

        waypointsTexture.SetPixels(pixels.ToArray());

        waypointsTexture.Apply();

        //Uggly debugging thingy
        if (debug)
        {
            debug = false;

            var newPixels = waypointsTexture.GetPixels();
            var debugString = "";
            foreach(var pixel in newPixels)
            {
                debugString += $"r: {pixel.r} , g: {pixel.g} , b: {pixel.b} , a: {pixel.a}\r\n";
            }
            Debug.Log(debugString);
        }
    }

    // Regenerate texture if the count is changed.
    void GenerateWaypointTexture( int count )
    {
        if (waypointsTexture != null) Destroy(waypointsTexture);

        waypointsTexture = new Texture2D(count, 1, TextureFormat.RGBAFloat, false);
        waypointsTexture.name = "Waypoint Texture";

        vfx.SetTexture("Waypoint Texture", waypointsTexture);
        vfx.SetInt("Waypoints Count", count);
    }
}
