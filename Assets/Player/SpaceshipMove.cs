
using UnityEngine;

public class SpaceshipMove : MonoBehaviour
{
    public float Speed = 15f;
    public Vector2 MoveInput { get; set; }

    private void SetMoveInput(Vector2 input) => MoveInput = input;
    void OnEnable()
    {
        SpaceshipInputHandler.OnMoveChanged += SetMoveInput;
    }
    void OnDisable()
    {
        SpaceshipInputHandler.OnMoveChanged -= SetMoveInput;
    }


    private float m_roll;
    private float m_pitch;
    void FixedUpdate()
    {
        transform.position += (Vector3)MoveInput * Speed * Time.fixedDeltaTime;

        m_roll = Mathf.Lerp(m_roll, -MoveInput.x * 25f, .2f);
        m_pitch = Mathf.Lerp(m_pitch, -MoveInput.y * 15f, .2f);
        transform.rotation = Quaternion.Euler(m_pitch, 0, m_roll);
    }
}
