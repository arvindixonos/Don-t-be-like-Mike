using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Player : MonoBehaviour
{

    public static Player Instance = null;

    public string playerName = "LiveWire";

    public bool isActive = false;

	public	float	moveSpeed = 2f;

	public	float	moveSpeedInc = 0.1f;

	public	Camera		playerCamera = null;

    private CharacterController m_CharacterController;

    private Vector2 m_Input;

    private Vector3 m_MoveDir = Vector3.zero;

    public MouseLook m_MouseLook;

	public	void	CamLookAt(Transform target)
	{
		
	}

	public	void	OnHit(eHomeObject	homeObject)
	{

	}

	void OnDestinationReached()
	{

	}

    void Awake()
    {
        if (Instance == null)
            Instance = this;
			
        m_CharacterController = GetComponent<CharacterController>();
        // m_MouseLook = new MouseLook();
        m_MouseLook.Init(transform, playerCamera.transform);
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

        m_MoveDir.x = desiredMove.x * moveSpeed;
        m_MoveDir.z = desiredMove.z * moveSpeed;

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
        m_MouseLook.LookRotation(transform, playerCamera.transform);
    }

	void OnDestroy()
	{
		Instance = null;
	}
}
