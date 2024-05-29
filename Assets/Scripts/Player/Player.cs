using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovementController playerMovementController;
    public PlayerConditionController playerConditionController;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        playerMovementController = GetComponent<PlayerMovementController>();
        playerConditionController = GetComponent<PlayerConditionController>();
    }
}
