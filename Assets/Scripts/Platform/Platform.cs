using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformSO platformSO;

    private Vector3 from;                   // 현재 위치
    private Vector3 destination;            // 목적지
    private Vector3 moveDirection;          // 이동 방향
    private float moveSpeed;                // 이동 속도
    private int destinationIndex;           // 목적지 리스트 인덱스
    private float distanceThreshold = 0.1f; // 남은 거리 오차 보정값

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        destinationIndex = 0;
        from = transform.position;
        destination = platformSO.destination[0];
        moveSpeed = platformSO.moveSpeed[0];
    }

    private void FixedUpdate()
    {
        moveDirection = SetDirection(); // 방향 설정
        Move(moveDirection);            // 이동
        ArrivedCheck();                 // 목적지 도착 체크
    }

    // 목적지 도착 체크
    private void ArrivedCheck()
    {
        from = transform.position;
        if (Vector3.Distance(from, destination) <= distanceThreshold)
        {
            if (++destinationIndex >= platformSO.destination.Count)
            {
                destinationIndex = 0;
            }
            destination = platformSO.destination[destinationIndex];
            moveSpeed = platformSO.moveSpeed[destinationIndex];
        }
    }

    // 방향 설정
    private Vector3 SetDirection()
    {
        Vector3 moveDirection;

        moveDirection = destination - from;
        moveDirection = moveDirection.normalized;
        moveDirection *= moveSpeed;

        return moveDirection;
    }

    // 이동
    private void Move(Vector3 moveDirection)
    {
        _rigidbody.velocity = moveDirection;
    }
}
