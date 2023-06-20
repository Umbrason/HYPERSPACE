
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] public Vector3 rotationSpeed;
    void FixedUpdate()
    {
        transform.localRotation *= Quaternion.Euler(rotationSpeed * Time.fixedDeltaTime);
    }
}
