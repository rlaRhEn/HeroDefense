using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerManager : MonoBehaviour
{
    [SerializeField]TowerSpawner towerSpawer;
    [SerializeField]TowerDataViewer towerDataViewer;

    public TowerCon nowObj;
    public Transform towerObjCircle, goalObjCircle;

    private void Update()
    {
        ClickMouse();
    }

    void ClickMouse()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.CompareTag("TileField"))
            {
                Debug.Log("타일선택");
                towerSpawer.SpawnTower(hit.transform);
            }
            else if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("타워 선택");
                towerDataViewer.OpenPanel(hit.transform);
            }
            else
            {

            }
        }
    }


    //void ClickMove() //캐릭터 무브
    //{
    //    if(Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        nowObj = null;
    //        goalObjCircle.transform.position = Vector2.zero;
    //        towerObjCircle.transform.position = Vector2.zero;
    //    }
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    //        for (int i = 0; i < hits.Length; i++)
    //        {
    //            RaycastHit2D hit = hits[i];
    //            if (hit.collider != null)//마우스가 오브젝트랑 닿았을 때
    //            {
    //                Debug.Log(hit.collider.gameObject.name);
    //                if(hit.collider.CompareTag("Player")) // 닿은 오브젝트가 플레이어면
    //                {
    //                    nowObj = hit.collider.GetComponent<TowerCon>();
    //                }
    //                else 
    //                {
    //                    //Set move Player object to this point
    //                    if (nowObj != null && hit.collider.CompareTag("TileField")) //지금 오브젝트가 플레이어로 값을 가지고 다른 걸 클릭했을 경우 TileField내에서만 이동가능
    //                    {
    //                        Vector2 goalPos = hit.point;
    //                        goalObjCircle.transform.position = hit.point;
    //                        nowObj.SetMovePos(goalPos);
    //                    }

    //                }
    //            }
    //        }   
    //    }
    //    if(nowObj != null) //지금 오브젝트가 플레이어로 값을 가진 후
    //    {
    //        towerObjCircle.transform.position = nowObj.transform.position;
    //    }
    //}
}
