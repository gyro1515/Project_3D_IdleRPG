using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyUtility 
{
    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            // 0 ~ i 사이의 무작위 인덱스 선택
            int j = Random.Range(0, i + 1);

            // i번째와 j번째 요소 교환
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
