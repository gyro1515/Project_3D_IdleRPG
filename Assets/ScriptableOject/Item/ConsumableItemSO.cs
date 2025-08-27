using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ConsumableType
{
    HP,
    MP,
    Damage,
    AttackRange
}
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
    public float duration;   // 지속 시간 (0이면 즉시 효과)
}
[CreateAssetMenu(fileName = "New ConsumableItem", menuName = "Item/ConsumableItem")]
public class ConsumableItemSO : ItemSO
{
    
}
