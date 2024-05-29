using UnityEngine;

public enum ItemType
{
    Etc,
    Equipable,
    Consumable,
    Resource
}

[CreateAssetMenu(fileName = "SO", menuName ="New Item")]
public class ItemSO : ScriptableObject
{
    [Header("Info")]
    public string itemName;         // 아이템 이름
    public string itemDescription;  // 아이템 설명
    public ItemType itemType;       // 아이템 타입
}
