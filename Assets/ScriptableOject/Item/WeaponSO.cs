using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Item/Weapon")]

public class WeaponSO : ItemSO
{
    [Header("장비 정보 세팅")]
    [SerializeField] GameObject equipPrefab;
    public GameObject EquipPrefab { get { return equipPrefab; } }
}
