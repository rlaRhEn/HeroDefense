using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDataViewer : MonoBehaviour
{
    //[SerializeField] Text textInfo;
    [SerializeField] Text textLevel; //레벨
    [SerializeField] Text textAttack; //공격력
    [SerializeField] Text textUpgrade;// 강화학률
    [SerializeField] Text textAttackIncrease; // 레벨에 따른 공격력 상승률
    [SerializeField] Text textCost; //업그레이드 비용

    [SerializeField] Button buttonUpgrade;

    [SerializeField] TowerAttackRange towerAttackRange;
    TowerCon currentTower;


    private void Start()
    {
        ClosePanel();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }

    public void OpenPanel(Transform towerCon)
    {
        currentTower = towerCon.GetComponent<TowerCon>();
        //타워정보 Panel On
        gameObject.SetActive(true);
        UpdateTowerData();
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    void UpdateTowerData()
    {
        //textInfo.text =
        textLevel.text = currentTower.Level.ToString();
        textAttack.text = currentTower.Attack.ToString();
        textUpgrade.text = currentTower.Level + " -> " + (currentTower.Level+1) + "\n 성공확률: " + currentTower.Probablilty + "%" + "\n실패(유지): " + currentTower.Fail + "%"; 
        textAttackIncrease.text = currentTower.AttackIncrease.ToString();
        textCost.text = currentTower.Cost.ToString();

        //업그레이드가 불가능해지면 버튼 비활성화
        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;
    }
    public void OnClickEventTowerUpgrade()
    {
        //타워 업그레이드 시도 (성공: true , 실패: false)
        bool isSuccess = currentTower.Upgrade();

        if(isSuccess == true)
        {
            //타워가 업그레이드 성공 시 타워 정보 갱신
            UpdateTowerData();
        }
    }
    public void OnClickEventTowerSell()
    {
        //타워 판매
        currentTower.Sell();
        //선택한 타워가 사라짐
        ClosePanel();
    }
}
