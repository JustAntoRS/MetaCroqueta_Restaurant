using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMovementManager : Singleton<FoodMovementManager>
{
    [SerializeField] private List<GameObject> positionList;

    public GameObject GetRandomMovementPoint()
    {
        int index = Random.Range(0, positionList.Count);

        return positionList[index];
    }
    
    public Vector3 GetRandomPosition()
    {
        int index = Random.Range(0, positionList.Count);

        return positionList[index].transform.position;
    }
}
