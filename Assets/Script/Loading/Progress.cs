using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{

    [SerializeField] Slider sliderProgress;
    [SerializeField] Text textPrgressData;
    [SerializeField] float progressTime;

    public void Play(UnityAction action = null)
    {
        StartCoroutine(OnProgress(action));
    }
    IEnumerator OnProgress(UnityAction action)
    {
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = (current/3) / progressTime;

            //text 정보설정
            textPrgressData.text = $"Now Loading... {sliderProgress.value * 100:F0}%";

            //slider 값 설정
            sliderProgress.value = Mathf.Lerp(0, 1, percent);

            yield return null;
        }
        //actin이 null이 아니면 action 메서드 실행
        action?.Invoke();
    }
}
