using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [Header("상점 아이템 슬롯 세팅")]
    [SerializeField] Button slotButton;
    [SerializeField] Outline outline;
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemPriceText;

    Shop shop;
    public ItemSO ItemData { get; private set; }
    public int ShopSlotIdx { get; private set; }
    private void Awake()
    {
        slotButton?.onClick.AddListener(OnClickSlot);
    }
    public void Init(Shop _shop, int idx, ItemSO _itemData)
    {
        shop = _shop;
        ShopSlotIdx = idx;
        ItemData = _itemData;
        itemImage.sprite = ItemData.Icon;
        itemNameText.text = ItemData.DisplayName;
        itemPriceText.text = ItemData.Price.ToString() + " G";

        outline.enabled = false; // 아웃 라인은 처음에는 비활성화
    }
    void OnClickSlot()
    {
        if (outline == null) return;

        outline.enabled = !outline.enabled; // 클릭할 때마다 아웃 라인 토글
        shop?.SelShopSlot(outline.enabled ? this : null);
    }
    public void OffOutLine()
    {
        if (outline == null) return;
        outline.enabled = false;
    }
}
