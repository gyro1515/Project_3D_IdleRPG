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
    [Header("소비 아이템 정보")]
    [SerializeField] ItemDataConsumable[] consumables;
    [SerializeField] int maxStackAmount; // 한 칸에 최대 몇개까지 보유할 수 있는가
    public int MaxStackAmount { get { return maxStackAmount; } }
    public ItemDataConsumable[] Consumables { get { return consumables; } }

}
