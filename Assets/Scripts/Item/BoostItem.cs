using System.Collections;
using UnityEngine;

public class BoostItem : Item
{
    public float addRate = 5f;    // 증가 속도

    public override void OnInteract()
    {
        SpeedUp();
    }

    private void SpeedUp()
    {
        StartCoroutine(SpeedUpCo());
    }

    private IEnumerator SpeedUpCo()
    {
        CharacterManager.Instance.Player.playerMovementController.moveSpeed += addRate;

        yield return new WaitForSeconds(5f);

        CharacterManager.Instance.Player.playerMovementController.moveSpeed -= addRate;
    }
}
