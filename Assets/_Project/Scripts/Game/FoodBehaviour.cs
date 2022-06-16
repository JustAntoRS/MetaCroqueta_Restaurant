using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    #region Vars
    
    [SerializeField] private GameObject targetPos;
    [Range(0f, 20f)]
    [SerializeField] private float movementSpeed;

    [SerializeField] private bool isStarted;
    
    private bool movementFinished;
    
    #endregion
    
    #region Unity
    
    // Start is called before the first frame update
    void Start()
    {
        isStarted = false;
        movementFinished = true;
        targetPos = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;
        
        if (movementFinished && isStarted)
        {
            movementFinished = false;
            targetPos = FoodMovementManager.Instance.GetRandomMovementPoint();
            Debug.Log("Nuevo Destino");
        }
        
        transform.position = Vector3.Lerp(currentPos, targetPos.transform.position, Time.deltaTime * movementSpeed);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mouth"))
        {
            Debug.Log($"Comido {other.name}");
            GameManager.Instance.IncreasePoints(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MovementPoint") && targetPos == other.gameObject)
        {
            movementFinished = true;
        }
    }
    
    #endregion
    
    #region Methods
    
    public void StartMovement()
    {
        isStarted = true;
    }
    
    #endregion
}
