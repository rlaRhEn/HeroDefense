using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //프리팹 보관할 변수 2개있으면
    public GameObject[] monsterPrefabs;
    public GameObject[] projectilePrefabs;
   

    //풀 담당을 하는 리스트들 2개필요
    public List<GameObject>[] monsterPools;
    List<GameObject>[] projectilePools;

    public List<GameObject>[] MonsterPools => monsterPools;
    private void Awake()
    {
        monsterPools = new List<GameObject>[monsterPrefabs.Length];
        //몬스터 자동생성
        for (int index = 0; index < monsterPools.Length; index++)
        {
            monsterPools[index] = new List<GameObject>();
        }

        projectilePools = new List<GameObject>[projectilePrefabs.Length];
        //발사체 자동생성
        for (int index = 0; index < projectilePools.Length; index++)
        {
            projectilePools[index] = new List<GameObject>();
        }

    }
    public GameObject GetMonster(int index)//게임오브젝트 반환 함수
    {
        GameObject select = null;

        //선택한 풀의 놀고 있는(비활성화 된) 게임오브젝트 접근
        //발견하면 select 변수에 할당
        foreach (GameObject item in monsterPools[index]) // 배열이나 리스트에 순차적인 반복문
        {
            if (item != null)
            {
                if (!item.activeSelf) // activeself 오브젝트가 비활성화(대기상태)인지 확인
                {
                    select = item;
                    select.SetActive(true);
                    break;
                }
            }
        }
        //못 찾았으면 새롭게 생성하여 select 변수에 할당 
        if (!select) //새롭게 생성하고 select 변수에 할당
        {
            select = Instantiate(monsterPrefabs[index], transform);
            monsterPools[index].Add(select);
        }
        return select;
    }

    public GameObject GetProJectile(int index)//게임오브젝트 반환 함수
    {
        GameObject select = null;

        //선택한 풀의 놀고 있는(비활성화 된) 게임오브젝트 접근
        //발견하면 select 변수에 할당
        foreach (GameObject item in projectilePools[index]) // 배열이나 리스트에 순차적인 반복문
        {
            if (item != null)
            {
                if (!item.activeSelf) // activeself 오브젝트가 비활성화(대기상태)인지 확인
                {
                    select = item;
                    select.SetActive(true);
                    break;
                }
            }
        }
        //못 찾았으면 새롭게 생성하여 select 변수에 할당 
        if (!select) //새롭게 생성하고 select 변수에 할당
        {
            select = Instantiate(projectilePrefabs[index], transform);
            projectilePools[index].Add(select);
        }
        return select;
    }
    public void OnDie(GameObject obj)
    {
        obj.SetActive(false);
    }
}
