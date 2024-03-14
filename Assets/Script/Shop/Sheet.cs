using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

using UnityEngine.Networking;

public class Sheet : MonoBehaviour
{
    public readonly string ADRESS = "https://docs.google.com/spreadsheets/d/1-zS5N7VAfl30BOx_vuHpB-Muo6KbDsRVOYAway74BeM";
    public readonly string RANGE = "A1:D";
    public readonly long SHEET_ID = 0;
   public static string GetSVAdress(string adress, string range, long sheetID)
    {//주소반환
        return $"{adress}/export?format=tsv&range={range}&gid={sheetID}";
    }
    private void Start()
    {
        StartCoroutine(LoadData());
       
    }
    private IEnumerator LoadData()
    {
        UnityWebRequest www = UnityWebRequest.Get(GetSVAdress(ADRESS, RANGE, SHEET_ID));
        yield return www.SendWebRequest();

        Debug.Log(GetData<Animal>(www.downloadHandler.text.Split('\n')[0].Split('\t')).name);//첫번째 행의 데이터
    }
    T GetData<T>(string[] datas)
    {
        object data = Activator.CreateInstance(typeof(T));

        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < datas.Length; i++)
        {
            try
            {
                // string > parse
                Type type = fields[i].FieldType;

                if (string.IsNullOrEmpty(datas[i])) continue;

                // 변수에 맞는 자료형으로 파싱해서 넣는다
                if (type == typeof(int))
                    fields[i].SetValue(data, int.Parse(datas[i]));

                else if (type == typeof(float))
                    fields[i].SetValue(data, float.Parse(datas[i]));

                else if (type == typeof(bool))
                    fields[i].SetValue(data, bool.Parse(datas[i]));

                else if (type == typeof(string))
                    fields[i].SetValue(data, datas[i]);

                // enum
                else
                    fields[i].SetValue(data, Enum.Parse(type, datas[i]));
            }

            catch (Exception e)
            {
                Debug.LogError($"SpreadSheet Error : {e.Message}");
            }
        }
        return (T)data;
    }
}
[System.Serializable]
public class Animal //데이터 담을 클래스 코드
{ //주의사항 1. 시트에있는 데이터와 일치
  //2.enum 값은 시트에 나와있는 데이터와 이름이 완전똑같아야한다.
  // >> pink나 PINK는 안됨
    public string name;
    public int attack;
    public float shield;
    public ColorType colorType;
}

public enum ColorType
{
    pink, Orange, Blue
}