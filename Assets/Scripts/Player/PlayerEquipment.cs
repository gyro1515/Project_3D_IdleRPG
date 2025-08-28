using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [Header("장비 장착 세팅")]
    [SerializeField] Transform weaponSocket;
    [SerializeField] Transform ArmorSocket; // 시간이 될까...? 일단 무기만
    [SerializeField] Transform ShoesSocket;
    [SerializeField] EquippableItemSO initialWeaponSO; // 초기 무기 세팅
    PlayerInventory playerInventory;
    Player player;
    Dictionary<EquippableItemSO, GameObject> equipments = new Dictionary<EquippableItemSO, GameObject>(); // 장비 재사용하기
    // 현재 장착된 장비들 (장비 타입별로 하나씩만 장착 가능)
    public Dictionary<EEquipmentType, EquippableItemSO> CurEquipments { get; private set; } = new Dictionary<EEquipmentType, EquippableItemSO>();

    private void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();
        player = GetComponent<Player>();
        CurEquipments.Add(EEquipmentType.Weapon, null);
        CurEquipments.Add(EEquipmentType.Armor, null);
        CurEquipments.Add(EEquipmentType.Shoes, null);
        // 초기 무기 세팅
        EquipEquipment(initialWeaponSO);
    }
    public void EquipEquipment(EquippableItemSO equipmentItemData)
    {
      
        if (equipmentItemData == null) return;
        if (!equipments.ContainsKey(equipmentItemData))
        {
            Debug.Log("장비 추가");
            // 해당 장비 없다면 장착 위치에 장비 추가
            Transform socket = null;
            switch (equipmentItemData.EquipmentType)
            {
                case EEquipmentType.Weapon:
                    socket = weaponSocket;
                    break;
                case EEquipmentType.Armor:
                    socket = ArmorSocket;
                    break;
                case EEquipmentType.Shoes:
                    socket = ShoesSocket;
                    break;
                default:
                    Debug.LogError("오류: 장비 타입 없음");
                    break;
            }
            // 소켓 없다면 null 뜰 것
            equipments.Add(equipmentItemData, Instantiate(equipmentItemData.EquipPrefab, socket));
        }
        // 해당 부위에 이미 장비가 있어도, 다른 장비 해제하고 이 장비 장착
        SetCurEquipEquipment(equipmentItemData);
    }
    void SetCurEquipEquipment(EquippableItemSO equipmentItemData)
    {
        if (CurEquipments[equipmentItemData.EquipmentType] == null)
        {
            Debug.Log("장비 없음, 장착");
            CurEquipments[equipmentItemData.EquipmentType] = equipmentItemData;
            equipments[equipmentItemData].SetActive(true);
        }
        else
        {
            Debug.Log("장비 있음, 교환 장착");
            // 이미 장비가 있다면, 그 장비 해제하고 이 장비 장착
            UnEquipEquipment(equipmentItemData.EquipmentType);
            CurEquipments[equipmentItemData.EquipmentType] = equipmentItemData;
            // 해당 장비 활성화
            equipments[equipmentItemData].SetActive(true);
        }
        if(equipmentItemData.EquipmentType == EEquipmentType.Weapon) player.SetAttackRange(); // 무기라면 공격 범위 갱신
    }
    public void UnEquipEquipment(EEquipmentType equipmentType)
    {
        EquippableItemSO preEquipmentData = CurEquipments[equipmentType];
        if (!preEquipmentData) return;
        // 장착된 장비 비활성화
        equipments[preEquipmentData]?.SetActive(false);
        // 해당 부위 장착 없음 표시
        CurEquipments[equipmentType] = null;
        // 장착 해제한 장비 인벤토리에 추가
        playerInventory.AddItem(preEquipmentData); 
    }

    public float GetDamage()
    {
        if (CurEquipments[EEquipmentType.Weapon]) return CurEquipments[EEquipmentType.Weapon].Damage;
        return -1f;
    }
}
