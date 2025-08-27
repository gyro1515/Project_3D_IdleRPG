using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    Equipable,
    Consumable
}
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]

public class ItemSO : ScriptableObject
{
    [Header("아이템 기본 정보")]
    [SerializeField] protected string displayName;
    [SerializeField] protected string description;
    [SerializeField] protected EItemType itemType;
    [SerializeField] protected Sprite icon;
    public string DisplayName { get { return displayName; } }
    public string Description { get { return description; } }
    public EItemType ItemType { get { return itemType; } }
    public Sprite Icon { get { return icon; } }
}
