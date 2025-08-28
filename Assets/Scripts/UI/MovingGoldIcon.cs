using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGoldIcon : MonoBehaviour
{
    Vector3 endPos;
    Vector3 startPos;
    float timer = 0.0f;
    private void Update()
    {
        timer += Time.deltaTime;
        gameObject.transform.position = Vector3.Lerp(startPos, endPos, timer);
        if (timer >= 1.0f)
        {
            timer = 0.0f;
            gameObject.SetActive(false);
            GameManager.Instance.Player.Gold++;
        }
    }
    public void Init(Vector3 pos, Vector3 _endPos)
    {
        endPos = _endPos;
        // 시작 위치
        startPos = Camera.main.WorldToScreenPoint(pos);
        gameObject.transform.position = pos;
        gameObject.SetActive(true);
    }
}
