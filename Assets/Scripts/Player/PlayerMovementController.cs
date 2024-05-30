using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerConditionController playerConditionController;

    [Header("Move")]
    public float moveSpeed;     // 속도
    public float jumpPower;     // 점프력
    private Vector2 inputVector;// WASD 입력 벡터

    [Header("Look")]
    public Transform cameraContainer;   // 카메라
    public float lookSensivity;         // 회전 감도
    private float camCurXRot;           // 카메라 X축 회전값
    private Vector2 mouseDelta;         // 마우스 입력 벡터
    private float sinTheta;             // Sin값
    private float cosTheta;             // Cos값
    public float radius;                // 카메라 회전 반지름
    public float playerHeight;          // 플레이어 키

    [Header("Jump")]
    public LayerMask groundLayerMask;   // 지면 레이어마스크

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerConditionController = GetComponent<PlayerConditionController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Look();
    }

    // 플레이어 움직임
    private void Move()
    {
        // 방향, 속도 설정
        Vector3 direction = transform.forward * inputVector.y + transform.right * inputVector.x;
        direction *= moveSpeed;
        // 상하 움직임(점프) 반영
        direction.y = _rigidbody.velocity.y;
        // 움직임 적용
        _rigidbody.velocity = direction;
    }

    // 플레이어 회전
    private void Look()
    {
        // 카메라 상하 회전
        camCurXRot += mouseDelta.y * lookSensivity;
        camCurXRot = Mathf.Clamp(camCurXRot, -85, 85);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        // 카메라 상하 움직임
        sinTheta = -Mathf.Sin(camCurXRot * Mathf.Deg2Rad);
        cosTheta = -Mathf.Cos(camCurXRot * Mathf.Deg2Rad);
        float y = sinTheta * radius + playerHeight;
        float z = cosTheta * radius;
        cameraContainer.transform.localPosition = new Vector3(0, y, z);
        // 플레이어 좌우 회전
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensivity);
    }

    // 플레이어 점프
    private void Jump()
    {
        float staminaUsedForJump = 10f; // 스태미나 감소량

        // 점프
        _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        // 스태미나 감소
        playerConditionController.UseStamina(staminaUsedForJump);
    }

    // 지면 검사
    private bool IsGrounded()
    {
        // 레이캐스트로 지면 검사
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1.5f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    // 아래 이벤트들은 모두 PlayerInput 컴포넌트에서 호출

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            inputVector = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            inputVector = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            Jump();
        }
    }
}
