using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new EquippableItem", menuName = "Item/EquippableItem")]

public class EquippableItemSO : ItemSO
{
    [Header("장비 정보 세팅")]
    [SerializeField] GameObject equipPrefab;
    public GameObject EquipPrefab { get { return equipPrefab; } }
}
