using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStat 
{
    float currentValue;
    float maxValue;
    public event Action<float, float> OnValueChange;
    public float CurrentValue {
        get { return currentValue; }
        set 
        {
            currentValue = Mathf.Clamp(value, 0, maxValue); 
            OnValueChange?.Invoke(currentValue, maxValue);
        }
    }
    public float MaxValue { 
        get { return maxValue; } set 
        { 
            maxValue = value; 
            OnValueChange?.Invoke(currentValue, maxValue);
        }
    }
    public CharacterStat(float maxValue)
    {
        this.maxValue = maxValue;
        currentValue = this.maxValue;
        // 생성은 CurrentValue 안쓰기 -> Awake에서 이벤트 등록 전에 호출될 수 있음
    }
    public void AddMaxValue(float amount) // 최대 체력 증가용
    {
        maxValue += amount;
        CurrentValue += amount;
    }

}
