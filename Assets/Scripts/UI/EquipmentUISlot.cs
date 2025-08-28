using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUISlot : MonoBehaviour
{
    [Header("장비 슬롯 세팅")]
    [SerializeField] Button slotButton;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI nameText;  // 아이콘이 없는 관계로 이름 표시..
    [SerializeField] EEquipmentType equipmentType = EEquipmentType.None;  // 장비 타입 세팅 필수
    private Outline outline;  // 장비 장착시 Outline 표시위한 컴포넌트
    EquipmentUI equipmentUI;
    public EquippableItemSO EquipmentItemData { get;  set; }
    public EEquipmentType EquipmentType { get { return equipmentType; } }
    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;  // 처음엔 비활성화
        slotButton?.onClick.AddListener(OnClickSlot);
    }
    public void Init(EquipmentUI _equipmentUI, EquippableItemSO equipmentItemData)
    {
        equipmentUI = _equipmentUI;
        SetSlot(equipmentItemData);
    }
    public void SetSlot(EquippableItemSO equipmentItemData)
    {
        if (equipmentItemData == null)
        {
            EquipmentItemData = null;
            icon.sprite = null;
            nameText.text = "";
            outline.enabled = false;
            return;
        }
        icon.sprite = equipmentItemData.Icon;
        nameText.text = equipmentItemData.DisplayName;
        EquipmentItemData = equipmentItemData;
    }
    void OnClickSlot()
    {
        if (outline == null) return;
        if (icon.sprite == null) return; // 아이템이 없으면 리턴
        Debug.Log($"{outline.enabled} 슬롯 클릭 전!");
        outline.enabled = !outline.enabled; // 클릭할 때마다 아웃 라인 토글
        Debug.Log($"{outline.enabled} 슬롯 클릭 후!");
        equipmentUI.SelEquipmentUISlot(outline.enabled ? this : null);
    }
    public void OffOutLine()
    {
        if (outline == null) return;
        outline.enabled = false;
    }
}
