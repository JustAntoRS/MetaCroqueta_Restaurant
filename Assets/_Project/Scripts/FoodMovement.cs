using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class FoodMovement : MonoBehaviour
{
    [SerializeField] private GameObject targetPos;
    [SerializeField] private bool movementFinished;
    
    [Range(0f, 20f)]
    [SerializeField] private float movementSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        movementFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;
        
        if (movementFinished)
        {
            movementFinished = false;
            targetPos = FoodMovementManager.Instance.GetRandomMovementPoint();
            Debug.Log("Nuevo Destino");
        }
        
        transform.position = Vector3.Lerp(currentPos, targetPos.transform.position, Time.deltaTime * movementSpeed);
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MovementPoint") && targetPos == other.gameObject)
        {
            movementFinished = true;
        }
    }
}
