using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneNames { loading =0, Lobby, Defense }
public class BuildScene
{
  public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    public static void LoadScene(string sceneName = "")
    {
        if(sceneName == "")
        {
            SceneManager.LoadScene(GetActiveScene());
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    public static void LoadScene(SceneNames sceneName)
    {
        //열거형으로 매개변수를 받아온 경우 Tostring() 처리
        SceneManager.LoadScene(sceneName.ToString());
    }
}
