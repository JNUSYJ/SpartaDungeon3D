using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovementController playerMovementController;
    public PlayerConditionController playerConditionController;

    public ItemSO itemData; // 아이템 데이터

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        playerMovementController = GetComponent<PlayerMovementController>();
        playerConditionController = GetComponent<PlayerConditionController>();
    }
}
