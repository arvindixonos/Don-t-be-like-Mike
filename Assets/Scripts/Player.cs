﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Rand = UnityEngine.Random;
using Enums;

public class Player : MonoBehaviour
{

    public static Player Instance = null;

    private bool active = false;

    public bool Active
    {
        get { return active; }

        set { active = value;}
    }

    public float moveSpeed = 2f;

    public float moveSpeedInc = 0.1f;

    public Camera playerCamera = null;

    public Transform drunkCam;

    private CharacterController m_CharacterController;

    private Vector2 m_Input;

    private Vector3 m_MoveDir = Vector3.zero;

    public MouseLook m_MouseLook;

	private Sequence drunkCamSequence;

    public void CamLookAt(Transform target)
    {

    }

    public void OnHit(eHomeObject homeObject)
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
        m_MouseLook.Init(transform, playerCamera.transform);

        StartHeadBob();
    }

    private void StartHeadBob()
    {
		float randomNum = Rand.Range(2, 5);
		float steadyDelay = Rand.Range(0.3f, 0.5f);

		drunkCamSequence = DOTween.Sequence();
		drunkCamSequence.OnComplete(StartHeadBob);
		drunkCamSequence.Append(drunkCam.DOLocalRotate(new Vector3(0f, 0f, randomNum), 2f).SetEase(Ease.OutQuad).SetSpeedBased())
						.Append(drunkCam.DOLocalRotate(new Vector3(0f, 0f, -randomNum), 2f).SetEase(Ease.InOutQuad).SetSpeedBased().SetDelay(steadyDelay))
						.Append(drunkCam.DOLocalRotate(Vector3.zero, 1f).SetEase(Ease.InQuad).SetSpeedBased().SetDelay(steadyDelay));
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
