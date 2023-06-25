
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Vector3 targetPosition { get; private set; }
    private Vector2 Input { get; set; }

    public float AimDistance = 15f;

    private Camera cached_MainCam;
    private Camera MainCam => cached_MainCam ??= Camera.main;
    private void SetInput(Vector2 value) => Input = value;
    public void Start()
    {
        SpaceshipInputHandler.OnAimChanged += SetInput;
    }

    public void OnDestroy()
    {
        SpaceshipInputHandler.OnAimChanged -= SetInput;
    }

    void FixedUpdate()
    {
        var ray = MainCam.ScreenPointToRay(Input);
        var Plane = new Plane(Vector3.forward, Vector3.forward * AimDistance);
        if (Plane.Raycast(ray, out var distance))
            targetPosition = ray.GetPoint(distance);
    }
}
