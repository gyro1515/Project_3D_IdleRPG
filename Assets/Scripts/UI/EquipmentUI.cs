using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : BaseInventory
{
    [Header("장착 창 세팅")]
    [SerializeField] Button returnlBtn;
    [SerializeField] Button equipBtn;
    [SerializeField] Button unEquipBtn;
    [SerializeField] Button useBtn;
    [SerializeField] Transform equipmentUISlotsTranform;
    [SerializeField] TextMeshProUGUI itemDescriptionText;
    EquipmentUISlot selectedEquipmentUISlot;
    Dictionary<EEquipmentType, EquipmentUISlot> equipmentUISlotDictionary = new Dictionary<EEquipmentType, EquipmentUISlot>();
    PlayerEquipment playerEquipment;

    protected override void Awake()
    {
        // 인벤토리 세팅
        base.Awake();
        returnlBtn?.onClick.AddListener(CloseUI);
        equipBtn?.onClick.AddListener(OnEquipItem);
        unEquipBtn?.onClick.AddListener(UnEquipItem);
        useBtn?.onClick.AddListener(OnUseItem);
        // 장착 세팅
        for (int i = 0; i < equipmentUISlotsTranform.childCount; i++)
        {
            EquipmentUISlot slot = equipmentUISlotsTranform.GetChild(i).GetComponent<EquipmentUISlot>();
            if (!slot || slot.EquipmentType == EEquipmentType.None) { Debug.LogError($"EquipmentUISlot 세팅 오류! {slot.name}"); }
            equipmentUISlotDictionary.Add(slot.EquipmentType, slot);
        }
    }
    protected override void Start()
    {
        base.Start();
        playerEquipment = GameManager.Instance.Player.PlayerEquipment;
        foreach(var equipmentUIslot in equipmentUISlotDictionary)
        {
            equipmentUIslot.Value.Init(this, null);
        }
        CloseUI();
    }
    public override void OpenUI()
    {
        base.OpenUI();
        if (selectedEquipmentUISlot)
        {
            selectedEquipmentUISlot.OffOutLine();
            selectedEquipmentUISlot = null;
        }
        ResetEquipmentUI();
        ResetInventoryUI();
        equipBtn.gameObject.SetActive(false);
        useBtn.gameObject.SetActive(false);
    }
    public override void SelInventoryUISlot(InventoryUISlot slot)
    {
        base.SelInventoryUISlot(slot);
        if (selectedInventoryUISlot == null)
        {
            equipBtn.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(false);
            itemDescriptionText.text = "";
        }
        else
        {
            EItemType itemType = inventoryUISlots[selectedInventoryUISlot.InventoryUISlotIdx].ItemData.ItemType;
            itemDescriptionText.text = inventoryUISlots[selectedInventoryUISlot.InventoryUISlotIdx].ItemData.Description;
            equipBtn.gameObject.SetActive(itemType == EItemType.Equipable);
            useBtn.gameObject.SetActive(itemType == EItemType.Consumable);
        }
    }

    public void SelEquipmentUISlot(EquipmentUISlot slot)
    {
        if (selectedEquipmentUISlot && selectedEquipmentUISlot != slot) equipmentUISlotDictionary[selectedEquipmentUISlot.EquipmentType].OffOutLine(); // 이전 선택된 슬롯 아웃라인 끄기
        selectedEquipmentUISlot = slot;
        if (selectedEquipmentUISlot == null) {/* Debug.Log("null이야");*/ return; }
    }
    void OnEquipItem()
    {
        if (!selectedInventoryUISlot) return;
        EquippableItemSO equippableItemSO = selectedInventoryUISlot.ItemData as EquippableItemSO;
        // 인벤토리 아이템은 삭제
        playerInventory.RemoveItem(selectedInventoryUISlot.InventoryUISlotIdx);
        // 인벤토리 설명/버튼 UI도 설정
        SelInventoryUISlot(null);
        // 장비창에 장착
        playerEquipment?.EquipEquipment(equippableItemSO);
        // UI 갱신
        ResetEquipmentUI();
        ResetInventoryUI();
    }
    void UnEquipItem()
    {
        if (!selectedEquipmentUISlot) return;
        playerEquipment?.UnEquipEquipment(equipmentUISlotDictionary[selectedEquipmentUISlot.EquipmentType].EquipmentType);
        ResetEquipmentUI();
        ResetInventoryUI();
    }
    void OnUseItem()
    {
        playerInventory.SelSlotIdx = selectedInventoryUISlot.InventoryUISlotIdx;
        if (playerInventory.UseItem()) SelInventoryUISlot(null); // 아이템을 다 사용했다면 UI 세팅해주기
        ResetInventoryUI();
    }
    protected void ResetEquipmentUI()
    {
        foreach (var playerEquipment in playerEquipment.CurEquipments)
        {
            equipmentUISlotDictionary[playerEquipment.Key].SetSlot(playerEquipment.Value);
        }
    }
}
