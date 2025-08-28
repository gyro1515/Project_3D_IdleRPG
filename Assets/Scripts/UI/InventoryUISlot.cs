using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour
{
    [Header("상점 인벤토리 슬롯 세팅")]
    [SerializeField] Button slotButton;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI cntText;  // 수량표시 Text
    [SerializeField] TextMeshProUGUI nameText;  // 아이콘이 없는 관계로 이름 표시.. ㅠㅠ
    private Outline outline;  // 장비 장착시 Outline 표시위한 컴포넌트
    public int InventoryUISlotIdx { get; private set; }
    public ItemSO ItemData { get; private set; }
    BaseInventory baseInventory;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;  // 처음엔 비활성화
        slotButton?.onClick.AddListener(OnClickSlot);
    }
    public void Init(BaseInventory _baseInventory, InventorySlot slotData, int idx)
    {
        baseInventory = _baseInventory;
        SetSlot(slotData);
        InventoryUISlotIdx = idx;
    }
    public void SetSlot(InventorySlot slotData)
    {
        if (slotData.ItemData == null)
        {
            ItemData = null;
            icon.sprite = null;
            cntText.text = "";
            nameText.text = "";
            outline.enabled = false;
            return;
        }
        icon.sprite = slotData.ItemData.Icon;
        cntText.text = slotData.ItemData.ItemType == EItemType.Consumable ? slotData.ItemCount.ToString() : ""; // 소모품이 아니면 수량표시
        nameText.text = slotData.ItemData.DisplayName;
        ItemData = slotData.ItemData;
        //outline.enabled = true;
    }
    void OnClickSlot()
    {
        if (outline == null) return;
        if (icon.sprite == null) return; // 아이템이 없으면 리턴
        outline.enabled = !outline.enabled; // 클릭할 때마다 아웃 라인 토글
        baseInventory?.SelInventoryUISlot(outline.enabled ? this : null);
    }
    public void OffOutLine()
    {
        if (outline == null) return;
        outline.enabled = false;
    }
}
