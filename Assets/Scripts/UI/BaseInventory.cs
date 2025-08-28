using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInventory : BaseUI
{
    [Header("인벤토리 틀 세팅")]
    [SerializeField] Transform inventoryUISlotsTranform;
    [SerializeField] GameObject inventoryUISlotGO;
    protected InventoryUISlot selectedInventoryUISlot;
    protected List<InventoryUISlot> inventoryUISlots = new List<InventoryUISlot>();
    protected PlayerInventory playerInventory;
    protected virtual void Awake()
    {
    }
    protected virtual void Start()
    {
        playerInventory = GameManager.Instance.Player.PlayerInventory;
        // 인벤토리 세팅
        InventoryInit();
    }
    public override void OpenUI()
    {
        base.OpenUI();
        if (selectedInventoryUISlot)
        {
            selectedInventoryUISlot.OffOutLine();
            selectedInventoryUISlot = null;
        }
        ResetInventoryUI();
    }
    protected void InventoryInit()
    {
        for (int i = 0; i < playerInventory.Items.Length; i++)
        {
            GameObject go = Instantiate(inventoryUISlotGO, inventoryUISlotsTranform);
            go.name = $"InventoryUISlot_{i}";
            InventoryUISlot slot = go.GetComponent<InventoryUISlot>();
            slot.Init(this, playerInventory.Items[i], i);
            inventoryUISlots.Add(slot);
        }
    }
    public virtual void SelInventoryUISlot(InventoryUISlot slot)
    {
        if(selectedInventoryUISlot) inventoryUISlots[selectedInventoryUISlot.InventoryUISlotIdx].OffOutLine(); // 이전 선택된 슬롯 아웃라인 끄기
        selectedInventoryUISlot = slot;
        if (selectedInventoryUISlot == null)
        {
            playerInventory.SelSlotIdx = -1; // 필요 없지만 안전하게
        }
    }
    
    protected void ResetInventoryUI()
    {
        for (int i = 0; i < inventoryUISlots.Count; i++)
        {
            inventoryUISlots[i].SetSlot(GameManager.Instance.Player.PlayerInventory.Items[i]);
        }
    }

}
