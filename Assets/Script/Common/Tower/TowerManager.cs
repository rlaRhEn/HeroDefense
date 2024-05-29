using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerManager : MonoBehaviour
{
    [SerializeField]TowerSpawner towerSpawer;
    [SerializeField]TowerDataViewer towerDataViewer;
    [SerializeField] TowerAttackRange towerattackRange;

    //public TowerCon nowObj;
    //public Transform towerObjCircle, goalObjCircle;

    private Transform selectedTile;
    private Transform previousTile; // 이전에 선택한 타일
    private Color originalColor; // 이전 타일의 원래 색상

    private void Update()
    {
        ClickMouse();
    }

    void ClickMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("TileField")) // 선택 시 구매 -> 선택 후 버튼을 눌러야 구매
                {
                    Debug.Log("타일선택");
                    towerDataViewer.ClosePanel();
                    towerattackRange.OffAttackRange();

                    // 이전 타일의 알파값 복원
                    if (previousTile != null)
                    {
                        SpriteRenderer previousRenderer = previousTile.GetComponent<SpriteRenderer>();
                        if (previousRenderer != null)
                        {
                            previousRenderer.color = originalColor;
                        }
                    }

                    // 새로 선택한 타일의 알파값 변경
                    SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        originalColor = spriteRenderer.color; // 현재 색상 저장
                        Color newColor = spriteRenderer.color;
                        newColor.a = 0.5f; // 원하는 알파값 (0.0f ~ 1.0f)
                        spriteRenderer.color = newColor;
                    }

                    previousTile = hit.transform; // 현재 타일을 이전 타일로 설정
                    selectedTile = hit.transform; // 선택된 타일 설정

                }
                else if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("타워 선택");
                    towerDataViewer.OpenPanel(hit.transform);

                }
            }
        }
    }
   public Transform GetSelectedTile()
    {
        return selectedTile;
    }
    public void BuildTower()
    {
        Transform selecteTile = GetSelectedTile();
        if(selecteTile != null)
        {
            towerSpawer.SpawnTower(selecteTile);
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
