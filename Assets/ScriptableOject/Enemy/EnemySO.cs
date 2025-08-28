using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Characters/Enemy")]
public class EnemySO : CharacterSO
{
    [Header("적 무기 세팅")]
    [SerializeField] EquippableItemSO weaponDataSO;
    [field: Header("골드 드랍 세팅")]
    [field: SerializeField] public int Gold { get; private set; }
    [field: Header("경험치 세팅")]
    [field: SerializeField] public int Exp { get; private set; }
    public EquippableItemSO WeaponDataSO { get { return weaponDataSO; } }
}
