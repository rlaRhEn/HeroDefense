using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScenario : MonoBehaviour
{
    [SerializeField] Progress progress;
    [SerializeField] SceneNames nextScene;

    private void Awake()
    {
        SystemSetUp();
    }
    void SystemSetUp()
    {
        //해상도 설정(1960,1080)
        int width = Screen.width;
        int height = Screen.height;
        Debug.Log(height);
        Screen.SetResolution(width, height, true);

        //화면이 꺼지지않도록 설정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        progress.Play(OnAfterProgress);
    }
    void OnAfterProgress()
    {
        BuildScene.LoadScene(nextScene);
    }
}
