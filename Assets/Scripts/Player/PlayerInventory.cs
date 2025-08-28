using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    [Header("플레이어 인벤토리")]
    const int maxItemCount = 12;
    Player player;
    // 띄워서 확인하기
    public InventorySlot[] Items { get; private set; }
    public int SelSlotIdx { get; set; } = -1; // 선택된 슬롯 인덱스
    private void Awake()
    {
        player = GetComponent<Player>();
        Items = new InventorySlot[maxItemCount];
        for (int i = 0; i < maxItemCount; i++)
        {
            Items[i] = new InventorySlot();
        }
    }
    public void BuyItem(ItemSO itemData, int cnt = 1)
    {
        if (itemData.Price > player.Gold) { Debug.Log("돈 없다"); return; }// 돈이 부족하면 리턴
        if (AddItem(itemData, cnt)) { player.Gold -= itemData.Price * cnt;  /*Debug.Log("샀다");*/ } // 아이템 추가시에만 돈 차감
    }
    bool AddItem(ItemSO itemData, int cnt = 1)
    {
        if (itemData.ItemType == EItemType.Consumable) // 소모품일 경우
        {
            ConsumableItemSO consumableItem = itemData as ConsumableItemSO;
            if (!consumableItem) { Debug.LogError("오류"); return false; } // 캐스팅 실패시 리턴 -> 오류 
            // 먼저 겹칠 수 있는지 확인
            for (int i = 0; i < Items.Length; i++)
            {
                // 같은 아이템이고, 최대 개수보다 작다면 겹치기
                if (Items[i].ItemData == itemData && consumableItem.MaxStackAmount >= Items[i].ItemCount + cnt) 
                {
                    //Debug.Log("겹침");
                    Items[i].ItemCount += cnt;
                    return true; //아이템이 추가 되었다면 리턴
                }
                else continue;
            }
        }

        // 여기는 빈 칸에 아이템 추가
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].ItemData == null) // 빈칸 발견
            {
                //Debug.Log("추가");
                Items[i].ItemData = itemData;
                Items[i].ItemCount = cnt;
                return true;
            }
        }
        Debug.Log("인벤토리가 가득 찼습니다!");
        return false;
    }
    public void SelItem()
    {
        if (SelSlotIdx == -1) return;
        if (Items[SelSlotIdx].ItemData == null) return;
        Items[SelSlotIdx].ItemCount--;
        player.Gold += Mathf.RoundToInt(Items[SelSlotIdx].ItemData.Price * 0.7f); // 판매가는 70%, 반올림
        if (Items[SelSlotIdx].ItemCount <= 0) // 아이템 개수가 0이하라면
        {
            Items[SelSlotIdx].ItemData = null;
            Items[SelSlotIdx].ItemCount = 0;
        }
    }
    public void UseOrEquipItem()
    {
        if (SelSlotIdx == -1) return;
        if (Items[SelSlotIdx].ItemData == null) return;
        if (Items[SelSlotIdx].ItemData.ItemType == EItemType.Consumable) // 소비 아이템
        {
            ConsumableItemSO consumableItem = Items[SelSlotIdx].ItemData as ConsumableItemSO;
            if (!consumableItem) { Debug.LogError("오류"); return; } // 캐스팅 실패시 리턴 -> 오류 
            //consumableItem.UseItem(player); // 아이템 사용
            Debug.Log($"{consumableItem.DisplayName} 사용!");
            Items[SelSlotIdx].ItemCount--;
            if (Items[SelSlotIdx].ItemCount <= 0) // 아이템 개수가 0이하라면
            {
                Items[SelSlotIdx].ItemData = null;
                Items[SelSlotIdx].ItemCount = 0;
            }
        }
        else // 장착 아이템
        {
             EquippableItemSO equippableItemData = Items[SelSlotIdx].ItemData as EquippableItemSO;
            if (!equippableItemData) { Debug.LogError("오류"); return; } // 캐스팅 실패시 리턴 -> 오류 
            //equippableItemData.EquipItem(player); // 아이템 장착
            Debug.Log($"{equippableItemData.DisplayName} 장착!");
        }
    }
}
