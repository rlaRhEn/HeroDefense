using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGotcha : MonoBehaviour
{
    // 85% 확률로 성공을 반환하는 함수
    public bool TrySuccess()
    {
        float randomValue = Random.value; // 0.0에서 1.0 사이의 난수를 생성
        Debug.Log(randomValue);
        return randomValue < 0.5f; // 85% 확률로 true 반환
    }

    // 테스트 함수
   public void OnClickRandon()
    {
        if (TrySuccess())
        {
            Debug.Log("성공");
        }
        else
        {
            Debug.Log("실패");
        }
    }
}
