using TMPro;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("Check")]
    public float checkRate = 0.1f;  // 체크 주기
    private float lastCheckTime;    // 마지막 체크 시간
    public float maxCheckDistance;  // 최대 인식 거리
    public LayerMask interactableLayerMask; // 상호작용 오브젝트 레이어마스크
    private Camera _camera;         // 카메라

    [Header("Item")]
    private GameObject curInteractGameObject;   // 현재 보고있는 게임 오브젝트
    private IInteractable curInteractable;      // 현재 상호작용 중인 상호작용 오브젝트

    [Header("UI")]
    public TextMeshProUGUI promptText;  // 상호작용 오브젝트 정보를 출력할 텍스트

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            // 주기 체크
            lastCheckTime = Time.time;

            // 화면 중앙에서 레이 발사
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            // 보고 있는 곳에 상호작용 가능한 오브젝트가 있을 경우
            if (Physics.Raycast(ray, out hit, maxCheckDistance, interactableLayerMask))
            {
                // 오브젝트 정보 화면 출력
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPrompt();
                }
            }
            // 보고 있는 곳에 상호작용 오브젝트가 없을 경우
            else
            {   
                // 초기화
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    // 오브젝트 정보 화면 출력
    private void SetPrompt()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractInfo();
    }
}
