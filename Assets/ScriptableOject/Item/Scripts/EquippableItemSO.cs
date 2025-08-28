using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EEquipmentType
{
    None,
    Weapon,
    Armor,
    Shoes,
}
[CreateAssetMenu(fileName = "new EquippableItem", menuName = "Item/EquippableItem")]

public class EquippableItemSO : ItemSO
{
    [Header("장비 정보 세팅")]
    [SerializeField] GameObject equipPrefab;
    [SerializeField] int damage;
    [SerializeField] EEquipmentType equipmentType = EEquipmentType.None;
    [SerializeField] float attackRange = 1.5f;
    public GameObject EquipPrefab { get { return equipPrefab; } }
    public int Damage { get { return damage; } }
    public EEquipmentType EquipmentType { get { return equipmentType; } }
    public float AttackRange { get { return attackRange; } }
}
