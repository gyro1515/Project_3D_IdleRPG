using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStat 
{
    float currentValue;
    float maxValue;
    public event Action<float> OnValueChange;
    public float CurrentValue {
        get { return currentValue; }
        set 
        {
            currentValue = Mathf.Clamp(value, 0, maxValue); 
            OnValueChange?.Invoke(currentValue / maxValue);
        } 
    }
    public CharacterStat(float maxValue)
    {
        this.maxValue = maxValue;
        currentValue = this.maxValue;
    }


}
