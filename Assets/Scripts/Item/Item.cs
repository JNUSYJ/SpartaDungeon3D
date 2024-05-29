using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public ItemSO itemData;     // 아이템 데이터

    // 아이템 정보 스트링 반환
    public string GetInteractInfo()
    {
        string infoStr = $"{itemData.name}\n{itemData.itemDescription}";

        return infoStr;
    }

    // 아이템 상호작용
    public virtual void OnInteract() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnInteract();
        }
    }
}
