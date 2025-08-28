using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationItemUI : BaseUI
{
    [SerializeField] GameObject durationItemUISlotPrefab;
    [SerializeField] Transform slotsTransform;

    List<DurationItemSlot> durationItemUISlots = new List<DurationItemSlot>(); // 오브젝트 풀링
    Dictionary<ConsumableItemSO, DurationItemSlot> usedDurationItemSlotDic = new Dictionary<ConsumableItemSO, DurationItemSlot>();
    public Dictionary<ConsumableItemSO, DurationItemSlot> UsedDurationItemSlotDic 
    { 
        get { return usedDurationItemSlotDic; } 
        private set { usedDurationItemSlotDic = value; } 
    }
    public void AddDurationItemUISlot(ConsumableItemSO itemData)
    {
        if(usedDurationItemSlotDic.ContainsKey(itemData)) // 이미 포함시
        {
            // 기존 갱신
            usedDurationItemSlotDic[itemData].ResetSlot();
            return;
        }

        // 돌아가는 아이템이 아니라면
        for (int i = 0; i < durationItemUISlots.Count; i++)
        {
            if (durationItemUISlots[i].gameObject.activeSelf) continue; // 활성화된 상태면 패스

            durationItemUISlots[i].Init(itemData, this);
            usedDurationItemSlotDic.Add(itemData, durationItemUISlots[i]);
            return; // 재사용하고 종료
        }
        // 재사용할 오브젝트가 없으면 새로 생성
        GameObject go = Instantiate(durationItemUISlotPrefab, slotsTransform);
        DurationItemSlot durationItemUISlot = go.GetComponent<DurationItemSlot>();
        if (durationItemUISlot)
        {
            durationItemUISlot?.Init(itemData, this);
            durationItemUISlots.Add(durationItemUISlot);
            usedDurationItemSlotDic.Add(itemData, durationItemUISlot);

        }
    }
}
