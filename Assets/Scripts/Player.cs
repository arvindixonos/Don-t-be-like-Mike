using System.Collections;
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

        set { active = value; }
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

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Destination"))
        {   
            GameManager.Instance.LevelComplete();
        }
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
        float randomNum = Rand.Range(2, 7);
        float steadyDelay = Rand.Range(0f, 0.3f);
        float speed = 1.5f;

        drunkCamSequence = DOTween.Sequence();
        drunkCamSequence.OnComplete(StartHeadBob);
        drunkCamSequence.Append(drunkCam.DOLocalRotate(new Vector3(0f, 0f, randomNum), speed).SetEase(Ease.OutQuad).SetSpeedBased())
                        .Append(drunkCam.DOLocalRotate(new Vector3(0f, 0f, -randomNum), speed).SetEase(Ease.InOutQuad).SetSpeedBased().SetDelay(steadyDelay))
                        .Append(drunkCam.DOLocalRotate(Vector3.zero, speed / 2).SetEase(Ease.InQuad).SetSpeedBased().SetDelay(steadyDelay));
    }

    private void Update()
    {
        if (GameManager.Instance.GameOver)
            return;

        RotateView();
        m_MoveDir.y = 0f;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameOver)
            return;

        GetInput();

        Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

        float lerpSpeed = 0.05f;
        float desiredMoveMag = desiredMove.sqrMagnitude;
        
        if(desiredMoveMag < 0.3f)
            lerpSpeed = 1f;

        moveSpeedInc = Mathf.Lerp(moveSpeedInc, desiredMoveMag, lerpSpeed);
        moveSpeedInc = Mathf.Clamp(moveSpeedInc, 0f, 1f);

        if (desiredMoveMag > 0.1f)
            SoundManager.Instance.PlaySound(eSoundType.SOUND_PLAYER_WALK, eSoundSourceType.SOUND_SOURCE_PLAYER,
                                                    moveSpeedInc * 0.1f);

        m_MoveDir.x = desiredMove.x * (moveSpeed + moveSpeedInc);
        m_MoveDir.z = desiredMove.z * (moveSpeed + moveSpeedInc);

        m_MoveDir += Physics.gravity * Time.fixedDeltaTime;
        m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        m_MouseLook.UpdateCursorLock();

        // if(Input.GetKeyUp(KeyCode.A))
        if(moveSpeedInc > 0.8f)
        {
            // GameManager.Instance.CaughtPlayer();
        }
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

    public void LookAtPosition(Vector3 position)
    {
        position.y = transform.position.y;

        transform.DOLookAt(position, 0.5f);
    }
}
