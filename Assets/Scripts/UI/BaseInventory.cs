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
        // 인벤토리 세팅
        InventoryInit();
    }
    protected virtual void Start()
    {
        playerInventory = GameManager.Instance.Player.PlayerInventory;
    }
    public override void OpenUI()
    {
        base.OpenUI();
        ResetInventoryUI();
    }
    protected void InventoryInit()
    {
        for (int i = 0; i < GameManager.Instance.Player.PlayerInventory.Items.Length; i++)
        {
            GameObject go = Instantiate(inventoryUISlotGO, inventoryUISlotsTranform);
            go.name = $"InventoryUISlot_{i}";
            InventoryUISlot slot = go.GetComponent<InventoryUISlot>();
            slot.Init(this, GameManager.Instance.Player.PlayerInventory.Items[i], i);
            inventoryUISlots.Add(slot);
        }
    }
    public void SelInventoryUISlot(InventoryUISlot slot)
    {
        selectedInventoryUISlot = slot;
        if (selectedInventoryUISlot == null)
        {
            playerInventory.SelSlotIdx = -1; // 필요 없지만 안전하게
            return;
        }
        ResetSelectedInventoryUISlot(selectedInventoryUISlot.InventoryUISlotIdx);
    }
    void ResetSelectedInventoryUISlot(int idx)
    {
        for (int i = 0; i < inventoryUISlots.Count; i++)
        {
            if (i == idx) continue;
            inventoryUISlots[i].OffOutLine();
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
