using UnityEngine;

public class ConditionUI : MonoBehaviour
{
    public Condition hp;        // HP
    public Condition stamina;   // 스태미나
    public Condition hunger;    // 배고픔

    private void Start()
    {
        CharacterManager.Instance.Player.playerConditionController.conditionUI = this;
    }
}
