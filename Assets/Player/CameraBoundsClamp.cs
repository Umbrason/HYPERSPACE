
using UnityEngine;

[DefaultExecutionOrder(100)]
public class CameraBoundsClamp : MonoBehaviour
{
    public float border = .05f;
    private Vector3 MaxCorner => ToWorldPoint(MainCam.ViewportPointToRay(Vector2.one * (1 - border)));
    private Vector3 MinCorner => ToWorldPoint(MainCam.ViewportPointToRay(Vector2.one * border));

    private Camera cached_MainCam;
    private Camera MainCam => cached_MainCam ??= Camera.main;

    void FixedUpdate()
    {
        transform.position = Vector3.Max(Vector3.Min(transform.position, MaxCorner), MinCorner);
    }

    private Vector3 ToWorldPoint(Ray ray)
    {
        Plane p = new(cached_MainCam.transform.forward, transform.position);
        if (p.Raycast(ray, out float distance))
            return ray.GetPoint(distance);
        return default;
    }
}
