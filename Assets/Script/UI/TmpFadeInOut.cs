using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TmpFadeInOut : MonoBehaviour
{

    [SerializeField]TextMeshProUGUI successMeshPro;
    [SerializeField]TextMeshProUGUI failedMeshPro;
    public float fadeInDuration = 2.0f; // 알파 값이 0에서 1로 변하는 데 걸리는 시간
    public float fadeOutDuration = 2.0f; // 알파 값이 1에서 0으로 변하는 데 걸리는 시간

    void Start()
    {
        
        // 코루틴 시작
        //StartCoroutine(FadeInAndOutText());
    }

    public IEnumerator SuccessFadeInAndOutText()
    {
        Color originalColor = successMeshPro.color;
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0); //알파 0

        // 텍스트의 시작 색상 설정
        successMeshPro.color = transparentColor;

        // 알파 값 증가 (페이드 인)
        float elapsedTime = 0;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeInDuration);
            successMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null; // 다음 프레임까지 대기
        }


        // 알파 값 감소 (페이드 아웃)
        elapsedTime = 0;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - elapsedTime / fadeOutDuration);
            successMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null; // 다음 프레임까지 대기
        }

        // 마지막으로 알파 값을 확실하게 0으로 설정
        successMeshPro.color = transparentColor;
    }


    public IEnumerator FailedFadeInAndOutText()
    {
        Color originalColor = failedMeshPro.color;
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0); //알파 0

        // 텍스트의 시작 색상 설정
        failedMeshPro.color = transparentColor;

        // 알파 값 증가 (페이드 인)
        float elapsedTime = 0;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeInDuration);
            failedMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null; // 다음 프레임까지 대기
        }


        // 알파 값 감소 (페이드 아웃)
        elapsedTime = 0;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - elapsedTime / fadeOutDuration);
            failedMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null; // 다음 프레임까지 대기
        }

        // 마지막으로 알파 값을 확실하게 0으로 설정
        failedMeshPro.color = transparentColor;
    }
}

