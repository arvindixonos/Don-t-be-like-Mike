using System;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera m_Camera;
    public float m_WalkSpeed;
    private MouseLook m_MouseLook;
    private Vector2 m_Input;
    private Vector3 m_MoveDir = Vector3.zero;
    private CharacterController m_CharacterController;

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_MouseLook = new MouseLook();
        m_MouseLook.Init(transform, m_Camera.transform);
    }

    private void Update()
    {
        RotateView();
        m_MoveDir.y = 0f;
    }

    private void FixedUpdate()
    {
        GetInput();

        Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

        m_MoveDir.x = desiredMove.x * m_WalkSpeed;
        m_MoveDir.z = desiredMove.z * m_WalkSpeed;

        m_MoveDir += Physics.gravity * Time.fixedDeltaTime * 2;
       m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        m_MouseLook.UpdateCursorLock();
    }

    private void GetInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Input = new Vector2(horizontal, vertical);

        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }
    }

    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }
}
