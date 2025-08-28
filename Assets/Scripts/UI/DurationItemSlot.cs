using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DurationItemSlot : MonoBehaviour
{
    [SerializeField] Image valueImg;
    [SerializeField] Image iconImg;
    [SerializeField] TextMeshProUGUI nameText;
    DurationItemUI durationUI;
    ConsumableItemSO itemData;
    float timer = -1f;
    float duration;
    float preValue;
    private void Update()
    {
        if (timer == -1f) return;
        timer += Time.deltaTime;
        timer = Math.Clamp(timer, 0.0f, duration);
        SetValue(1 - timer / duration);
    }
    public void Init(ConsumableItemSO _itemData, DurationItemUI _durationUI)
    {
        durationUI = _durationUI;
        iconImg.sprite = _itemData.Icon;
        valueImg.fillAmount = 1f;
        nameText.text = _itemData.DisplayName;
        gameObject.SetActive(true);
        enabled = true;
        timer = 0.0f;
        duration = _itemData.Consumables[0].duration;
        itemData = _itemData;
        switch (itemData.Consumables[0].type)
        {
            case ConsumableType.Damage:
                preValue = GameManager.Instance.Player.AttackDamageModifier;
                GameManager.Instance.Player.AttackDamageModifier = itemData.Consumables[0].value;
                break;
            case ConsumableType.AttackRange:
                preValue = GameManager.Instance.Player.AttackRangeModifier;
                GameManager.Instance.Player.AttackRangeModifier = itemData.Consumables[0].value;
                break;
        }
    }
    public void ResetSlot()
    {
        timer = 0.0f;
        valueImg.fillAmount = 1f;
    }
    void SetValue(float value)
    {
        valueImg.fillAmount = value;
        if (value > 0) return;
        switch (itemData.Consumables[0].type)
        {
            case ConsumableType.Damage:
                GameManager.Instance.Player.AttackDamageModifier = preValue;
                break;
            case ConsumableType.AttackRange:
                GameManager.Instance.Player.AttackRangeModifier = preValue;
                break;
        }
        gameObject.SetActive(false);
        enabled = false;
        iconImg.sprite = null;
        timer = -1f;
        durationUI.UsedDurationItemSlotDic.Remove(itemData);
        itemData = null;
    }
}
