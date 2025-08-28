using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : BaseInventory
{
    [Header("상점 창 세팅")]
    [SerializeField] Button returnlBtn;
    [SerializeField] Button buyBtn;
    [SerializeField] Button selBtn;
    [SerializeField] Transform shopSlotsTranform;
    [SerializeField] GameObject shopSlotGO;
    // 판매 가능한 아이템 리스트 SO 넣고, 그 SO를 기반으로 슬롯 초기화하기
    [SerializeField] List<ItemSO> shopItemSOList = new List<ItemSO>();
    ShopSlot selectedSlot;
    List<ShopSlot> shopSlots = new List<ShopSlot>();
    protected override void Awake()
    {
        // 인벤토리 세팅
        base.Awake();
        returnlBtn?.onClick.AddListener(CloseUI);
        buyBtn?.onClick.AddListener(OnBuyButton);
        selBtn?.onClick.AddListener(OnSelButton);
        // 상점 세팅
        // 무작위 섞기
        MyUtility.Shuffle(shopItemSOList);
        for (int i = 0; i < shopItemSOList.Count; i++)
        {
            GameObject go = Instantiate(shopSlotGO, shopSlotsTranform);
            go.name = $"ShopSlot_{i}";
            ShopSlot slot = go.GetComponent<ShopSlot>();
            slot?.Init(this, i, shopItemSOList[i]);
            shopSlots.Add(slot);
        }
        CloseUI();
    }
    protected override void Start()
    {
        base.Start();
    }
    public void SelShopSlot(ShopSlot slot)
    {
        selectedSlot = slot;
        if (selectedSlot == null) { return; } 
        ResetSelectedShopSlot(selectedSlot.ShopSlotIdx);
    }
    void ResetSelectedShopSlot(int idx)
    {
        for (int i = 0; i < shopSlots.Count; i++)
        {
            if (i == idx) continue;
            shopSlots[i].OffOutLine();
        }
    }
    void OnBuyButton()
    {
        if (!selectedSlot) return;
        //Debug.Log($"구매: {selectedSlot.ShopSlotIdx}");
        playerInventory.BuyItem(selectedSlot.ItemData);
        ResetInventoryUI();
    }
    
    void OnSelButton()
    {
        if (!selectedInventoryUISlot) return;
        //Debug.Log($"판매: {selectedInventoryUISlot}");
        playerInventory.SelSlotIdx = selectedInventoryUISlot.InventoryUISlotIdx;
        playerInventory.SelItem();
        ResetInventoryUI();
    }
    

}
