using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurnOffField : MonoBehaviour
{
    GameObject obj_Field;
    Image image;
    [SerializeField] SceneNames sceneName;

    private bool checkbool = false; // 투명도 조절 논리형 변수
    private bool turnOffbool = false;

    private void Awake()
    {
        obj_Field = this.gameObject; //스크립트 참조된 오브젝트
        image = obj_Field.GetComponent<Image>(); 
    }
    private void Update()
    {
        if(turnOffbool)
        {
            StartCoroutine(SplashField());
        }
        if (checkbool)
        {
            BuildScene.LoadScene(sceneName);
            //Destroy(this.gameObject);
        }
    }
    IEnumerator SplashField()
    {
        Color color = image.color;

        for (int i = 100; i >= 0; i--)
        {
            color.a -= Time.deltaTime * 0.01f;
            image.color = color;

            if(image.color.a <=0)
            {
                checkbool = true;
            }
        }
        yield return null;
    }
    public void NextScene()
    {
        turnOffbool = true;
    }
}
