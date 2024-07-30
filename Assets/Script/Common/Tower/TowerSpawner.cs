using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] towerPrefab;

    [SerializeField] int towerBuildGold = 50;
    [SerializeField] PlayerGold playerGold; 

    public void SpawnTower(Transform tileTransform)
    {
        int randomTower;
        randomTower = Random.Range(0, towerPrefab.Length); //랜덤
        Debug.Log(randomTower);
        //타워 건설만큼의 돈 없으면 건설 x
        if(towerBuildGold > playerGold.CurrentGold)
        {
            return;
        }
        Tile tile = tileTransform.GetComponent<Tile>();
        //현재 타일의 위치에 이미 타워가 건설되어 있으면 타워 건설 x
        if(tile.IsBuildTower ==true)
        {
            return;
        }
        //타워가 건설되어 있음으로 설정
        tile.IsBuildTower = true;
        //타워 건설에 필요한 골드만큼 감소
        playerGold.CurrentGold -= towerBuildGold;
        //선택한 타일의 위치에 타워건설(타일 보다 z축 -1의 위치에 배치)
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject tower =  Instantiate(towerPrefab[randomTower], position, Quaternion.identity);
        tower.GetComponent<TowerCon>().SetUp(tile);
    }
}
